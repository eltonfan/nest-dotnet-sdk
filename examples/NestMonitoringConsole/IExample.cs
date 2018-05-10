using Elton.Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NestMonitoringConsole
{
    public interface IExample
    {
        void Execute(NestClient nest);
    }

    public class ExampleAttribute : Attribute
    {
        public string Name { get; private set; }
        public ExampleAttribute(string name = default)
            : base()
        {
            this.Name = name;
        }
    }
}
