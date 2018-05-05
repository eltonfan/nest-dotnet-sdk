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
    /**
     * SmokeCOAlarm represents and contains all properties of a Nest smoke+CO alarm device.
     */
    [DataContract]
    public class SmokeCOAlarm : Device, IEquatable<SmokeCOAlarm>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Device" /> class.
        /// </summary>
        [JsonConstructor]
        protected SmokeCOAlarm() { }

        public SmokeCOAlarm(string BatteryHealth = default, string CoAlarmState = default, string SmokeAlarmState = default,
            bool IsManualTestActive = default, string LastManualTestTime = default, string UiColorState = default)
        {
            this.BatteryHealth = BatteryHealth;
            this.CoAlarmState = CoAlarmState;
            this.SmokeAlarmState = SmokeAlarmState;
            this.IsManualTestActive = IsManualTestActive;
            this.LastManualTestTime = LastManualTestTime;
            this.UiColorState = UiColorState;
        }

        /**
         * Returns the battery life/health, an estimate of remaining battery power level.
         * <p/>
         * Values: "ok", "replace"
         *
         * @return the battery life/health, an estimate of remaining battery power level.
         */
        [DataMember(Name = "battery_health")]
        public string BatteryHealth { get; set; }

        /**
         * Returns the CO alarm status.
         * <p/>
         * Values: "ok", "warning", "emergency"
         *
         * @return the CO alarm status.
         */
        [DataMember(Name = "co_alarm_state")]
        public string CoAlarmState { get; set; }

        /**
         * Returns the smoke alarm status.
         * <p/>
         * Values: "ok", "warning", "emergency"
         *
         * @return the smoke alarm status.
         */
        [DataMember(Name = "smoke_alarm_state")]
        public string SmokeAlarmState { get; set; }

        /**
         * Returns the state of the manual smoke and CO alarm test.
         *
         * @return the state of the manual smoke and CO alarm test.
         */
        [DataMember(Name = "is_manual_test_active")]
        public bool IsManualTestActive { get; set; }

        /**
         * Returns the timestamp of the last successful manual smoke+CO alarm test, in ISO 8601 format.
         *
         * @return the timestamp of the last successful manual smoke+CO alarm test, in ISO 8601 format.
         */
        [DataMember(Name = "last_manual_test_time")]
        public string LastManualTestTime { get; set; }

        /**
         * Returns the device status by color in the Nest app UI. It is an aggregate condition for
         * battery+smoke+co states, and reflects the actual color indicators displayed in the Nest app.
         * <p/>
         * Values: "gray", "green", "yellow", "red"
         *
         * @return the device status by color in the Nest app UI.
         */
        [DataMember(Name = "ui_color_state")]
        public string UiColorState { get; set; }

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
            return this.Equals(obj as SmokeCOAlarm);
        }

        public bool Equals(SmokeCOAlarm other)
        {
            return Utils.AreEqual(this, other);
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
