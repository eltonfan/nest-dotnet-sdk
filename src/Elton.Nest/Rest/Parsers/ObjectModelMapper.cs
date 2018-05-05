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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace Elton.Nest.Rest.Parsers
{

    public class ObjectModelMapper : Mapper
    {
        readonly StreamingEventHandler eventHandler;

        public ObjectModelMapper(StreamingEventHandler handler)
        {
            this.eventHandler = handler;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ParserException"></exception>
        void Deserialize(JObject node, Dictionary<string, IList> lists, string key, Type objectType)
        {
            try
            {
                var obj = node.ToObject(objectType);
                lists[key].Add(obj);
            }
            catch (Exception ex)
            {
                throw new ParserException(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ParserException"></exception>
        void Parse(JObject node, Dictionary<string, IList> lists, string key, Type objectType)
        {
            foreach (var pair in node)
            {
                if (!(pair.Value is JObject current))
                    continue;

                switch (pair.Key)
                {
                    case Constants.KEY_DEVICES:
                    case "data":
                        Parse(current, lists, null, null);
                        break;
                    case Constants.KEY_STRUCTURES:
                        Parse(current, lists, Constants.KEY_STRUCTURES, typeof(Structure));
                        break;
                    case Constants.KEY_THERMOSTATS:
                        Parse(current, lists, Constants.KEY_THERMOSTATS, typeof(Thermostat));
                        break;
                    case Constants.KEY_CAMERAS:
                        Parse(current, lists, Constants.KEY_CAMERAS, typeof(Camera));
                        break;
                    case Constants.KEY_SMOKE_CO_ALARMS:
                        Parse(current, lists, Constants.KEY_SMOKE_CO_ALARMS, typeof(SmokeCOAlarm));
                        break;
                    case Constants.KEY_METADATA:
                        Deserialize(current, lists, Constants.KEY_METADATA, typeof(Metadata));
                        break;
                    default:
                        if (key != null && objectType != null)
                        {
                            Deserialize(current, lists, key, objectType);
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ParserException"></exception>
        private void mapData(string json)
        {
            var thermostats = new List<Thermostat>();
            var cameras = new List<Camera>();
            var smokeAlarms = new List<SmokeCOAlarm>();
            var structures = new List<Structure>();
            var metadata = new List<Metadata>();

            var lists = new Dictionary<string, IList>();
            lists.Add(Constants.KEY_THERMOSTATS, thermostats);
            lists.Add(Constants.KEY_STRUCTURES, structures);
            lists.Add(Constants.KEY_CAMERAS, cameras);
            lists.Add(Constants.KEY_SMOKE_CO_ALARMS, smokeAlarms);
            lists.Add(Constants.KEY_METADATA, metadata);

            try
            {
                var node = JToken.Parse(json) as JObject;
                Parse(node, lists, null, null);
            }
            catch (Exception e)
            {
                throw new ParserException(e);
            }

            eventHandler.handleData(new GlobalUpdate(
                    thermostats, smokeAlarms, cameras, structures,
                    metadata.FirstOrDefault()));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ParserException"></exception>
        private void mapError(string json)
        {
            try
            {
                var error = JsonConvert.DeserializeObject<ErrorMessage>(json);
                eventHandler.handleError(error);
            }
            catch (JsonSerializationException e)
            {
                throw new ParserException(e);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ParserException"></exception>
        public void map(StreamingEvent eventData)
        {
            switch (eventData.EventType)
            {
                case "put":
                    mapData(eventData.Message);
                    break;
                case "auth_revoked":
                    eventHandler.handleAuthRevoked();
                    break;
                case "error":
                    mapError(eventData.Message);
                    break;
            }
        }
    }
}