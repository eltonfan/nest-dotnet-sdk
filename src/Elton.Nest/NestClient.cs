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
using System.Reflection;
using System.Text;

namespace Elton.Nest
{
    public class NestClient : IDisposable
    {
        static readonly Common.Logging.ILog log = Common.Logging.LogManager.GetLogger(typeof(NestClient));

        readonly Rest.RestClient restClient;
        readonly StreamingClient streamingClient;
        readonly Notifier notifier = new Notifier();

        readonly CameraSetter cameras;
        readonly ThermostatSetter thermostats;
        readonly StructureSetter structures;
        readonly Oauth2FlowHandler oauth2;

        /// <summary>
        /// Creates a new instance of the <see cref="NestClient"/>.
        /// </summary>
        public NestClient(NestConfig config) : this(config, new RestConfig()) { }

        /// <summary>
        /// Creates a new instance of the <see cref="NestClient"/>.
        /// </summary>
        public NestClient(NestConfig config, RestConfig restConfig)
        {
            var messageParser = new MessageParser(new ObjectModelMapper(notifier));

            var httpClient = new HttpClient();
            var streamingHttpClient = new HttpClient(new HttpClientHandler { AllowAutoRedirect = true, MaxAutomaticRedirections = 10, }, true);
            //.connectTimeout(10, TimeUnit.SECONDS)
            streamingHttpClient.Timeout = TimeSpan.FromSeconds(60);//.readTimeout(60, TimeUnit.SECONDS)

            oauth2 = new Oauth2FlowHandler(httpClient);
            restClient = new RestClient(httpClient, restConfig.GetUrl(), messageParser);
            streamingClient = new RestStreamClient(streamingHttpClient, restConfig, messageParser,
                exceptionHandler: exception => { StreamingError?.Invoke(exception); });

            cameras = new CameraSetter(restClient);
            structures = new StructureSetter(restClient);
            thermostats = new ThermostatSetter(restClient);

            SetConfig(config.ClientId, config.ClientSecret, config.RedirectUrl);
        }

        /// <summary>
        /// Requests authentication with a <see cref="NestToken"/>.
        /// </summary>
        /// <param name="token">the NestToken to authenticate with</param>
        public void StartWithToken(NestToken token)
        {
            StartWithToken(token.Token);
        }

        /// <summary>
        /// Requests authentication with a raw token.
        /// </summary>
        /// <param name="token">the token string to authenticate with</param>
        public void StartWithToken(string token)
        {
            restClient.SetToken(token);
            streamingClient.Start(token);
        }

        public void Stop()
        {
            streamingClient.Stop();
        }

        NestConfig oauth2Config;
        /// <summary>
        /// Sets the Nest configuration values used for authentication.
        /// </summary>
        /// <param name="clientId">The Nest client ID.</param>
        /// <param name="clientSecret">The Nest client secret.</param>
        /// <param name="redirectUrl">The Nest redirect URL.</param>
        public void SetConfig(string clientId, string clientSecret, string redirectUrl)
        {
            this.oauth2Config = new NestConfig.Builder()
                .SetClientId(clientId)
                .SetClientSecret(clientSecret)
                .SetRedirectUrl(redirectUrl)
                .Build();

            oauth2.Config = oauth2Config;
        }
        
        /// <summary>
        /// Clears the currently stored credentials.
        /// </summary>
        public void ClearConfig()
        {
            oauth2.Config = null;
        }

        public NestToken CreateToken(string mCode)
        {
            return oauth2.CreateToken(mCode);
        }

        public void RevokeToken(NestToken token, Callback callback)
        {
            oauth2.RevokeToken(token, callback);
        }

        public void Dispose()
        {
            restClient?.Dispose();
            streamingClient?.Dispose();
            oauth2?.Dispose();
        }

        /// <summary>
        /// Returns a <see cref="NestConfig"/> object containing the currently set credentials. If there are no
        /// credentials set, returns null.
        /// </summary>
        /// <value">a <see cref="NestConfig"/> object containing current config values, or null if unset.</value>
        public NestConfig Config => oauth2Config;

        public CameraSetter Cameras => cameras;
        public ThermostatSetter Thermostats => thermostats;
        public StructureSetter Structures => structures;

        public Notifier Notifier => notifier;

        static Version sdkVersion = null;
        public static Version SdkVersion
        {
            get
            {
                if (sdkVersion == null)
                {
                    var assembly = typeof(NestClient).GetTypeInfo().Assembly;
                    var title = assembly.GetCustomAttribute<AssemblyTitleAttribute>()?.Title;
                    var copyright = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright;
                    var fileVersion = assembly.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version;

                    if (!Version.TryParse(fileVersion, out sdkVersion))
                        sdkVersion = assembly.GetName().Version;

                    //Print library version
                    log.Debug($"{title} v{sdkVersion} {copyright}");
                }

                return sdkVersion;
            }
        }

        public event ExceptionHandler StreamingError;
    }
}