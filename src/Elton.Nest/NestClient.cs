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

using Elton.Nest.Models;
using Elton.Nest.Rest;
using Elton.Nest.Rest.Parsers;
using Elton.Nest.Setters;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Elton.Nest
{
    public class NestClient
    {
        static readonly Common.Logging.ILog log = Common.Logging.LogManager.GetLogger(typeof(NestClient));

        readonly Rest.RestClient restClient;
        readonly StreamingClient streamingClient;
        readonly Notifier notifier = new Notifier();

        readonly CameraSetter cameras;
        readonly ThermostatSetter thermostats;
        readonly StructureSetter structures;
        //public readonly Oauth2FlowHandler oauth2;

        class EmptyExceptionHandler : ExceptionHandler
        {
            public void handle(NestException value) { }
        }
        /// <summary>
        /// Creates a new instance of the {@link NestClient}.
        /// </summary>

        public NestClient() : this(new RestConfig(), new EmptyExceptionHandler()) { }

        public NestClient(ExceptionHandler exceptionHandler) : this(new RestConfig(), exceptionHandler) { }


        /// <summary>
        /// Creates a new instance of the {@link NestClient}.
        /// </summary>
        public NestClient(RestConfig restConfig, ExceptionHandler exceptionHandler)
        {
            var messageParser = new MessageParser(new ObjectModelMapper(notifier));

            var httpClient = new HttpClient();
            var streamingHttpClient = new HttpClient(new HttpClientHandler { AllowAutoRedirect = true, MaxAutomaticRedirections = 10, }, true);
            //.connectTimeout(10, TimeUnit.SECONDS)
            streamingHttpClient.Timeout = TimeSpan.FromSeconds(60);//.readTimeout(60, TimeUnit.SECONDS)

            //oauth2 = new Oauth2FlowHandler(httpClient);
            restClient = new RestClient(httpClient, restConfig, messageParser);
            streamingClient = new RestStreamClient.Builder(streamingHttpClient, restConfig, messageParser)
                    .setExceptionHandler(exceptionHandler)
                    .build();

            cameras = new CameraSetter(restClient);
            structures = new StructureSetter(restClient);
            thermostats = new ThermostatSetter(restClient);
        }

        /// <summary>
        /// Requests authentication with a {@link NestToken}.
        ///
        /// @param token    the NestToken to authenticate with
        /// </summary>
        public void startWithToken(NestToken token)
        {
            startWithToken(token.Token);
        }

        /// <summary>
        /// Requests authentication with a raw token.
        ///
        /// @param token        the token string to authenticate with
        /// </summary>
        public void startWithToken(string token)
        {
            restClient.setToken(token);
            streamingClient.start(token);
        }

        public void stop()
        {
            notifier.removeAllListeners();
            streamingClient.stop();
        }

        public void addListener(NestListener listener)
        {
            notifier.addListener(listener);
        }

        public void removeListener(NestListener listener)
        {
            notifier.removeListener(listener);
        }

        public void removeAllListeners()
        {
            notifier.removeAllListeners();
        }

        public CameraSetter Cameras => cameras;
        public ThermostatSetter Thermostats => thermostats;
        public StructureSetter Structures => structures;
    }
}