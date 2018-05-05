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
using System.Threading;
using System.Threading.Tasks;

namespace Elton.Nest.Rest
{
    internal class RetryExecutor
    {
        readonly System.Timers.Timer timer = new System.Timers.Timer();
        readonly BackOff backOff;

        internal RetryExecutor(BackOff backOff)
        {
            this.backOff = backOff;
        }

        internal void reset()
        {
            backOff.reset();
        }

        internal void schedule<T>(Action<T> consumer, T value)
        {
            long delay = backOff.nextInterval();

            timer.Elapsed += (sender, args) =>
            {
                consumer(value);
            };
            timer.AutoReset = true;
            timer.Interval = delay;
            timer.Start();
        }

        internal void cancel()
        {
            timer.Stop();
        }
    }
}