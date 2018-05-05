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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace Elton.Nest.Rest.Parsers
{
    public class MessageParser : Parser
    {
        readonly Mapper mapper;

        public MessageParser(Mapper mapper)
        {
            this.mapper = mapper;
        }

        /// <summary>
        /// May contain one or more events
        /// </summary>
        /// <exception cref="ParserException"></exception>
        public void parse(string msg)
        {
            if (string.IsNullOrEmpty(msg))
                return;

            var lines = msg.Split('\n');
            int i = 0;
            while (i < lines.Length)
            {
                var currentLine = lines[i];
                if (currentLine.StartsWith("{\"error\":"))
                {
                    mapper.map(new StreamingEvent("error", currentLine));
                }
                else if (currentLine.StartsWith("event:") && lines.Length > i + 1)
                {
                    var nextLine = lines[i + 1];
                    if (currentLine.Length <= 8)
                        throw new ParserException("Unexpected length of event line.");

                    if (nextLine.Length <= 7)
                        throw new ParserException("Unexpected length of data line.");

                    var eventType = currentLine.Substring(7); //7 = length of("event: ")
                    var json = nextLine.Substring(6); //6 = length of("data: ")

                    mapper.map(new StreamingEvent(eventType, json));
                    i++;
                }
                i++;
            }
        }
    }
}