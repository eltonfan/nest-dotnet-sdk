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

using System;
using System.Collections.Generic;
using System.Text;

namespace Elton.Nest
{
    /// <summary>
    /// Callback is used when setting values using the NestClient. You can pass in a new instance of an
    /// anonymous class implementing Callback and get notified when the result of setting the value is returned.
    /// </summary>
    public class Callback
    {

        /// <summary>
        /// onSuccess is called when the action succeeded without error.
        /// </summary>
        public Action OnSuccess { get; set; }

        /// <summary>
        /// onFailure is called when the action failed due to some error.
        /// </summary>
        public ExceptionHandler OnFailure { get; set; }

        public Callback(Action OnSuccess = default, ExceptionHandler OnFailure = default)
        {
            this.OnSuccess = OnSuccess;
            this.OnFailure = OnFailure;
        }
    }

}
