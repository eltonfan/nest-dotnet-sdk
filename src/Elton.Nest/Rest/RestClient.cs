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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Elton.Nest.Rest
{
    public class RestClient : IDisposable
    {
        readonly string baseApiUrl;
        string redirectApiUrl = null;
        string token = null;
        readonly Parser parser;
        readonly HttpClient httpClient;
        static readonly Callback callbackStub = new Callback();

        public RestClient(HttpClient httpClient, string baseApiUrl, Parser parser)
        {
            this.httpClient = httpClient;
            this.parser = parser;
            this.baseApiUrl = baseApiUrl;
        }

        public void SetToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new MissingTokenException();
            this.token = token;
        }

        public void WriteLong(string path, string field, long value, Callback callback)
        {
            Write(path, field, value.ToString(), callback);
        }

        public void WriteDouble(string path, string field, double value, Callback callback)
        {
            Write(path, field, value.ToString(), callback);
        }

        public void WriteString(string path, string field, string value, Callback callback)
        {
            Write(path, field, "\"" + value + "\"", callback);
        }

        public void WriteBoolean(string path, string field, bool value, Callback callback)
        {
            Write(path, field, value.ToString(), callback);
        }

        private void Write(string path, string field, string value, Callback callback)
        {
            if (string.IsNullOrEmpty(token))
                throw new MissingTokenException();

            var internalCallback = callback ?? callbackStub;

            var apiUrl = redirectApiUrl ?? baseApiUrl;
            var body = "{\"" + field + "\": " + value + "}";

            var request = new HttpRequestMessage(HttpMethod.Put,
                requestUri: apiUrl + path);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Authorization", "Bearer " + token);
            request.Content = new StringContent(body, Encoding.UTF8, "application/json");//"application/json; charset=utf-8"

            try
            {
                //Note: HttpClient auto redirect if 307
                using (var response = httpClient.SendAsync(request).ConfigureAwait(false).GetAwaiter().GetResult())
                {
                    //response.EnsureSuccessStatusCode();
                    var statusCode = (int)response.StatusCode;
                    if (statusCode < 200 || statusCode >= 500)
                    {
                        internalCallback.OnFailure?.Invoke(new ServerException($"Unexpected server response. Error code: {statusCode}"));
                    }
                    else if (statusCode == 404)
                    {//UnknownHostException
                     //Reset redirect url if WWN host goes offline
                        redirectApiUrl = null;
                        Write(path, field, value, callback);
                        return;
                    }
                    else
                    {
                        //Save redirect url
                        redirectApiUrl = response.RequestMessage.RequestUri.ToString();
                        try
                        {
                            //UTF-8
                            var responseBody = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                            parser.Parse(responseBody);
                            internalCallback.OnSuccess();
                        }
                        catch (ParserException ex)
                        {
                            internalCallback.OnFailure?.Invoke(ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                internalCallback.OnFailure?.Invoke(new NestException("Write request failed.", ex));
            }
        }

        public void Dispose()
        {
            httpClient?.Dispose();
        }
    }
}