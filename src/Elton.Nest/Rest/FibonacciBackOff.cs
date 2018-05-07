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

namespace Elton.Nest.Rest
{
    /// <summary>
    /// Exponential <see cref="BackOff"/> implementation that starts with initial delay and follows Fibonacci sequence
    /// up to maximum delay.
    /// </summary>
    public sealed class FibonacciBackOff : BackOff
    {
        const long DEFAULT_DELAY_MILLIS = 60000;
        const long DEFAULT_INITIAL_DELAY_MILLIS = 1000;

        private long lastDelay = 0;
        private int f1 = 0;
        private int f2 = 1;
        readonly long maxDelayMillis;
        readonly long initialDelayMillis;

        private FibonacciBackOff(Builder builder)
        {
            maxDelayMillis = builder.MaxDelayMillis;
            initialDelayMillis = builder.InitialDelayMillis;
        }

        public long NextInterval()
        {
            if (lastDelay < maxDelayMillis)
            {
                //Next Fibonacci#
                int temp = f2;
                f2 = f1 + f2;
                f1 = temp;

                lastDelay = f1 * initialDelayMillis;
                if (lastDelay > maxDelayMillis) lastDelay = maxDelayMillis;
            }
            return lastDelay;
        }

        public void Reset()
        {
            lastDelay = 0;
            f1 = 0;
            f2 = 1;
        }

        public class Builder
        {
            private long maxDelayMillis = DEFAULT_DELAY_MILLIS;
            private long initialDelayMillis = DEFAULT_INITIAL_DELAY_MILLIS;

            public long MaxDelayMillis => maxDelayMillis;
            public long InitialDelayMillis => initialDelayMillis;

            public Builder setMaxDelayMillis(long maxDelayMillis)
            {
                this.maxDelayMillis = maxDelayMillis;
                return this;
            }


            public Builder setInitialDelayMillis(long initialDelayMillis)
            {
                this.initialDelayMillis = initialDelayMillis;
                return this;
            }

            public FibonacciBackOff build()
            {
                return new FibonacciBackOff(this);
            }
        }
    }
}
