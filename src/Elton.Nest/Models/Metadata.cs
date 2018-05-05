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
    /// Metadata holds the information related to your Nest client.
    /// </summary>
    [DataContract]
    public class Metadata : IEquatable<Metadata>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Device" /> class.
        /// </summary>
        [JsonConstructor]
        protected Metadata() { }

        public Metadata(string AccessToken = default, long ClientVersion = default)
        {
            this.AccessToken = AccessToken;
            this.ClientVersion = ClientVersion;
        }

        /// <summary>
        /// Returns the access token associated with your Nest API connection.
        ///
        /// @return the access token associated with your Nest API connection.
        /// </summary>
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// Returns the last user-authorized version of a product. The client version increments every
        /// time a change is made to the permissions in a product. You can use this to make sure that a
        /// user has the right permissions for certain features.
        /// <p/>
        /// See here for more information: https://goo.gl/J4RPAc
        ///
        /// @return the last user-authorized version of a product.
        /// </summary>
        [DataMember(Name = "client_version")]
        public long ClientVersion { get; set; }

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
            return this.Equals(obj as Metadata);
        }

        public bool Equals(Metadata other)
        {
            return Utils.AreEqual(this, other);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
