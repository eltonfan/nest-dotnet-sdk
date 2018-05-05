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
    /// An Exception for returning encountered errors in using {@link NestClient}.
    /// </summary>
    public class NestException : Exception
    {
        /// <summary>
        /// Constructs a new {@code NestException} that includes the current stack trace.
        /// </summary>
        public NestException() : base() { }

        /// <summary>
        /// Constructs a new {@code NestException} with the current stack trace and the specified detail
        /// message.
        ///
        /// @param message the detail message for this exception.
        /// </summary>
        public NestException(String message) : base(message) { }

        /// <summary>
        /// Constructs a new {@code NestException} with the current stack trace, the specified detail
        /// message and the specified cause.
        ///
        /// @param message the detail message for this exception.
        /// @param innerException     the cause of this exception.
        /// </summary>
        public NestException(String message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Constructs a new {@code NestException} with the current stack trace and the specified cause.
        ///
        /// @param innerException the cause of this exception.
        /// </summary>
        public NestException(Exception innerException) : base(null, innerException) { }
    }
}