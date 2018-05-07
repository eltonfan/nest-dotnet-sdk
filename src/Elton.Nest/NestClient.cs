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
    public class NestClient
    {
        static readonly Common.Logging.ILog log = Common.Logging.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        readonly Rest.RestClient restClient;
        readonly StreamingClient streamingClient;
        readonly Notifier notifier = new Notifier();

        readonly CameraSetter cameras;
        readonly ThermostatSetter thermostats;
        readonly StructureSetter structures;
        public readonly Oauth2FlowHandler oauth2;

        /// <summary>
        /// Creates a new instance of the <see cref="NestClient"/>.
        /// </summary>
        public NestClient() : this(new RestConfig()) { }

        /// <summary>
        /// Creates a new instance of the <see cref="NestClient"/>.
        /// </summary>
        public NestClient(RestConfig restConfig)
        {
            var messageParser = new MessageParser(new ObjectModelMapper(notifier));

            var httpClient = new HttpClient();
            var streamingHttpClient = new HttpClient(new HttpClientHandler { AllowAutoRedirect = true, MaxAutomaticRedirections = 10, }, true);
            //.connectTimeout(10, TimeUnit.SECONDS)
            streamingHttpClient.Timeout = TimeSpan.FromSeconds(60);//.readTimeout(60, TimeUnit.SECONDS)

            oauth2 = new Oauth2FlowHandler(httpClient);
            restClient = new RestClient(httpClient, restConfig, messageParser);
            streamingClient = new RestStreamClient.Builder(streamingHttpClient, restConfig, messageParser)
                    .setExceptionHandler((exception) => { Error?.Invoke(exception); })
                    .build();

            cameras = new CameraSetter(restClient);
            structures = new StructureSetter(restClient);
            thermostats = new ThermostatSetter(restClient);
        }

        /// <summary>
        /// Requests authentication with a <see cref="NestToken"/>.
        /// </summary>
        /// <param name="token">the NestToken to authenticate with</param>
        public void startWithToken(NestToken token)
        {
            startWithToken(token.Token);
        }

        /// <summary>
        /// Requests authentication with a raw token.
        /// </summary>
        /// <param name="token">the token string to authenticate with</param>
        public void startWithToken(string token)
        {
            restClient.SetToken(token);
            streamingClient.Start(token);
        }

        public void stop()
        {
            streamingClient.Stop();
        }

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
                    var assembly = Assembly.GetExecutingAssembly();
                    var attrTitle = Attribute.GetCustomAttribute(assembly, typeof(AssemblyTitleAttribute)) as AssemblyTitleAttribute;
                    var attrCopyright = Attribute.GetCustomAttribute(assembly, typeof(AssemblyCopyrightAttribute)) as AssemblyCopyrightAttribute;
                    var attrFileVersion = Attribute.GetCustomAttribute(assembly, typeof(AssemblyFileVersionAttribute)) as AssemblyFileVersionAttribute;

                    //Print library version
                    log.InfoFormat("{0} v{1} ({2})", attrTitle.Title, sdkVersion, Environment.MachineName);
                    log.Info(attrCopyright.Copyright.Replace("\u00A9", "(C)"));

                    if (!Version.TryParse(attrFileVersion.Version, out sdkVersion))
                        sdkVersion = assembly.GetName().Version;
                }

                return sdkVersion;
            }
        }

        public event ExceptionHandler Error;
    }
}