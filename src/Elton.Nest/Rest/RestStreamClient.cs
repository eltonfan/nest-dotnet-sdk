﻿#region License

//   Copyright 2018 Elton FAN (eltonfan@live.cn, http://elton.io)
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License. 

#endregion

using Elton.Nest.Rest.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Elton.Nest.Rest
{
    public class RestStreamClient : StreamingClient, IDisposable
    {
        static readonly Common.Logging.ILog log = Common.Logging.LogManager.GetLogger(typeof(RestStreamClient));

        const long DEFAULT_BYTE_COUNT = 2048L;

        private string token = null;
        bool started = false;
        readonly string apiUrl;
        readonly HttpClient httpClient;
        readonly RestConfig restConfig;
        readonly Parser parser;
        readonly RetryExecutor retryExecutor;
        readonly ExceptionHandler exceptionHandler;

        CancellationTokenSource cancellationTokenSource = null;
        public RestStreamClient(HttpClient httpClient, RestConfig restConfig, Parser parser,
            BackOff backOff = default, ExceptionHandler exceptionHandler = null)
        {
            this.httpClient = httpClient;
            this.restConfig = restConfig;
            this.parser = parser;
            this.retryExecutor = new RetryExecutor(backOff??new FibonacciBackOff.Builder().build());
            this.exceptionHandler = exceptionHandler;

            this.apiUrl = restConfig.GetUrl();
        }
        
        public bool Start(string accessToken)
        {
            if (started)
                return false;
            if (string.IsNullOrEmpty(accessToken))
                throw new MissingTokenException();

            token = accessToken;
            cancellationTokenSource = new CancellationTokenSource();
            var executorTask = Task.Factory.StartNew(
                () => { new RestStreamClient.Reader(this).run(); },
                cancellationTokenSource.Token);

            started = true;
            return true;
        }

        public void Stop()
        {
            started = false;
            retryExecutor.Reset();
            retryExecutor.cancel();

            cancellationTokenSource.Cancel();
            httpClient.CancelPendingRequests(); //httpClient.dispatcher().cancelAll();
        }

        public void Dispose()
        {
            Stop();
            httpClient?.Dispose();
        }

        private class Reader
        {
            readonly RestStreamClient owner;
            public Reader(RestStreamClient owner)
            {
                this.owner = owner;
            }

            private string accumulator = "";
            private string segment(string buf)
            {
                if (buf.EndsWith("\n") || buf.EndsWith("}"))
                {
                    string msg = accumulator + buf;
                    accumulator = "";
                    return msg;
                }
                else accumulator = accumulator + buf;

                return null;
            }

            public void run()
            {

                var request = new HttpRequestMessage(HttpMethod.Get,
                    requestUri: owner.apiUrl + "?auth=" + owner.token);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/event-stream"));//addHeader("Accept", "text/event-stream")

                HttpResponseMessage response = null;
                try
                {
                    response = owner.httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false).GetAwaiter().GetResult();
                    response.EnsureSuccessStatusCode();

                    var buffer = new byte[DEFAULT_BYTE_COUNT];

                    using (response)
                    using (var content = response.Content.ReadAsStreamAsync().ConfigureAwait(false).GetAwaiter().GetResult())
                    {
                        while (true) //while (!response.body().source().exhausted()) {
                        {
                            owner.cancellationTokenSource.Token.ThrowIfCancellationRequested();

                            // TODO: it really sucks that this does not take a cancellation token
                            var count = content.Read(buffer, 0, buffer.Length);
                            if (count < 1)
                                break;

                            var msg = segment(Encoding.UTF8.GetString(buffer, 0, count));
                            if (msg != null)
                            {
                                try
                                {
                                    owner.parser.Parse(msg);
                                }
                                catch (ParserException ex)
                                {
                                    owner.exceptionHandler?.Invoke(ex);
                                }
                                catch (Exception ex)
                                {
                                    //notify client and ignore downstream exceptions
                                    owner.exceptionHandler?.Invoke(new NestException(ex));
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    owner.exceptionHandler?.Invoke(new NestException(ex));
                }
                finally
                {

                    if (response != null)
                        response.Dispose();

                    if (owner.started)
                    {
                        owner.retryExecutor.schedule(
                            (accessToken) => { owner.Start(accessToken); },
                            owner.token);
                    }
                }
            }
        }
    }
}