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

        private String mClientID;
        private String mStateValue;
        private String mClientSecret;
        private String mRedirectURL;

        /// <summary>
        /// Initializes a new instance of the <see cref="NestConfig" /> class.
        /// </summary>
        private NestConfig(Builder builder)
        {
            mClientID = builder.mBuilderClientID;
            mStateValue = builder.mBuilderStateValue;
            mClientSecret = builder.mBuilderClientSecret;
            mRedirectURL = builder.mBuilderRedirectURL;
        }

        /// <summary>
        /// Returns the client id.
        ///
        /// @return the client id.
        /// </summary>
        public String ClientID => mClientID;

        /// <summary>
        /// Returns the state value. This is randomly generated for each {@link NestConfig}.
        ///
        /// @return the state value.
        /// </summary>
        public String StateValue => mStateValue;

        /// <summary>
        /// Returns the client secret. Keep this secret safe.
        ///
        /// @return the client secret.
        /// </summary>
        public String ClientSecret => mClientSecret;

        /// <summary>
        /// Returns the redirect URL. Must match the redirect URL set in the Nest developer portal.
        ///
        /// @return the redirect URL.
        /// </summary>
        public String RedirectURL => mRedirectURL;

        public override string ToString()
        {
            try
            {
                var json = new JObject();
                json.Add(KEY_CLIENT_ID, mClientID);
                json.Add(KEY_CLIENT_SECRET, mClientSecret);
                json.Add(KEY_REDIRECT_URL, mRedirectURL);
                json.Add(KEY_STATE_VALUE, mStateValue);

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
        /// Builder for creating {@link NestConfig} objects.
        /// </summary>
        public class Builder
        {
            internal string mBuilderClientID;
            internal string mBuilderRedirectURL;
            internal string mBuilderStateValue;
            internal string mBuilderClientSecret;

            /// <summary>
            /// Sets the client id.
            ///
            /// @param id the client id.
            /// @return the {@link Builder} instance.
            /// </summary>
            public Builder clientID(String id)
            {
                mBuilderClientID = id;
                return this;
            }

            /// <summary>
            /// Sets the client secret.
            ///
            /// @param secret the client secret.
            /// @return the {@link Builder} instance.
            /// </summary>
            public Builder clientSecret(String secret)
            {
                mBuilderClientSecret = secret;
                return this;
            }

            /// <summary>
            /// Sets the redirect URL. Must match the redirect URL set in the Nest developer portal.
            ///
            /// @param url the redirect url.
            /// @return the {@link Builder} instance.
            /// </summary>
            public Builder redirectURL(String url)
            {
                mBuilderRedirectURL = url;
                return this;
            }

            /// <summary>
            /// Sets the state value. This method be called directly, as the state value is automatically
            /// and randomly generated when {@link Builder#build()} is called.
            ///
            /// @param state the state value.
            /// @return the {@link Builder} instance.
            /// </summary>
            private Builder SetStateValue(String state)
            {
                mBuilderStateValue = state;
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
                this.clientID(config.client_id)
                    .clientSecret(config.client_secret)
                    .redirectURL(config.redirect_url);
                    //.SetStateValue(config.state_value);

                return this;
            }

            /// <summary>
            /// Builds and returns the new {@link NestConfig} object.
            ///
            /// @return the new {@link NestConfig} object.
            /// </summary>
            public NestConfig build()
            {
                // Create random state value on each creation
                SetStateValue($"app-state" + Stopwatch.GetTimestamp() + "-" + random.Next());
                return new NestConfig(this);
            }
            readonly static Random random = new Random();
        }
    }
}