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

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Elton.Nest.Models
{
    /// <summary>
    /// NestToken contains the access token and expiry duration associated with an authenticated user.
    /// </summary>
    [DataContract]
    public class NestToken : IEquatable<NestToken>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Device" /> class.
        /// </summary>
        [JsonConstructor]
        protected NestToken() { }

        /// <summary>
        /// Returns the access token. Use the access token to authenticate with the WWN API.
        /// </summary>
        /// <value">the access token.</value>
        [DataMember(Name = "access_token")]
        public string Token { get; set; }

        /// <summary>
        /// Returns the number of seconds until the token expires.
        /// </summary>
        /// <value">the number of seconds until the token expires.</value>
        [DataMember(Name = "expires_in")]
        public long ExpiresInSecs { get; set; }

        /// <summary>
        /// Create a new NestToken.
        /// </summary>
        /// <param name="token">the access token.</param>
        /// <param name="expiresIn">the number of seconds until the token expires.</param>
        public NestToken(string token = default, long expiresIn = default)
        {
            this.Token = token;
            this.ExpiresInSecs = expiresIn;
        }

        public override string ToString()
        {
            return Utils.ToString(this);
        }

        public override int GetHashCode()
        {
            return Utils.GetHashCode(this);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as NestToken);
        }

        public bool Equals(NestToken other)
        {
            return Utils.AreEqual(this, other);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}