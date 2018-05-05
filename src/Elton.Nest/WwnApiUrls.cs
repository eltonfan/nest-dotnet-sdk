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

namespace Elton.Nest
{
    public class WwnApiUrls
    {
        public const string DEFAULT_PROTOCOL = "https";
        public const string DEFAULT_WWN_URL = "developer-api.nest.com";
        public const string DEFAULT_PORT = "";
        internal const string BASE_AUTHORIZATION_URL = "https://home.nest.com/";
        internal const string AUTHORIZATION_SERVER_URL = "https://api.home.nest.com/";
        internal const string ACCESS_URL = AUTHORIZATION_SERVER_URL
            + "oauth2/access_token?code=%s&client_id=%s&client_secret=%s"
            + "&grant_type=authorization_code";

        internal const string CLIENT_CODE_URL = BASE_AUTHORIZATION_URL
                + "login/oauth2?client_id=%s&state=%s";

        public static string GetAccessUrl(string clientId, string clientSecret, string code)
        {
            return AUTHORIZATION_SERVER_URL
                + $"oauth2/access_token?code={code}&client_id={clientId}&client_secret={clientSecret}"
                + "&grant_type=authorization_code";
        }
    }
}
