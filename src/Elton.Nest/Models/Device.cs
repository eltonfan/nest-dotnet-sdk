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
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Elton.Nest.Models
{
    /// <summary>
    /// Device represents any Nest device. All devices (e.g. <see cref="Thermostat"/>, <see cref="Camera"/>,
    /// <see cref="SmokeCOAlarm"/>) should extend Device and thus will contain all properties that Device contains.
    /// </summary>
    [DataContract]
    public class Device : IEquatable<Device>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Device" /> class.
        /// </summary>
        [JsonConstructor]
        protected Device() { }

        public Device(string DeviceId = default, string Locale = default, string SoftwareVersion = default, string StructureId = default,
            string Name = default, string NameLong = default, string LastConnection = default, bool IsOnline = default)
        {
            this.DeviceId = DeviceId;
            this.Locale = Locale;
            this.SoftwareVersion = SoftwareVersion;
            this.StructureId = StructureId;
            this.Name = Name;
            this.NameLong = NameLong;
            this.LastConnection = LastConnection;
            this.IsOnline = IsOnline;
        }

        /// <summary>
        /// Returns the unique identifier of this device.
        /// </summary>
        /// <value">the unique identifier of this device.</value>
        [DataMember(Name = "device_id")]
        public string DeviceId { get; set; }

        /// <summary>
        /// Returns the locale for this device, if set.
        /// </summary>
        /// <value">the locale for this device, if set.</value>
        [DataMember(Name = "locale")]
        public string Locale { get; set; }

        /// <summary>
        /// Returns the current software version that this device has installed.
        /// </summary>
        /// <value">the current software version that this device has installed.</value>
        [DataMember(Name = "software_version")]
        public string SoftwareVersion { get; set; }

        /// <summary>
        /// Returns the id of the structure that this device is contained in.
        /// </summary>
        /// <value">the id of the structure that this device is contained in.</value>
        [DataMember(Name = "structure_id")]
        public string StructureId { get; set; }

        /// <summary>
        /// Returns an abbreviated version of the user's name for this device.
        /// </summary>
        /// <value">an abbreviated version of the user's name for this device.</value>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Returns a verbose version of the user's name for this device.
        /// </summary>
        /// <value">a verbose version of the user's name for this device.</value>
        [DataMember(Name = "name_long")]
        public string NameLong { get; set; }

        /// <summary>
        /// Returns the timestamp (in ISO-8601 format) when the device last connected to the Nest.
        /// </summary>
        /// <value">the timestamp (in ISO-8601 format) when the device last connected to the Nest.</value>
        [DataMember(Name = "last_connection")]
        public string LastConnection { get; set; }

        /// <summary>
        /// Returns whether the device is online.
        /// </summary>
        /// <value">whether the device is online.</value>
        [DataMember(Name = "is_online")]
        public bool IsOnline { get; set; }

        /// <summary>
        /// Returns a unique, Nest-generated identifier that represents the display name of the device.
        /// </summary>
        /// <value">a unique, Nest-generated identifier that represents the display name of the device.</value>
        [DataMember(Name = "where_id")]
        public string WhereId { get; set; }

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
            return this.Equals(obj as Device);
        }

        public bool Equals(Device other)
        {
            return Utils.AreEqual(this, other);
        }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}