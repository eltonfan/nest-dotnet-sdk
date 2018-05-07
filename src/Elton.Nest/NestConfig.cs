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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text;

namespace Elton.Nest
{
    /// <summary>
    /// NestConfig holds the various configuration values needed to launch the OAuth 2.0 flow.
    /// </summary>
    public sealed class NestConfig : IEquatable<NestConfig>
    {
        public const string KEY_CLIENT_ID = "client_id";
        public const string KEY_CLIENT_SECRET = "client_secret";
        public const string KEY_REDIRECT_URL = "redirect_url";
        public const string KEY_STATE_VALUE = "state_value";

        private string clientId;
        private string clientSecret;
        private string stateValue;
        private string redirectUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="NestConfig" /> class.
        /// </summary>
        private NestConfig(Builder builder)
        {
            this.clientId = builder.ClientId;
            this.stateValue = builder.State;
            this.clientSecret = builder.ClientSecret;
            this.redirectUrl = builder.RedirectUrl;
        }

        /// <summary>
        /// Returns the client id.
        /// </summary>
        /// <value>the client id.</value>
        public string ClientId => clientId;

        /// <summary>
        /// Returns the state value. This is randomly generated for each <see cref="NestConfig"/>.
        /// </summary>
        /// <value>the state value.</value>
        public string StateValue => stateValue;

        /// <summary>
        /// Returns the client secret. Keep this secret safe.
        /// </summary>
        /// <value>the client secret.</value>
        public string ClientSecret => clientSecret;

        /// <summary>
        /// Returns the redirect URL. Must match the redirect URL set in the Nest developer portal.
        /// </summary>
        /// <value>the redirect URL.</value>
        public string RedirectUrl => redirectUrl;

        public override string ToString()
        {
            try
            {
                var json = new JObject();
                json.Add(KEY_CLIENT_ID, clientId);
                json.Add(KEY_CLIENT_SECRET, clientSecret);
                json.Add(KEY_REDIRECT_URL, redirectUrl);
                json.Add(KEY_STATE_VALUE, stateValue);

                return json.ToString();
            }
            catch (Exception e)
            {
                return base.ToString();
            }
        }

        public override int GetHashCode()
        {
            return Utils.GetHashCode(this);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as NestConfig);
        }

        public bool Equals(NestConfig other)
        {
            return Utils.AreEqual(this, other);
        }


        /// <summary>
        /// Builder for creating <see cref="NestConfig"/> objects.
        /// </summary>
        public class Builder
        {
            string clientId;
            string clientSecret;
            string redirectUrl;
            string stateValue;

            public string ClientId => clientId;
            public string ClientSecret => clientSecret;
            public string RedirectUrl => redirectUrl;
            public string State => stateValue;

            /// <summary>
            /// Sets the client id.
            /// </summary>
            /// <param name="id">the client id.</param>
            /// <returns>the <see cref="Builder"/> instance.</returns>
            public Builder SetClientId(string id)
            {
                clientId = id;
                return this;
            }

            /// <summary>
            /// Sets the client secret.
            /// </summary>
            /// <param name="secret">the client secret.</param>
            /// <returns>the <see cref="Builder"/> instance.</returns>
            public Builder SetClientSecret(string secret)
            {
                clientSecret = secret;
                return this;
            }

            /// <summary>
            /// Sets the redirect URL. Must match the redirect URL set in the Nest developer portal.
            /// </summary>
            /// <param name="url">the redirect url.</param>
            /// <returns>the <see cref="Builder"/> instance.</returns>
            public Builder SetRedirectUrl(string url)
            {
                redirectUrl = url;
                return this;
            }

            /// <summary>
            /// Sets the state value. This method be called directly, as the state value is automatically
            /// and randomly generated when <see cref="Builder#build()"/> is called.
            /// </summary>
            /// <param name="state">the state value.</param>
            /// <returns>the <see cref="Builder"/> instance.</returns>
            private Builder SetState(string state)
            {
                this.stateValue = state;
                return this;
            }

            public Builder FromJsonString(string jsonString)
            {
                dynamic config = new
                {
                    client_id = "",
                    client_secret = "",
                    redirect_url = "",
                    state_value = "",
                };

                config = JsonConvert.DeserializeAnonymousType(jsonString, config);
                this.SetClientId(config.client_id)
                    .clientSecret(config.client_secret)
                    .redirectURL(config.redirect_url);
                    //.SetStateValue(config.state_value);

                return this;
            }

            /// <summary>
            /// Builds and returns the new <see cref="NestConfig"/> object.
            /// </summary>
            /// <returns>the new <see cref="NestConfig"/> object.</returns>
            public NestConfig Build()
            {
                // Create random state value on each creation
                SetState($"app-state" + Stopwatch.GetTimestamp() + "-" + random.Next());
                return new NestConfig(this);
            }

            readonly static Random random = new Random();
        }
    }
}