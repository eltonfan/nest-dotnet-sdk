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
    /// Thermostat represents and contains all properties of a Nest Thermostat.
    /// </summary>
    [DataContract]
    public class Thermostat : Device, IEquatable<Device>, IValidatableObject
    {
        public const string KEY_CAN_COOL = "can_cool";
        public const string KEY_CAN_HEAT = "can_heat";
        public const string KEY_IS_USING_EMERGENCY_HEAT = "is_using_emergency_heat";
        public const string KEY_HAS_FAN = "has_fan";
        public const string KEY_FAN_TIMER_ACTIVE = "fan_timer_active";
        public const string KEY_FAN_TIMER_TIMEOUT = "fan_timer_timeout";
        public const string KEY_HAS_LEAF = "has_leaf";
        public const string KEY_TEMP_SCALE = "temperature_scale";
        public const string KEY_TARGET_TEMP_F = "target_temperature_f";
        public const string KEY_TARGET_TEMP_C = "target_temperature_c";
        public const string KEY_TARGET_TEMP_HIGH_F = "target_temperature_high_f";
        public const string KEY_TARGET_TEMP_HIGH_C = "target_temperature_high_c";
        public const string KEY_TARGET_TEMP_LOW_F = "target_temperature_low_f";
        public const string KEY_TARGET_TEMP_LOW_C = "target_temperature_low_c";
        public const string KEY_AWAY_TEMP_HIGH_F = "away_temperature_high_f";
        public const string KEY_AWAY_TEMP_HIGH_C = "away_temperature_high_c";
        public const string KEY_AWAY_TEMP_LOW_F = "away_temperature_low_f";
        public const string KEY_AWAY_TEMP_LOW_C = "away_temperature_low_c";
        public const string KEY_HVAC_MODE = "hvac_mode";
        public const string KEY_AMBIENT_TEMP_F = "ambient_temperature_f";
        public const string KEY_AMBIENT_TEMP_C = "ambient_temperature_c";
        public const string KEY_HUMIDITY = "humidity";
        public const string KEY_HVAC_STATE = "hvac_state";
        public const string KEY_IS_LOCKED = "is_locked";
        public const string KEY_LOCKED_TEMP_MIN_F = "locked_temp_min_f";
        public const string KEY_LOCKED_TEMP_MAX_F = "locked_temp_max_f";
        public const string KEY_LOCKED_TEMP_MIN_C = "locked_temp_min_c";
        public const string KEY_LOCKED_TEMP_MAX_C = "locked_temp_max_c";
        public const string KEY_LABEL = "label";

        /// <summary>
        /// Initializes a new instance of the <see cref="Device" /> class.
        /// </summary>
        [JsonConstructor]
        protected Thermostat() { }

        public Thermostat(bool CanCool = default)
        {
            this.CanCool = CanCool;
        }

        /// <summary>
        /// Returns true if this thermostat is connected to a cooling system.
        /// </summary>
        /// <value">true if this thermostat is connected to a cooling system.</value>
        [DataMember(Name = "can_cool")]
        public bool CanCool { get; set; }

        /// <summary>
        /// Returns true if this thermostat is connected to a heating system.
        /// </summary>
        /// <value">true if this thermostat is connected to a heating system.</value>
        [DataMember(Name = "can_heat")]
        public bool CanHeat { get; set; }

        /// <summary>
        /// Returns true if this thermostat is currently operating using the emergency heating system.
        /// </summary>
        /// <value">true if this thermostat is currently operating using the emergency heating system.</value>
        [DataMember(Name = "is_using_emergency_heat")]
        public bool IsUsingEmergencyHeat { get; set; }

        /// <summary>
        /// Returns true if this thermostat has a connected fan.
        /// </summary>
        /// <value">true if this thermostat has a connected fan.</value>
        [DataMember(Name = "has_fan")]
        public bool HasFan { get; set; }

        /// <summary>
        /// Returns true if the fan is currently running on a timer, false otherwise.
        /// </summary>
        /// <value">true if the fan is currently running on a timer, false otherwise.</value>
        [DataMember(Name = "fan_timer_active")]
        public bool FanTimerActive { get; set; }

        /// <summary>
        /// If the fan is running on a timer, this provides the timestamp (in ISO-8601 format) at which
        /// the fan will stop running.
        /// </summary>
        /// <value">the timestamp (in ISO-8601 format) at which the fan will stop running.</value>
        [DataMember(Name = "fan_timer_timeout")]
        public string FanTimerTimeout { get; set; }

        /// <summary>
        /// Returns true if the thermostat is currently displaying the leaf indicator, false otherwise.
        /// </summary>
        /// <value">true if the thermostat is currently displaying the leaf indicator, false otherwise.</value>
        [DataMember(Name = "has_leaf")]
        public bool HasLeaf { get; set; }

        /// <summary>
        /// Returns the temperature scale: one of "C" (Celsius) or "F" (Fahrenheit) that this thermostat
        /// should display temperatures in.
        /// </summary>
        /// <value>
        /// the temperature scale: one of "C" (Celsius) or "F" (Fahrenheit) that this thermostat
        /// should display temperatures in.
        /// </value>
        [DataMember(Name = "temperature_scale")]
        public string TemperatureScale { get; set; }

        /// <summary>
        /// Returns the target temperature of the thermostat in Fahrenheit. Note that this is only
        /// applicable when in Heat or Cool mode, not "Heat and Cool" mode.
        /// </summary>
        /// <value">the target temperature of the thermostat in Fahrenheit.</value>
        [DataMember(Name = "target_temperature_f")]
        public long TargetTemperatureF { get; set; }

        /// <summary>
        /// Returns the target temperature of the thermostat in Celsius. Note that this is only
        /// applicable when in Heat or Cool mode, not "Heat and Cool" mode.
        /// </summary>
        /// <value">the target temperature of the thermostat in Celsius.</value>
        [DataMember(Name = "target_temperature_c")]
        public double TargetTemperatureC { get; set; }

        /// <summary>
        /// Returns the target temperature of the cooling system in Fahrenheit when in "Heat and Cool" mode.
        /// </summary>
        /// <value">the target temperature of the cooling system in Fahrenheit when in "Heat and Cool" mode.</value>
        [DataMember(Name = "target_temperature_high_f")]
        public long TargetTemperatureHighF { get; set; }

        /// <summary>
        /// Returns the target temperature of the cooling system in Celsius when in "Heat and Cool" mode.
        /// </summary>
        /// <value">the target temperature of the cooling system in Celsius when in "Heat and Cool" mode.</value>
        [DataMember(Name = "target_temperature_high_c")]
        public double TargetTemperatureHighC { get; set; }

        /// <summary>
        /// Returns the target temperature of the heating system in Celsius when in "Heat and Cool" mode.
        /// </summary>
        /// <value">the target temperature of the heating system in Celsius when in "Heat and Cool" mode.</value>
        [DataMember(Name = "target_temperature_low_f")]
        public long TargetTemperatureLowF { get; set; }

        /// <summary>
        /// Returns the target temperature of the heating system in Fahrenheit when in "Heat and Cool" mode.
        /// </summary>
        /// <value">the target temperature of the heating system in Fahrenheit when in "Heat and Cool" mode.</value>
        [DataMember(Name = "target_temperature_low_c")]
        public double TargetTemperatureLowC { get; set; }


        /// <summary>
        /// Returns the temperature (in Fahrenheit) at which the cooling system will engage when in
        /// "Away" state.
        /// </summary>
        /// <value>
        /// the temperature (in Fahrenheit) at which the cooling system will engage when in
        /// "Away" state.
        /// </value>
        [DataMember(Name = "away_temperature_high_f")]
        public long AwayTemperatureHighF { get; set; }

        /// <summary>
        /// Returns the temperature (in Celsius) at which the cooling system will engage when in "Away" state.
        /// </summary>
        /// <value">the temperature (in Celsius) at which the cooling system will engage when in "Away" state.</value>
        [DataMember(Name = "away_temperature_high_c")]
        public double AwayTemperatureHighC { get; set; }

        /// <summary>
        /// Returns the temperature (in Fahrenheit) at which the heating system will engage when in
        /// "Away" state.
        /// </summary>
        /// <value>
        /// the temperature (in Fahrenheit) at which the heating system will engage when in
        /// "Away" state.
        /// </value>
        [DataMember(Name = "away_temperature_low_f")]
        public long AwayTemperatureLowF { get; set; }

        /// <summary>
        /// Returns the temperature (in Celsius) at which the heating system will engage when in "Away" state.
        /// </summary>
        /// <value">the temperature (in Celsius) at which the heating system will engage when in "Away" state.</value>
        [DataMember(Name = "away_temperature_low_c")]
        public double AwayTemperatureLowC { get; set; }

        /// <summary>
        /// Returns the current operating mode of the thermostat.
        /// </summary>
        /// <value">the current operating mode of the thermostat.</value>
        [DataMember(Name = "hvac_mode")]
        public string HvacMode { get; set; }

        /// <summary>
        /// Returns the current ambient temperature in the structure in Fahrenheit.
        /// </summary>
        /// <value">the current ambient temperature in the structure in Fahrenheit.</value>
        [DataMember(Name = "ambient_temperature_f")]
        public long AmbientTemperatureF { get; set; }

        /// <summary>
        /// Returns the current ambient temperature in the structure in Celsius.
        /// </summary>
        /// <value">the current ambient temperature in the structure in Celsius.</value>
        [DataMember(Name = "ambient_temperature_c")]
        public double AmbientTemperatureC { get; set; }

        /// <summary>
        /// Returns the humidity, in percent (%) format, measured at the device.
        /// </summary>
        /// <value">the humidity, in percent (%) format, measured at the device.</value>
        [DataMember(Name = "humidity")]
        public long Humidity { get; set; }

        /// <summary>
        /// Returns whether HVAC system is actively heating, cooling or is off.
        /// <p/>
        /// Values: "heating", "cooling", "off"
        /// </summary>
        /// <value">whether HVAC system is actively heating, cooling or is off.</value>
        [DataMember(Name = "hvac_state")]
        public string HvacState { get; set; }

        /// <summary>
        /// Returns true if the thermostat is locked.
        /// </summary>
        /// <value">a boolean indicating if the thermostat is locked or not.</value>
        [DataMember(Name = "is_locked")]
        public bool IsLocked { get; set; }

        /// <summary>
        /// Returns the minimum locked temperature in Fahrenheit.
        /// </summary>
        /// <value">the minimum locked temperature in Fahrenheit.</value>
        [DataMember(Name = "locked_temp_min_f")]
        public string LockedTempMinF { get; set; }

        /// <summary>
        /// Returns the maximum locked temperature in Fahrenheit.
        /// </summary>
        /// <value">the maximum locked temperature in Fahrenheit.</value>
        [DataMember(Name = "locked_temp_max_f")]
        public string LockedTempMaxF { get; set; }

        /// <summary>
        /// Returns the minimum locked temperature in Celsius.
        /// </summary>
        /// <value">the minimum locked temperature in Celsius.</value>
        [DataMember(Name = "locked_temp_min_c")]
        public string LockedTempMinC { get; set; }

        /// <summary>
        /// Returns the maximum locked temperature in Celsius.
        /// </summary>
        /// <value">the maximum locked temperature in Celsius.</value>
        [DataMember(Name = "locked_temp_max_c")]
        public string LockedTempMaxC { get; set; }

        /// <summary>
        /// Returns the current label of the thermostat.
        /// </summary>
        /// <value">the current label of the thermostat.</value>
        [DataMember(Name = "label")]
        public string Label { get; set; }

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
            return this.Equals(obj as Thermostat);
        }

        public bool Equals(Thermostat other)
        {
            return Utils.AreEqual(this, other);
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
