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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace Elton.Nest.Rest.Parsers
{
    internal class CacheNode
    {
        readonly ConcurrentDictionary<string, CacheNode> dicNodes = new ConcurrentDictionary<string, CacheNode>();

        public string Name { get; set; }
        public string Value { get; set; }
        public CacheNode Parent { get; set; }
        public bool Created { get; set; }

        public CacheNode(CacheNode parent = null, string name = null, bool created = false)
        {
            this.Parent = parent;
            this.Name = name;
            this.Created = created;
        }

        public ConcurrentDictionary<string, CacheNode> Children => dicNodes;
    }

    public class ObjectModelMapper : Mapper
    {
        readonly StreamingEventHandler eventHandler;
        readonly CacheNode rootNode = new CacheNode(null, null, false);
        readonly object lockedObj = new object();
        readonly char[] _seperator = { '/' };
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

            eventHandler.HandleData(new GlobalUpdate(
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
                eventHandler.HandleError(error);
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
        public void Map(StreamingEvent eventData)
        {
            switch (eventData.EventType)
            {
                case "put":
                    mapData(eventData.Message);

                    //Updates the firebase cache.
                    //Note that the patch event is not supported in the Nest API.
                    using (var r = new StringReader(eventData.Message))
                    using (var reader = new JsonTextReader(r))
                    {
                        ReadToNamedPropertyValue(reader, "path");
                        reader.Read();

                        string path = reader.Value.ToString();
                        UpdateCache(path, ReadToNamedPropertyValue(reader, "data"));
                    }
                    break;
                case "auth_revoked":
                    eventHandler.HandleAuthRevoked();
                    break;
                case "error":
                    mapError(eventData.Message);
                    break;
            }
        }
        
        private JsonReader ReadToNamedPropertyValue(JsonReader reader, string property)
        {
            while (reader.Read() && reader.TokenType != JsonToken.PropertyName)
            {
                // skip the property
            }

            string prop = reader.Value.ToString();
            if (property != prop)
            {
                throw new InvalidOperationException("Error parsing response.  Expected json property named: " + property);
            }

            return reader;
        }

        public void UpdateCache(string path, JsonReader data, bool replace = false)
        {
            lock (lockedObj)
            {
                CacheNode root = FindRoot(path);
                UpdateChildren(root, data, replace);
            }
        }

        internal CacheNode Root => rootNode;

        CacheNode FindRoot(string path)
        {
            var segments = path.Split(_seperator, StringSplitOptions.RemoveEmptyEntries);
            var root = rootNode;
            foreach (string segment in segments)
                root = GetNamedChild(root, segment);

            return root;
        }


        private static CacheNode GetNamedChild(CacheNode root, string segment)
        {
            if (!root.Children.TryGetValue(segment, out CacheNode newRoot))
            {
                newRoot = new CacheNode { Name = segment, Parent = root, Created = true };
                root.Children.TryAdd(newRoot.Name, newRoot);
            }

            return newRoot;
        }

        private void UpdateChildren(CacheNode root, JsonReader reader, bool replace = false)
        {
            if (replace)
            {
                DeleteChild(root);

                // if we just deleted this, we need to wire it back up
                root.Parent?.Children.TryAdd(root.Name, root);
            }

            while (reader.Read())
            {
                switch (reader.TokenType)
                {
                    case JsonToken.PropertyName:
                        UpdateChildren(GetNamedChild(root, reader.Value.ToString()), reader);
                        break;
                    case JsonToken.Boolean:
                    case JsonToken.Bytes:
                    case JsonToken.Date:
                    case JsonToken.Float:
                    case JsonToken.Integer:
                    case JsonToken.String:
                        if (root.Created)
                        {
                            root.Value = reader.Value.ToString();
                            eventHandler.HandleValueAdded(PathFromRoot(root), reader.Value.ToString());
                            root.Created = false;
                        }
                        else
                        {
                            string oldData = root.Value;
                            string newData = reader.Value.ToString();
                            if (oldData != newData)
                            {
                                root.Value = newData;
                                eventHandler.HandleValueChanged(PathFromRoot(root), newData, oldData);
                            }
                        }

                        return;
                    case JsonToken.Null:
                        DeleteChild(root);
                        return;
                    default:
                        // do nothing
                        break;
                }
            }
        }

        private void DeleteChild(CacheNode root)
        {
            // if we're not the root, delete this from the parent
            if (root.Parent != null)
            {
                if(root.Parent?.Children.TryRemove(root.Name, out _)??false)
                {//exists
                    eventHandler.HandleValueRemoved(PathFromRoot(root));
                }
            }
            else
            {
                // we just cleared out the root - so delete all
                // the children one-by-one (so events fire in proper order)
                // we're modifying the collection, so ToArray
                foreach (var child in root.Children.Values.ToArray())
                {
                    child.Parent?.Children.TryRemove(child.Name, out _);
                    eventHandler.HandleValueRemoved(PathFromRoot(child));
                }
            }
        }

        // dont' need a lock since access is serialized
        readonly LinkedList<CacheNode> _pathFromRootList = new LinkedList<CacheNode>();
        private string PathFromRoot(CacheNode root)
        {
            // track the sizeso when we allocate our builder we get the right size up front
            int size = 1;

            while (root.Name != null)
            {
                size += root.Name.Length + 1;
                _pathFromRootList.AddFirst(root);
                root = root.Parent;

            }

            if (_pathFromRootList.Count == 0)
            {
                return "/";
            }

            StringBuilder sb = new StringBuilder(size);
            foreach (CacheNode d in _pathFromRootList)
            {
                sb.AppendFormat("/{0}", d.Name);
            }

            _pathFromRootList.Clear();

            return sb.ToString();
        }
    }
}