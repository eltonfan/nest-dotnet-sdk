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

using System;
using System.Collections.Generic;
using System.Text;

namespace Elton.Nest.Models
{
    /// <summary>
    /// GlobalUpdate contains the state of all devices, structures and metadata in the Nest account when
    /// a change is detected in anything. A GlobalUpdate object is returned by <see cref="GlobalListener#onUpdate(GlobalUpdate)"/>
    /// when an update occurs.
    /// </summary>
    public class GlobalUpdate
    {
        readonly DeviceUpdate devices;
        readonly List<Structure> mStructures;
        readonly Metadata mMetadata;

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalUpdate" /> class.
        /// </summary>
        public GlobalUpdate(List<Thermostat> thermostats, List<SmokeCOAlarm> smokeCOAlarms,
            List<Camera> cameras, List<Structure> structures, Metadata metadata)
        {
            devices = new DeviceUpdate(thermostats, smokeCOAlarms, cameras);
            mStructures = structures;
            mMetadata = metadata;
        }

        public DeviceUpdate Devices => devices;

        /// <summary>
        /// Returns all the <see cref="Thermostat"/> objects in the Nest account at the time of the update.
        /// </summary>
        /// <value">all the <see cref="Thermostat"/> objects in the Nest account at the time of the update.</value>
        public List<Thermostat> Thermostats => devices.Thermostats;

        /// <summary>
        /// Returns all the <see cref="SmokeCOAlarm"/> objects in the Nest account at the time of the update.
        /// </summary>
        /// <value">all the <see cref="SmokeCOAlarm"/> objects in the Nest account at the time of the update.</value>
        public List<SmokeCOAlarm> SmokeCOAlarms => devices.SmokeCOAlarms;

        /// <summary>
        /// Returns all the <see cref="Camera"/> objects in the Nest account at the time of the update.
        /// </summary>
        /// <value">all the <see cref="Camera"/> objects in the Nest account at the time of the update.</value>
        public List<Camera> Cameras => devices.Cameras;

        /// <summary>
        /// Returns all the <see cref="Structure"/> objects in the Nest account at the time of the update.
        /// </summary>
        /// <value">all the <see cref="Structure"/> objects in the Nest account at the time of the update.</value>
        public List<Structure> Structures => mStructures;

        /// <summary>
        /// Returns the <see cref="Metadata"/> object in the Nest account at the time of the update.
        /// </summary>
        /// <value">the <see cref="Metadata"/> object in the Nest account at the time of the update.</value>
        public Metadata Metadata => mMetadata;
    }
}
