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
using System;
using System.Collections.Generic;
using System.Text;

namespace Elton.Nest.Rest.Parsers
{
    public class Notifier : StreamingEventHandler, IDisposable
    {
        public event EventHandler<NestGlobalEventArgs> GlobalUpdated;
        public event EventHandler<NestDeviceEventArgs> DeviceUpdated;
        public event EventHandler<NestStructureEventArgs> StructureUpdated;
        public event EventHandler<NestThermostatEventArgs> ThermostatUpdated;
        public event EventHandler<NestCameraEventArgs> CameraUpdated;
        public event EventHandler<NestSmokeCOAlarmEventArgs> SmokeCOAlarmUpdated;
        public event EventHandler<NestMetadataEventArgs> MetadataUpdated;

        public event EventHandler<NestAuthFailureEventArgs> AuthFailure;
        public event EventHandler<NestErrorEventArgs> Error;

        public event EventHandler<NestAuthRevokedEventArgs> AuthRevoked;


        public event EventHandler<ValueAddedEventArgs> ValueAdded;
        public event EventHandler<ValueChangedEventArgs> ValueChanged;
        public event EventHandler<ValueRemovedEventArgs> ValueRemoved;

        public void HandleData(GlobalUpdate eventData)
        {
            GlobalUpdated?.Invoke(this, new NestGlobalEventArgs(eventData));
            DeviceUpdated?.Invoke(this, new NestDeviceEventArgs(eventData.Devices));
            StructureUpdated?.Invoke(this, new NestStructureEventArgs(eventData.Structures));
            ThermostatUpdated?.Invoke(this, new NestThermostatEventArgs(eventData.Thermostats));
            CameraUpdated?.Invoke(this, new NestCameraEventArgs(eventData.Cameras));
            SmokeCOAlarmUpdated?.Invoke(this, new NestSmokeCOAlarmEventArgs(eventData.SmokeCOAlarms));
            MetadataUpdated?.Invoke(this, new NestMetadataEventArgs(eventData.Metadata));
        }

        public void HandleError(ErrorMessage errorMessage)
        {
            bool authError = (errorMessage.Error == "unauthorized");
            if (authError)
                AuthFailure?.Invoke(this, new NestAuthFailureEventArgs(new NestException(errorMessage.Message)));
            else
                Error?.Invoke(this, new NestErrorEventArgs(errorMessage));
        }

        public void HandleAuthRevoked()
        {
            AuthRevoked?.Invoke(this, new NestAuthRevokedEventArgs());
        }

        public void HandleValueAdded(string path, string data)
        {
            ValueAdded?.Invoke(this, new ValueAddedEventArgs(path, data));
        }

        public void HandleValueChanged(string path, string data, string oldData)
        {
            ValueChanged?.Invoke(this, new ValueChangedEventArgs(path, data, oldData));
        }

        public void HandleValueRemoved(string path)
        {
            ValueRemoved?.Invoke(this, new ValueRemovedEventArgs(path));
        }

        public void Dispose()
        {
            GlobalUpdated = null;
            DeviceUpdated = null;
            StructureUpdated = null;
            ThermostatUpdated = null;
            CameraUpdated = null;
            SmokeCOAlarmUpdated = null;
            MetadataUpdated = null;

            AuthFailure = null;
            Error = null;
            AuthRevoked = null;

            ValueAdded = null;
            ValueChanged = null;
            ValueRemoved = null;
        }
    }
}