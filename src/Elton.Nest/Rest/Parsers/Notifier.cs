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
        readonly List<NestListener> listeners = new List<NestListener>();

        public void addListener(NestListener listener)
        {
            listeners.Add(listener);
        }

        public void removeListener(NestListener listener)
        {
            listeners.Remove(listener);
        }

        public void removeAllListeners()
        {
            listeners.Clear();
        }

        public void handleData(GlobalUpdate eventData)
        {
            foreach (var listener in listeners)
            {
                if (listener is GlobalListener)
                    ((GlobalListener)listener).onUpdate(eventData);
                else if (listener is DeviceListener)
                    ((DeviceListener)listener).onUpdate(eventData.Devices);
                else if (listener is StructureListener)
                    ((StructureListener)listener).onUpdate(eventData.Structures);
                else if (listener is ThermostatListener)
                    ((ThermostatListener)listener).onUpdate(eventData.Thermostats);
                else if (listener is CameraListener)
                    ((CameraListener)listener).onUpdate(eventData.Cameras);
                else if (listener is SmokeCOAlarmListener)
                    ((SmokeCOAlarmListener)listener).onUpdate(eventData.SmokeCOAlarms);
                else if (listener is MetadataListener)
                    ((MetadataListener)listener).onUpdate(eventData.Metadata);
            }
        }

        public void handleError(ErrorMessage errorMessage)
        {
            bool authError = (errorMessage.Error == "unauthorized");

            foreach (var listener in listeners)
            {
                if (listener is AuthListener && authError)
                {
                    ((AuthListener)listener).onAuthFailure(new NestException(errorMessage.Message));
                }
                else if (listener is ErrorListener && !authError)
                {
                    ((ErrorListener)listener).onError(errorMessage);
                }
            }
        }

        public void handleAuthRevoked()
        {
            foreach (var listener in listeners)
            {
                if (listener is AuthListener)
                {
                    ((AuthListener)listener).onAuthRevoked();
                }
            }
        }
    }
}