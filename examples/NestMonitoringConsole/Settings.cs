#region License

//   Copyright 2014 Elton FAN (eltonfan@live.cn, http://elton.io)
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

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NestMonitoringConsole
{
    public class Settings
    {
        readonly string configPath = null;
        public Settings(string configPath)
        {
            this.configPath = configPath;
        }

        public string ReadJson(string name)
        {
            var configFile = Path.Combine(configPath, name + ".json");

            if (!File.Exists(configFile))
                return default;

            return File.ReadAllText(configFile, Encoding.UTF8);
        }

        public void WriteJson(string name, string jsonString)
        {
            var configFile = Path.Combine(configPath, name + ".json");
            File.WriteAllText(configFile, jsonString, Encoding.UTF8);
        }

        public T Read<T>(string name)
        {
            var jsonString = ReadJson(name);
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public void Write<T>(string name, T value)
        {
            var jsonString = JsonConvert.SerializeObject(value, Formatting.Indented);
            WriteJson(name, jsonString);
        }

        public string ConfigPath => configPath;
    }
}
