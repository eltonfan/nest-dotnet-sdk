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
using Elton.Nest.Rest.Parsers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;

namespace Elton.Nest
{
    public class NestOauth2 : IDisposable
    {
        internal const string BASE_AUTHORIZATION_URL = "https://home.nest.com/";
        internal const string AUTHORIZATION_SERVER_URL = "https://api.home.nest.com/";
        internal const string ACCESS_URL = AUTHORIZATION_SERVER_URL
            + "oauth2/access_token?code=%s&client_id=%s&client_secret=%s"
            + "&grant_type=authorization_code";

        internal const string CLIENT_CODE_URL = BASE_AUTHORIZATION_URL
                + "login/oauth2?client_id=%s&state=%s";

        const string KEY_ACCESS_TOKEN = "access_token_key";
        const string REVOKE_TOKEN_PATH = "oauth2/access_tokens/";
        const string KEY_CLIENT_METADATA = "client_metadata_key";

        const string QUERY_PARAM_CODE = "code";

        NestConfig oauth2Config;
        readonly HttpClient httpClient;

        public NestOauth2(NestConfig config)
        {
            this.httpClient = new HttpClient();
            this.oauth2Config = config;
        }
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
        }
        /// <summary>
        /// Clears the currently stored credentials.
        /// </summary>
        public void ClearConfig()
        {
            oauth2Config = null;
        }

        public string GetClientCodeUrl(string redirectUri = null)
        {
            string clientId = oauth2Config.ClientId;
            string state = oauth2Config.StateValue;
            redirectUri = redirectUri ?? oauth2Config.RedirectUrl;

            return BASE_AUTHORIZATION_URL
                + $"login/oauth2?client_id={clientId}&state={state}"
                + (redirectUri == null ? "" : "&redirect_uri=" + System.Net.WebUtility.UrlEncode(redirectUri))
                + "&grant_type=authorization_code";
        }

        private string GetAccessUrl(string code)
        {
            string clientId = oauth2Config.ClientId;
            string clientSecret = oauth2Config.ClientSecret;

            return AUTHORIZATION_SERVER_URL
                + $"oauth2/access_token?code={code}&client_id={clientId}&client_secret={clientSecret}"
                + "&grant_type=authorization_code";
        }

        /// <summary>
        /// Returns a <see cref="NestConfig"/> object containing the currently set credentials. If there are no
        /// credentials set, returns null.
        /// </summary>
        /// <value">a <see cref="NestConfig"/> object containing current config values, or null if unset.</value>
        public NestConfig Config => oauth2Config;

        /// <summary>
        /// Start an <see cref="Activity"/> which will guide a user through the authentication process.
        /// </summary>
        /// <param name="activity">the <see cref="Activity"/> return the result. Typically the current <see cref="Activity"/>.</param>
        /// <param name="requestCode">the request code for which a result will be returned.</param>
        //public void launchAuthFlow(Activity activity, int requestCode)
        //{
        //    final Intent authFlowIntent = new Intent(activity, NestAuthActivity.class);
        //    authFlowIntent.putExtra(KEY_CLIENT_METADATA, oauth2Config);
        //    activity.startActivityForResult(authFlowIntent, requestCode);
        //}

        /// <summary>
        /// Returns a <see cref="NestToken"/> embedded in the <see cref="Intent"/> that is returned in the result
        /// from <see cref="#launchAuthFlow(Activity, int)"/>.
        /// </summary>
        /// <param name="intent">the intent to retrieve the NestToken from.</param>
        /// <value">the <see cref="NestToken"/>, if it was contained in the <see cref="Intent"/>, otherwise null.</value>
        //public NestToken getAccessTokenFromIntent(Intent intent)
        //{
        //    return intent.getParcelableExtra(KEY_ACCESS_TOKEN);
        //}

        /// <summary>
        /// Revokes a <see cref="NestToken"/> from the Nest API.
        /// </summary>
        /// <param name="token">The token to revoke.</param>
        /// <param name="callback">A callback for the result of the revocation.</param>
        public void RevokeToken(NestToken token, Callback callback)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
                requestUri: AUTHORIZATION_SERVER_URL + REVOKE_TOKEN_PATH + token.Token);
            HttpResponseMessage response = null;
            try
            {
                response = httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false).GetAwaiter().GetResult();
                //response.EnsureSuccessStatusCode();

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    callback.OnFailure?.Invoke(
                            new ServerException("Token revocation failed: " + response.StatusCode));
                }
                else callback.OnSuccess();
            }
            catch (Exception ex)
            {
                callback.OnFailure?.Invoke(new NestException("Request to revoke token failed.", ex));
            }
            finally
            {
                if (response != null)
                    response.Dispose();
            }
        }

        public static Dictionary<string, string> ParseQueryString(string requestQueryString)
        {
            Dictionary<string, string> rc = new Dictionary<string, string>();
            string[] ar1 = requestQueryString.Split(new char[] { '&', '?' });
            foreach (string row in ar1)
            {
                if (string.IsNullOrEmpty(row)) continue;
                int index = row.IndexOf('=');
                if (index < 0) continue;
                rc[Uri.UnescapeDataString(row.Substring(0, index))] = Uri.UnescapeDataString(row.Substring(index + 1)); // use Unescape only parts          
            }
            return rc;
        }

        /// <summary>
        /// Get the authorization code from the resulting URL.
        /// </summary>
        /// <param name="resultingUrl">The resulting URL.</param>
        /// <returns>the authorization code</returns>
        public string ParseAuthorizationCode(string resultingUrl)
        {
            if (!resultingUrl.StartsWith(oauth2Config.RedirectUrl))
                throw new FormatException("resultingUrl do not match RedirectUrl");

            var dicQuery = ParseQueryString(new Uri(resultingUrl).Query);
            if(dicQuery.TryGetValue("error", out string error) && !string.IsNullOrEmpty(error))
            {
                dicQuery.TryGetValue("error_description", out string desc);
                throw new FormatException($"{error}: {desc}");
            }
            if (!dicQuery.TryGetValue(QUERY_PARAM_CODE, out string authorizationCode) || string.IsNullOrEmpty(authorizationCode))
                throw new FormatException("The code query is empty.");

            if (!dicQuery.TryGetValue("state", out string state))
                throw new FormatException("The state query is empty.");

            if (state != oauth2Config.StateValue)
            {// the state values do not match
                //throw a 401 Unauthorized HTTP error code
                throw new FormatException("the state values do not match");
            }

            return authorizationCode;
        }

        /// <summary>
        /// Use the code to get an access token.
        /// </summary>
        public NestToken CreateToken(string mCode)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                requestUri: GetAccessUrl(mCode));
            request.Content = new StringContent("", Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;

            try
            {
                response = httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false).GetAwaiter().GetResult();
                //response.EnsureSuccessStatusCode();
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    throw new ServerException("Token revocation failed: " + response.StatusCode);

                var body = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                try
                {
                    return JsonConvert.DeserializeObject<NestToken>(body);
                }
                catch (Exception ex)
                {
                    throw new ParserException("Failed to parse response for token.", ex);
                }
            }
            catch (Exception ex)
            {
                //Log.e(TAG, "Failed to get token.", e);
                throw new NestException("Request to revoke token failed.", ex);
            }
            finally
            {
                if (response != null)
                    response.Dispose();
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            httpClient?.Dispose();
        }
    }
}