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
using Elton.Nest.Rest;
using Elton.Nest.Rest.Parsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Elton.Nest.Setters
{
    public class ThermostatSetter
    {
        private static string GetPath(string thermostatId)
        {
            return new Utils.PathBuilder()
                .Append(Constants.KEY_DEVICES)
                .Append(Constants.KEY_THERMOSTATS)
                .Append(thermostatId)
                .Build();
        }

        readonly RestClient restClient;
        public ThermostatSetter(RestClient restClient)
        {
            this.restClient = restClient;
        }

        /// <summary>
        /// Sets the desired temperature, in full degrees Fahrenheit (1&deg;F). Used when hvac_mode =
        /// "heat" or "cool".
        /// </summary>
        /// <param name="thermostatId">The unique identifier for the <see cref="Thermostat"/>.</param>
        /// <param name="temperature">The desired temperature in full degrees Fahrenheit.</param>
        /// <param name="callback">A <see cref="Callback"/> to receive whether the change was successful.</param>
        public void SetTargetTemperatureF(string thermostatId, long temperature, Callback callback = null)
        {
            restClient.WriteLong(GetPath(thermostatId), Thermostat.KEY_TARGET_TEMP_F, temperature, callback);
        }

        /// <summary>
        /// Sets the desired temperature, in half degrees Celsius (0.5&deg;C). Used when hvac_mode =
        /// "heat" or "cool".
        /// </summary>
        /// <param name="thermostatId">The unique identifier for the <see cref="Thermostat"/>.</param>
        /// <param name="temperature">The desired temperature, in half degrees Celsius (0.5&deg;C).</param>
        /// <param name="callback">A <see cref="Callback"/> to receive whether the change was successful.</param>
        public void SetTargetTemperatureC(string thermostatId, double temperature, Callback callback = null)
        {
            restClient.WriteDouble(GetPath(thermostatId), Thermostat.KEY_TARGET_TEMP_C, temperature, callback);
        }

        /// <summary>
        /// Sets the minimum target temperature, displayed in whole degrees Fahrenheit (1&deg;F). Used
        /// when hvac_mode = "heat-cool" (Heat / Cool mode).
        /// </summary>
        /// <param name="thermostatId">The unique identifier for the <see cref="Thermostat"/>.</param>
        /// <param name="temperature">The minimum desired temperature, displayed in whole degrees Fahrenheit.</param>
        /// <param name="callback">A <see cref="Callback"/> to receive whether the change was successful.</param>
        public void SetTargetTemperatureLowF(string thermostatId, long temperature, Callback callback = null)
        {
            restClient.WriteLong(GetPath(thermostatId), Thermostat.KEY_TARGET_TEMP_LOW_F, temperature, callback);
        }

        /// <summary>
        /// Sets the minimum target temperature, displayed in half degrees Celsius (0.5&deg;C). Used when
        /// hvac_mode = "heat-cool" (Heat / Cool mode).
        /// </summary>
        /// <param name="thermostatId">The unique identifier for the <see cref="Thermostat"/>.</param>
        /// <param name="temperature">The minimum target temperature, displayed in half degrees Celsius.</param>
        /// <param name="callback">A <see cref="Callback"/> to receive whether the change was successful.</param>
        public void SetTargetTemperatureLowC(string thermostatId, double temperature, Callback callback = null)
        {
            restClient.WriteDouble(GetPath(thermostatId), Thermostat.KEY_TARGET_TEMP_C, temperature, callback);
        }

        /// <summary>
        /// Sets the maximum target temperature, displayed in whole degrees Fahrenheit (1&deg;F). Used
        /// when hvac_mode = "heat-cool" (Heat / Cool mode).
        /// </summary>
        /// <param name="thermostatId">The unique identifier for the <see cref="Thermostat"/>.</param>
        /// <param name="temperature">The maximum desired temperature, displayed in whole degrees Fahrenheit.</param>
        /// <param name="callback">A <see cref="Callback"/> to receive whether the change was successful.</param>
        public void SetTargetTemperatureHighF(string thermostatId, long temperature, Callback callback = null)
        {
            restClient.WriteLong(GetPath(thermostatId), Thermostat.KEY_TARGET_TEMP_HIGH_F, temperature, callback);
        }

        /// <summary>
        /// Sets the maximum target temperature, displayed in half degrees Celsius (0.5&deg;C). Used when
        /// hvac_mode = "heat-cool" (Heat / Cool mode).
        /// </summary>
        /// <param name="thermostatId">The unique identifier for the <see cref="Thermostat"/>.</param>
        /// <param name="temperature">The maximum target temperature, displayed in half degrees Celsius.</param>
        /// <param name="callback">A <see cref="Callback"/> to receive whether the change was successful.</param>
        public void SetTargetTemperatureHighC(string thermostatId, double temperature, Callback callback = null)
        {
            restClient.WriteDouble(GetPath(thermostatId), Thermostat.KEY_TARGET_TEMP_HIGH_C, temperature, callback);
        }

        /// <summary>
        /// Sets the HVAC system heating/cooling modes. For systems with both heating and cooling
        /// capability, set this value to "heat-cool" (Heat / Cool mode) to get the best experience.
        /// </summary>
        /// <param name="thermostatId">The unique identifier for the <see cref="Thermostat"/>.</param>
        /// <param name="mode">The heating/cooling mode. Values can be "heat", "cool", "heat-cool", or "off".</param>
        /// <param name="callback">A <see cref="Callback"/> to receive whether the change was successful.</param>
        public void SetHVACMode(string thermostatId, string mode, Callback callback = null)
        {
            restClient.WriteString(GetPath(thermostatId), Thermostat.KEY_HVAC_MODE, mode, callback);
        }

        /// <summary>
        /// Sets whether the fan timer is engaged; used with fanTimerTimeout to turn on the fan for a
        /// (user-specified) preset duration.
        /// </summary>
        /// <param name="thermostatId">The unique identifier for the <see cref="Thermostat"/>.</param>
        /// <param name="isActive">true if the fan timer is to be engaged, false if the fan timer should be disengaged.</param>
        /// <param name="callback">A <see cref="Callback"/> to receive whether the change was successful.</param>
        public void setFanTimerActive(string thermostatId, bool isActive, Callback callback = null)
        {
            restClient.WriteBoolean(GetPath(thermostatId), Thermostat.KEY_FAN_TIMER_ACTIVE, isActive, callback);
        }

        /// <summary>
        /// Sets the thermostat scale to Fahrenheit or Celsius; used with temperature display.
        /// </summary>
        /// <param name="thermostatId">The unique identifier for the <see cref="Thermostat"/>.</param>
        /// <param name="tempScale">A string for temperature scale. "F" for Fahrenheit, "C" for Celsius.</param>
        public void setTemperatureScale(string thermostatId, string tempScale, Callback callback = null)
        {
            restClient.WriteString(GetPath(thermostatId), Thermostat.KEY_TEMP_SCALE, tempScale, callback);
        }

        /// <summary>
        /// Sets the thermostat label.
        /// </summary>
        /// <param name="thermostatId">The unique identifier for the <see cref="Thermostat"/>.</param>
        /// <param name="label">A string for the custom label.</param>
        public void setLabel(string thermostatId, string label, Callback callback = null)
        {
            restClient.WriteString(GetPath(thermostatId), Thermostat.KEY_LABEL, label, callback);
        }
    }
}
