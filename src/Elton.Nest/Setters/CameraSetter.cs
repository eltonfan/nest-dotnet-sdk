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
    public class CameraSetter
    {
        static string getPath(string cameraId)
        {
            return new Utils.PathBuilder()
                    .Append(Constants.KEY_DEVICES)
                    .Append(Constants.KEY_CAMERAS)
                    .Append(cameraId)
                    .Build();
        }

        readonly RestClient restClient;
        public CameraSetter(RestClient restClient)
        {
            this.restClient = restClient;
        }

        /// <summary>
        /// Sets the {@link Camera} streaming status on or off.
        ///
        /// @param cameraId    The unique identifier of the camera.
        /// @param isStreaming true to turn streaming on, false to turn streaming off.
        /// @param callback    A {@link Callback} to receive whether the change was successful.
        /// </summary>
        public void setIsStreaming(string cameraId, bool isStreaming, Callback callback)
        {
            restClient.WriteBoolean(getPath(cameraId), Camera.KEY_IS_STREAMING, isStreaming, callback);
        }

        /// <summary>
        /// Sets the {@link Camera} streaming status on or off.
        ///
        /// @param cameraId    The unique identifier of the camera.
        /// @param isStreaming true to turn streaming on, false to turn streaming off.
        /// </summary>
        public void setIsStreaming(string cameraId, bool isStreaming)
        {
            setIsStreaming(cameraId, isStreaming, null);
        }
    }
}
