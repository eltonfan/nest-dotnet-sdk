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
    /// Structure represents and contains all properties of a Nest structure.
    /// </summary>
    [DataContract]
    public class Structure : IEquatable<Structure>, IValidatableObject
    {
        public const string KEY_STRUCTURE_ID = "structure_id";
        public const string KEY_THERMOSTATS = "thermostats";
        public const string KEY_SMOKE_CO_ALARMS = "smoke_co_alarms";
        public const string KEY_CAMERAS = "cameras";
        public const string KEY_DEVICES = "devices";
        public const string KEY_AWAY = "away";
        public const string KEY_NAME = "name";
        public const string KEY_COUNTRY_CODE = "country_code";
        public const string KEY_POSTAL_CODE = "postal_code";
        public const string KEY_PEAK_PERIOD_START_TIME = "peak_period_start_time";
        public const string KEY_PEAK_PERIOD_END_TIME = "peak_period_end_time";
        public const string KEY_TIME_ZONE = "time_zone";
        public const string KEY_ETA = "eta";
        public const string KEY_RHR_ENROLLMENT = "rhr_enrollment";
        public const string KEY_WHERES = "wheres";

        /// <summary>
        /// Initializes a new instance of the <see cref="Structure" /> class.
        /// </summary>
        [JsonConstructor]
        protected Structure() { }

        public Structure(string StructureId = default)
        {
            this.StructureId = default;
        }

        /// <summary>
        /// Returns the ID number of the structure.
        /// </summary>
        /// <value">the ID number of the structure.</value>
        [DataMember(Name = "structure_id")]
        public string StructureId { get; set; }

        /// <summary>
        /// Returns the list of thermostats in the structure.
        /// </summary>
        /// <value">the list of thermostats in the structure.</value>
        [DataMember(Name = "thermostats")]
        public List<string> Thermostats { get; set; }

        /// <summary>
        /// Returns the list of smoke+CO alarms in the structure.
        /// </summary>
        /// <value">the list of smoke+CO alarms in the structure.</value>
        [DataMember(Name = "smoke_co_alarms")]
        public List<string> SmokeCoAlarms { get; set; }

        /// <summary>
        /// Returns the list of cameras in the structure.
        /// </summary>
        /// <value">the list of cameras in the structure.</value>
        [DataMember(Name = "cameras")]
        public List<string> Cameras { get; set; }

        /// <summary>
        /// Returns an object containing $company and $product_type information.
        /// <p/>
        /// More info: https://developer.nest.com/documentation/api-reference/overview#devices2
        /// </summary>
        /// <value">an object containing $company and $product_type information.</value>
        [DataMember(Name = "devices")]
        public Dictionary<string, object> Devices { get; set; }

        /// <summary>
        /// Returns the away state of the structure. In order for a structure to be in the Auto-Away
        /// state, all devices must also be in Auto-Away state. When any device leaves the Auto-Away
        /// state, then the structure also leaves the Auto-Away state.
        /// <p/>
        /// Values can be "home", "away", or "auto-away"
        /// </summary>
        /// <value">the away state of the structure.</value>
        [DataMember(Name = "away")]
        public string Away { get; set; }

        /// <summary>
        /// Returns the user-defined name of the structure.
        /// </summary>
        /// <value">the user-defined name of the structure.</value>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Returns the country code, in ISO 3166 alpha-2 format.
        /// </summary>
        /// <value">the country code, in ISO 3166 alpha-2 format.</value>
        [DataMember(Name = "country_code")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Returns the postal or zip code, depending on the country.
        /// </summary>
        /// <value">the postal or zip code.</value>
        [DataMember(Name = "postal_code")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Returns the start time of the Energy rush hour event, in ISO 8601 format.
        /// </summary>
        /// <value">the start time of the Energy rush hour event, in ISO 8601 format.</value>
        [DataMember(Name = "peak_period_start_time")]
        public string PeakPeriodStartTime { get; set; }

        /// <summary>
        /// Returns the end time of the Energy rush hour event, in ISO 8601 format.
        /// </summary>
        /// <value">the end time of the Energy rush hour event, in ISO 8601 format.</value>
        [DataMember(Name = "peak_period_end_time")]
        public string PeakPeriodEndTime { get; set; }

        /// <summary>
        /// Returns the time zone at the structure, in IANA time zone format.
        /// </summary>
        /// <value">the time zone at the structure, in IANA time zone format.</value>
        [DataMember(Name = "time_zone")]
        public string TimeZone { get; set; }

        /// <summary>
        /// Returns the ETA object that can be set on a structure.
        /// <p/>
        /// More info: https://developer.nest.com/documentation/cloud/eta-guide
        /// </summary>
        /// <value">the ETA object that can be set on a structure.</value>
        [DataMember(Name = "eta")]
        public ETA Eta { get; set; }

        /// <summary>
        /// Returns the Rush Hour Rewards enrollment status.
        /// <p/>
        /// More info: http://support.nest.com/article/What-is-Rush-Hour-Rewards
        /// </summary>
        /// <value">the Rush Hour Rewards enrollment status.</value>
        [DataMember(Name = "rhr_enrollment")]
        public bool RhrEnrollment { get; set; }

        /// <summary>
        /// Returns an object containing where identifiers for devices in the structure.
        /// <p/>
        /// More info: https://developer.nest.com/documentation/cloud/how-to-structures-object#wheres
        /// </summary>
        /// <value">an object containing where identifiers for devices in the structure.</value>
        [DataMember(Name = "wheres")]
        public Dictionary<string, Where> Wheres { get; set; }

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
            return this.Equals(obj as Structure);
        }

        public bool Equals(Structure other)
        {
            return Utils.AreEqual(this, other);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }

        /// <summary>
        /// Where is an object containing where identifiers for devices in the structure.
        /// </summary>
        [DataContract]
        public class Where : IEquatable<Where>, IValidatableObject
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Where" /> class.
            /// </summary>
            [JsonConstructor]
            protected Where() { }

            /// <summary>
            /// Returns the Where unique identifier.
            /// </summary>
            /// <value">the Where unique identifier.</value>
            [DataMember(Name = "where_id")]
            /// <summary>
            /// Returns the name of the room. E.g. "Bedroom".
            /// </summary>
            /// <value">the name of the room. E.g. "Bedroom".</value>
            public string WhereId { get; set; }
            [DataMember(Name = "name")]
            public string Name { get; set; }

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
                return this.Equals(obj as Where);
            }

            public bool Equals(Where other)
            {
                return Utils.AreEqual(this, other);
            }

            public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
            {
                yield break;
            }
        }

        /// <summary>
        /// ETA is used to let Nest know that a user is expected to return home at a specific time.
        /// </summary>
        [DataContract]
        public class ETA : IEquatable<ETA>, IValidatableObject
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Where" /> class.
            /// </summary>
            [JsonConstructor]
            protected ETA() { }

            /// <summary>
            /// Returns a unique identifier for this ETA instance.
            /// </summary>
            /// <value">a unique identifier for this ETA instance.</value>
            [DataMember(Name = "trip_id")]
            public string TripId { get; set; }

            /// <summary>
            /// Returns the beginning time of the estimated arrival window, in ISO 8601 format.
            /// </summary>
            /// <value">the beginning time of the estimated arrival window, in ISO 8601 format.</value>
            [DataMember(Name = "estimated_arrival_window_begin")]
            public string EstimatedArrivalWindowBegin { get; set; }

            /// <summary>
            /// Returns the end time of the estimated arrival window, in ISO 8601 format.
            /// </summary>
            /// <value">the end time of the estimated arrival window, in ISO 8601 format.</value>
            [DataMember(Name = "estimated_arrival_window_end")]
            public string EstimatedArrivalWindowEnd { get; set; }

            public ETA(string tridId, string estArrivalWindowBegin, string estArrivalWindowEnd)
            {
                this.TripId = tridId;
                this.EstimatedArrivalWindowBegin = estArrivalWindowBegin;
                this.EstimatedArrivalWindowEnd = estArrivalWindowEnd;
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
                return this.Equals(obj as ETA);
            }

            public bool Equals(ETA other)
            {
                return Utils.AreEqual(this, other);
            }

            public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
            {
                yield break;
            }
        }
    }
}