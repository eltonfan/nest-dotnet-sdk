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
    public class Notifier : StreamingEventHandler
    {
        public event EventHandler<GlobalEventArgs> GlobalUpdated;
        public event EventHandler<DeviceEventArgs> DeviceUpdated;
        public event EventHandler<StructureEventArgs> StructureUpdated;
        public event EventHandler<ThermostatEventArgs> ThermostatUpdated;
        public event EventHandler<CameraEventArgs> CameraUpdated;
        public event EventHandler<SmokeCOAlarmEventArgs> SmokeCOAlarmUpdated;
        public event EventHandler<MetadataEventArgs> MetadataUpdated;

        public event EventHandler<AuthFailureEventArgs> AuthFailure;
        public event EventHandler<ErrorEventArgs> Error;

        public event EventHandler<AuthRevokedEventArgs> AuthRevoked;

        public void handleData(GlobalUpdate eventData)
        {
            GlobalUpdated?.Invoke(this, new GlobalEventArgs(eventData));
            DeviceUpdated?.Invoke(this, new DeviceEventArgs(eventData.Devices));
            StructureUpdated?.Invoke(this, new StructureEventArgs(eventData.Structures));
            ThermostatUpdated?.Invoke(this, new ThermostatEventArgs(eventData.Thermostats));
            CameraUpdated?.Invoke(this, new CameraEventArgs(eventData.Cameras));
            SmokeCOAlarmUpdated?.Invoke(this, new SmokeCOAlarmEventArgs(eventData.SmokeCOAlarms));
            MetadataUpdated?.Invoke(this, new MetadataEventArgs(eventData.Metadata));
        }

        public void handleError(ErrorMessage errorMessage)
        {
            bool authError = (errorMessage.Error == "unauthorized");
            if (authError)
                AuthFailure?.Invoke(this, new AuthFailureEventArgs(new NestException(errorMessage.Message)));
            else
                Error?.Invoke(this, new ErrorEventArgs(errorMessage));
        }

        public void handleAuthRevoked()
        {
            AuthRevoked?.Invoke(this, new AuthRevokedEventArgs());
        }
    }
}