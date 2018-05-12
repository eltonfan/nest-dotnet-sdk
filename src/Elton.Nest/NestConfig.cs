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

        readonly static Random random = new Random();

        /// <summary>
        /// Initializes a new instance of the <see cref="NestConfig" /> class.
        /// </summary>
        public NestConfig(string clientId = default, string clientSecret = default, string redirectUrl = default, string state = null)
        {
            this.ClientId = clientId;
            this.ClientSecret = clientSecret;
            this.RedirectUrl = redirectUrl;

            if (state == null)
                this.StateValue = $"app-state" + Stopwatch.GetTimestamp() + "-" + random.Next();
            else
                this.StateValue = state;
        }

        public static NestConfig FromJson(string jsonString)
        {
            dynamic config = new
            {
                client_id = "",
                client_secret = "",
                redirect_url = "",
                state_value = "",
            };

            config = JsonConvert.DeserializeAnonymousType(jsonString, config);

            return new NestConfig(
                (string)config.client_id,
                config.client_secret,
                (string)config.redirect_url);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NestConfig" /> class.
        /// </summary>
        private NestConfig(Builder builder)
        {
            this.ClientId = builder.ClientId;
            this.StateValue = builder.State;
            this.ClientSecret = builder.ClientSecret;
            this.RedirectUrl = builder.RedirectUrl;
        }

        /// <summary>
        /// Returns the client id.
        /// </summary>
        /// <value>the client id.</value>
        public string ClientId { get; private set; }

        /// <summary>
        /// Returns the state value. This is randomly generated for each <see cref="NestConfig"/>.
        /// </summary>
        /// <value>the state value.</value>
        public string StateValue { get; private set; }

        /// <summary>
        /// Returns the client secret. Keep this secret safe.
        /// </summary>
        /// <value>the client secret.</value>
        public string ClientSecret { get; private set; }

        /// <summary>
        /// Returns the redirect URL. Must match the redirect URL set in the Nest developer portal.
        /// </summary>
        /// <value>the redirect URL.</value>
        public string RedirectUrl { get; private set; }

        public override string ToString()
        {
            try
            {
                var json = new JObject();
                json.Add(KEY_CLIENT_ID, ClientId);
                json.Add(KEY_CLIENT_SECRET, ClientSecret);
                json.Add(KEY_REDIRECT_URL, RedirectUrl);
                json.Add(KEY_STATE_VALUE, StateValue);

                return json.ToString();
            }
            catch (Exception)
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
            public string ClientId { get; private set; }
            public string ClientSecret { get; private set; }
            public string RedirectUrl { get; private set; }
            public string State { get; private set; }

            /// <summary>
            /// Sets the client id.
            /// </summary>
            /// <param name="id">the client id.</param>
            /// <returns>the <see cref="Builder"/> instance.</returns>
            public Builder SetClientId(string id)
            {
                ClientId = id;
                return this;
            }

            /// <summary>
            /// Sets the client secret.
            /// </summary>
            /// <param name="secret">the client secret.</param>
            /// <returns>the <see cref="Builder"/> instance.</returns>
            public Builder SetClientSecret(string secret)
            {
                ClientSecret = secret;
                return this;
            }

            /// <summary>
            /// Sets the redirect URL. Must match the redirect URL set in the Nest developer portal.
            /// </summary>
            /// <param name="url">the redirect url.</param>
            /// <returns>the <see cref="Builder"/> instance.</returns>
            public Builder SetRedirectUrl(string url)
            {
                RedirectUrl = url;
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
                this.State = state;
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
                this.SetClientId((string)config.client_id)
                    .SetClientSecret((string)config.client_secret)
                    .SetRedirectUrl((string)config.redirect_url);
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
        }
    }
}