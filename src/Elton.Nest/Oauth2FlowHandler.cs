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
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;

namespace Elton.Nest
{
    public class Oauth2FlowHandler
    {
        const string KEY_ACCESS_TOKEN = "access_token_key";
        const string REVOKE_TOKEN_PATH = "oauth2/access_tokens/";
        const string KEY_CLIENT_METADATA = "client_metadata_key";

        NestConfig oauth2Config;
        readonly HttpClient httpClient;

        public Oauth2FlowHandler(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        /// <summary>
        /// Sets the Nest configuration values used for authentication.
        ///
        /// @param clientId     The Nest client ID.
        /// @param clientSecret The Nest client secret.
        /// @param redirectUrl  The Nest redirect URL.
        /// </summary>
        public void setConfig(String clientId, String clientSecret, String redirectUrl)
        {
            oauth2Config = new NestConfig.Builder().clientID(clientId)
                    .clientSecret(clientSecret)
                    .redirectURL(redirectUrl)
                    .build();
        }

        /// <summary>
        /// Returns a {@link NestConfig} object containing the currently set credentials. If there are no
        /// credentials set, returns null.
        ///
        /// @return a {@link NestConfig} object containing current config values, or null if unset.
        /// </summary>
        public NestConfig Config => oauth2Config;

        /// <summary>
        /// Clears the currently stored credentials.
        /// </summary>
        public void clearConfig()
        {
            oauth2Config = null;
        }

        /// <summary>
        /// Start an {@link Activity} which will guide a user through the authentication process.
        ///
        /// @param activity    the {@link Activity} return the result. Typically the current {@link
        ///                    Activity}.
        /// @param requestCode the request code for which a result will be returned.
        /// </summary>
        //public void launchAuthFlow(Activity activity, int requestCode)
        //{
        //    final Intent authFlowIntent = new Intent(activity, NestAuthActivity.class);
        //    authFlowIntent.putExtra(KEY_CLIENT_METADATA, oauth2Config);
        //    activity.startActivityForResult(authFlowIntent, requestCode);
        //}

        /// <summary>
        /// Returns a {@link NestToken} embedded in the {@link Intent} that is returned in the result
        /// from {@link #launchAuthFlow(Activity, int)}.
        ///
        /// @param intent the intent to retrieve the NestToken from.
        /// @return the {@link NestToken}, if it was contained in the {@link Intent}, otherwise null.
        /// </summary>
        //public NestToken getAccessTokenFromIntent(Intent intent)
        //{
        //    return intent.getParcelableExtra(KEY_ACCESS_TOKEN);
        //}

        /// <summary>
        /// Revokes a {@link NestToken} from the Nest API.
        ///
        /// @param token    The token to revoke.
        /// @param callback A callback for the result of the revocation.
        /// </summary>
        public void revokeToken(NestToken token, Callback callback)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
                requestUri: NestApiUrls.AUTHORIZATION_SERVER_URL + REVOKE_TOKEN_PATH + token.Token);
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

        /// <summary>
        /// Use the code to get an access token.
        /// </summary>
        public NestToken CreateToken(string mCode)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                requestUri: NestApiUrls.GetAccessUrl(oauth2Config.ClientID, oauth2Config.ClientSecret, mCode));
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
    }
}