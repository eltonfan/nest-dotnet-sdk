#region License

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
    public class RestClient
    {
        readonly string baseApiUrl;
        string redirectApiUrl = null;
        string token = null;
        readonly Parser parser;
        readonly HttpClient httpClient;
        //static readonly MediaType JSON = MediaType.parse("application/json; charset=utf-8");
        const string JSON = "application/json";
        static readonly Callback callbackStub = new Callback();

        public RestClient(HttpClient httpClient, RestConfig restConfig, Parser parser)
        {
            this.httpClient = httpClient;
            this.parser = parser;
            this.baseApiUrl = restConfig.getUrl();
        }

        public void setToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new MissingTokenException();
            this.token = token;
        }

        public void writeLong(string path, string field, long value, Callback callback)
        {
            write(path, field, value.ToString(), callback);
        }

        public void writeDouble(string path, string field, double value, Callback callback)
        {
            write(path, field, value.ToString(), callback);
        }

        public void writeString(string path, string field, string value, Callback callback)
        {
            write(path, field, "\"" + value + "\"", callback);
        }

        public void writeBoolean(string path, string field, bool value, Callback callback)
        {
            write(path, field, value.ToString(), callback);
        }

        private void write(string path, string field, string value, Callback callback)
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
            request.Content = new StringContent(body, Encoding.UTF8, "application/json");

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
                    else
                    {
                        try
                        {
                            //UTF-8
                            var responseBody = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                            parser.parse(responseBody);
                            internalCallback.OnSuccess();
                        }
                        catch (ParserException ex)
                        {
                            internalCallback.OnFailure?.Invoke(ex);
                        }
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                if ((ex.InnerException is WebException webException) &&
                    (webException.Status == WebExceptionStatus.NameResolutionFailure))
                {//UnknownHostException
                    //Reset redirect url if WWN host goes offline
                    redirectApiUrl = null;
                    write(path, field, value, callback);
                    return;
                }
                internalCallback.OnFailure?.Invoke(new NestException("Write request failed.", ex));
            }
            catch (Exception ex)
            {
                internalCallback.OnFailure?.Invoke(new NestException("Write request failed.", ex));
            }
        }
    }
}