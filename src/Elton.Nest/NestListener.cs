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
    /// NestListener are listeners that receive events from the NestClient and allow a user to complete
    /// actions when those events occur.
    /// </summary>
    public interface NestListener
    {

    }

    /// <summary>
    /// Listens for updates to any objects in a user's Nest account, including all devices,
    /// structures and metadata.
    /// </summary>
    public interface GlobalListener : NestListener
    {

        /// <summary>
        /// Called when an update occurs on any device, structure or metadata object.
        ///
        /// @param update a {@link GlobalUpdate} object containing all values at the time of the
        ///               update.
        /// </summary>
        void onUpdate(GlobalUpdate update);
    }

    /// <summary>
    /// Listens for updates on all devices in a user's Nest account.
    /// </summary>
    public interface DeviceListener : NestListener
    {
        /// <summary>
        /// Called when an update occurs on any device object.
        ///
        /// @param update a {@link DeviceUpdate} object containing all devices at the time of the
        ///               update.
        /// </summary>
        void onUpdate(DeviceUpdate update);
    }

    /// <summary>
    /// Listens for updates to any {@link Camera} in a user's Nest account.
    /// </summary>
    public interface CameraListener : NestListener
    {
        /// <summary>
        /// Called when an update occurs on any {@link Camera} device.
        ///
        /// @param cameras an {@link List} of all {@link Camera} objects in the user's account
        ///                at the time of the update.
        /// </summary>
        void onUpdate(List<Camera> cameras);
    }

    /// <summary>
    /// Listens for updates to any {@link Thermostat} in a user's Nest account.
    /// </summary>
    public interface ThermostatListener : NestListener
    {
        /// <summary>
        /// Called when an update occurs on any {@link Thermostat} device.
        ///
        /// @param thermostats an {@link List} of all {@link Thermostat} objects in the user's
        ///                    account at the time of the update.
        /// </summary>
        void onUpdate(List<Thermostat> thermostats);
    }

    /// <summary>
    /// Listens for updates to any {@link Structure} in a user's Nest account.
    /// </summary>
    public interface StructureListener : NestListener
    {
        /// <summary>
        /// Called when an update occurs on any {@link Structure} object.
        ///
        /// @param structures an {@link List} of all {@link Structure} objects in the user's
        ///                   account at the time of the update.
        /// </summary>
        void onUpdate(List<Structure> structures);
    }

    /// <summary>
    /// Listens for updates to any {@link SmokeCOAlarm} in a user's Nest account.
    /// </summary>
    public interface SmokeCOAlarmListener : NestListener
    {
        /// <summary>
        /// Called when an update occurs on any {@link SmokeCOAlarm} device.
        ///
        /// @param smokeCOAlarms an {@link List} of all {@link SmokeCOAlarm} objects in the
        ///                      user's account at the time of the update.
        /// </summary>
        void onUpdate(List<SmokeCOAlarm> smokeCOAlarms);
    }

    /// <summary>
    /// Listens for updates to the {@link Metadata} object in a user's Nest account.
    /// </summary>
    public interface MetadataListener : NestListener
    {
        /// <summary>
        /// Called when an update occurs on the {@link Metadata} object.
        ///
        /// @param metadata the {@link Metadata} object in user's account at the time of the update.
        /// </summary>
        void onUpdate(Metadata metadata);
    }

    /// <summary>
    /// Listens for updates to the status of authentication of {@link WwnApiUrls} to the Nest service.
    /// </summary>
    public interface AuthListener : NestListener
    {
        /// <summary>
        /// Called when the authentication with the token fails. An exception is returned that can
        /// either be thrown or read to determine the cause of the error.
        ///
        /// @param exception a {@link NestException} object containing the error that occurred.
        /// </summary>
        void onAuthFailure(NestException exception);

        /// <summary>
        /// Called when a previously authenticated connection becomes unauthenticated. This usually
        /// occurs if the access token is revoked or has expired.
        /// </summary>
        void onAuthRevoked();
    }

    public interface ErrorListener : NestListener
    {
        void onError(ErrorMessage errorMessage);
    }
}