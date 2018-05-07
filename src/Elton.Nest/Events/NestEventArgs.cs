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
using Elton.Nest.Rest.Parsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Elton.Nest
{
    /// <summary>
    /// NestEventArgs are listeners that receive events from the NestClient and allow a user to complete
    /// actions when those events occur.
    /// </summary>
    public class NestEventArgs : EventArgs
    {

    }

    /// <summary>
    /// Listens for updates to any objects in a user's Nest account, including all devices,
    /// structures and metadata.
    /// </summary>
    public class NestGlobalEventArgs : NestEventArgs
    {
        public GlobalUpdate Data { get; private set; }
        /// <summary>
        /// Called when an update occurs on any device, structure or metadata object.
        /// </summary>
        /// <param name="update">a <see cref="GlobalUpdate"/> object containing all values at the time of the update.</param>
        public NestGlobalEventArgs(GlobalUpdate update)
        {
            this.Data = update;
        }
    }

    /// <summary>
    /// Listens for updates on all devices in a user's Nest account.
    /// </summary>
    public class NestDeviceEventArgs : NestEventArgs
    {
        public DeviceUpdate Data { get; private set; }
        /// <summary>
        /// Called when an update occurs on any device object.
        /// </summary>
        /// <param name="update">a <see cref="DeviceUpdate"/> object containing all devices at the time of the update.</param>
        public NestDeviceEventArgs(DeviceUpdate update)
        {
            this.Data = update;
        }
    }

    /// <summary>
    /// Listens for updates to any <see cref="Camera"/> in a user's Nest account.
    /// </summary>
    public class NestCameraEventArgs : NestEventArgs
    {
        public List<Camera> Data { get; private set; }
        /// <summary>
        /// Called when an update occurs on any <see cref="Camera"/> device.
        /// </summary>
        /// <param name="cameras">
        /// an <see cref="List"/> of all <see cref="Camera"/> objects in the user's account
        /// at the time of the update.
        /// </param>
        public NestCameraEventArgs(List<Camera> cameras)
        {
            this.Data = cameras;
        }
    }

    /// <summary>
    /// Listens for updates to any <see cref="Thermostat"/> in a user's Nest account.
    /// </summary>
    public class NestThermostatEventArgs : NestEventArgs
    {
        public List<Thermostat> Data { get; private set; }
        /// <summary>
        /// Called when an update occurs on any <see cref="Thermostat"/> device.
        /// </summary>
        /// <param name="thermostats">
        /// an <see cref="List"/> of all <see cref="Thermostat"/> objects in the user's
        /// account at the time of the update.
        /// </param>
        public NestThermostatEventArgs(List<Thermostat> thermostats)
        {
            this.Data = thermostats;
        }
    }

    /// <summary>
    /// Listens for updates to any <see cref="Structure"/> in a user's Nest account.
    /// </summary>
    public class NestStructureEventArgs : NestEventArgs
    {
        public List<Structure> Data { get; private set; }
        /// <summary>
        /// Called when an update occurs on any <see cref="Structure"/> object.
        /// </summary>
        /// <param name="structures">
        /// an <see cref="List"/> of all <see cref="Structure"/> objects in the user's
        /// account at the time of the update.
        /// </param>
        public NestStructureEventArgs(List<Structure> structures)
        {
            this.Data = structures;
        }
    }

    /// <summary>
    /// Listens for updates to any <see cref="SmokeCOAlarm"/> in a user's Nest account.
    /// </summary>
    public class NestSmokeCOAlarmEventArgs : NestEventArgs
    {
        public List<SmokeCOAlarm> Data { get; private set; }
        /// <summary>
        /// Called when an update occurs on any <see cref="SmokeCOAlarm"/> device.
        /// </summary>
        /// <param name="smokeCOAlarms">
        /// an <see cref="List"/> of all <see cref="SmokeCOAlarm"/> objects in the
        /// user's account at the time of the update.
        /// </param>
        public NestSmokeCOAlarmEventArgs(List<SmokeCOAlarm> smokeCOAlarms)
        {
            this.Data = smokeCOAlarms;
        }
    }

    /// <summary>
    /// Listens for updates to the <see cref="Metadata"/> object in a user's Nest account.
    /// </summary>
    public class NestMetadataEventArgs : NestEventArgs
    {
        public Metadata Data { get; private set; }
        /// <summary>
        /// Called when an update occurs on the <see cref="Metadata"/> object.
        /// </summary>
        /// <param name="metadata">the <see cref="Metadata"/> object in user's account at the time of the update.</param>
        public NestMetadataEventArgs(Metadata metadata)
        {
            this.Data = metadata;
        }
    }

    /// <summary>
    /// Listens for updates to the status of authentication of <see cref="NestApiUrls"/> to the Nest service.
    /// </summary>
    public class NestAuthFailureEventArgs : NestEventArgs
    {
        public NestException Exception { get; private set; }
        /// <summary>
        /// Called when the authentication with the token fails. An exception is returned that can
        /// either be thrown or read to determine the cause of the error.
        /// </summary>
        /// <param name="exception">a <see cref="NestException"/> object containing the error that occurred.</param>
        public NestAuthFailureEventArgs(NestException exception)
        {
            this.Exception = exception;
        }
    }

    public class NestAuthRevokedEventArgs : NestEventArgs
    {
        /// <summary>
        /// Called when a previously authenticated connection becomes unauthenticated. This usually
        /// occurs if the access token is revoked or has expired.
        /// </summary>
        public NestAuthRevokedEventArgs()
        {
        }
    }


    public class NestErrorEventArgs : NestEventArgs
    {
        public ErrorMessage Error { get; private set; }
        public NestErrorEventArgs(ErrorMessage errorMessage)
        {
            this.Error = errorMessage;
        }
    }
}
