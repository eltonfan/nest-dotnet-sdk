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
    /**
     * DeviceUpdate contains the state of all devices in the Nest account when a change is detected in
     * any device. A DeviceUpdate object is returned by {@link com.nestlabs.sdk.NestListener
     * .DeviceListener#onUpdate(DeviceUpdate)} when an update occurs.
     */
    public class DeviceUpdate
    {
        readonly List<Thermostat> mThermostats;
        readonly List<SmokeCOAlarm> mSmokeCOAlarms;
        readonly List<Camera> mCameras;

        public DeviceUpdate(List<Thermostat> thermostats, List<SmokeCOAlarm> smokeCOAlarms, List<Camera> cameras)
        {
            mThermostats = thermostats;
            mSmokeCOAlarms = smokeCOAlarms;
            mCameras = cameras;
        }

        /**
         * Returns all the {@link Thermostat} objects in the Nest account at the time of the update.
         *
         * @return all the {@link Thermostat} objects in the Nest account at the time of the update.
         */
        public List<Thermostat> Thermostats => mThermostats;
        /**
         * Returns all the {@link SmokeCOAlarm} objects in the Nest account at the time of the update.
         *
         * @return all the {@link SmokeCOAlarm} objects in the Nest account at the time of the update.
         */
        public List<SmokeCOAlarm> SmokeCOAlarms => mSmokeCOAlarms;
        /**
         * Returns all the {@link Camera} objects in the Nest account at the time of the update.
         *
         * @return all the {@link Camera} objects in the Nest account at the time of the update.
         */
        public List<Camera> Cameras => mCameras;
    }
}
