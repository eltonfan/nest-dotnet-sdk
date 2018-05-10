using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Elton.Nest
{
    public enum AwayState
    {
        [EnumMember(Value = "home")]
        Home,
        [EnumMember(Value = "away")]
        Away,
        [EnumMember(Value = "auto-away")]
        AutoAway,
    }
}
