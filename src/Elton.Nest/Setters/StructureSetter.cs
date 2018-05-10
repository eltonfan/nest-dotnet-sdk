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
using Elton.Nest.Rest;
using Elton.Nest.Rest.Parsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Elton.Nest.Setters
{
    public class StructureSetter
    {
        private static string GetPath(string structureId)
        {
            return new Utils.PathBuilder()
                    .Append(Constants.KEY_STRUCTURES)
                    .Append(structureId)
                    .Build();
        }

        readonly RestClient restClient;
        public StructureSetter(RestClient restClient)
        {
            this.restClient = restClient;
        }

        /// <summary>
        /// Sets the state of the structure. In order for a structure to be in the Auto-Away state, all
        /// devices must also be in Auto-Away state. When any device leaves the Auto-Away state, then the
        /// structure also leaves the Auto-Away state.
        /// </summary>
        /// <param name="structureId">The unique identifier for the <see cref="Structure"/>.</param>
        /// <param name="awayState">The state of the structure. Values can be "home", "away", or "auto-away".</param>
        /// <param name="callback">A <see cref="Callback"/> to receive whether the change was successful.</param>
        public void SetAway(string structureId, AwayState awayState, Callback callback = null)
        {
            restClient.WriteString(GetPath(structureId), Structure.KEY_AWAY, awayState.GetValueString(), callback);
        }

        /// <summary>
        /// Sets the ETA on a structure. It is used to let Nest know that a user is expected to return
        /// home at a specific time.
        /// </summary>
        /// <param name="structureId">The unique identifier for the <see cref="Structure"/>.</param>
        /// <param name="eta">The <see cref="Structure.ETA"/> object containing the ETA values.</param>
        /// <param name="callback">A <see cref="Callback"/> to receive whether the change was successful.</param>
        public void SetEta(string structureId, Structure.ETA eta, Callback callback = null)
        {
            restClient.WriteString(GetPath(structureId), Structure.KEY_ETA, eta.ToString(), callback);
        }
    }
}
