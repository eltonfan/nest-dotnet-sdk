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

using System;
using System.Collections.Generic;
using System.Text;

namespace Elton.Nest.Rest
{
    public class RestConfig
    {
        public string Protocol { get; set; }

        public string Host { get; set; }

        public string Port { get; set; }

        /// <summary>
        /// Creates instance of RestCofig with default configuration.
        /// </summary>
        public RestConfig()
            : this(NestApiUrls.DEFAULT_PROTOCOL, NestApiUrls.DEFAULT_WWN_URL, NestApiUrls.DEFAULT_PORT)
        {
        }

        public RestConfig(string protocol, string host, string port)
        {
            this.Protocol = protocol;
            this.Host = host;
            this.Port = port;
        }

        public string GetUrl()
        {
            return Protocol + "://" + Host + (string.IsNullOrEmpty(Port) ? "" : ":" + Port);
        }
    }
}