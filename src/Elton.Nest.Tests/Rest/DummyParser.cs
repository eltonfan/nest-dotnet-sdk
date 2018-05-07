using System;
using System.IO;
using Elton.Nest.Models;
using Elton.Nest.Rest.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elton.Nest.Tests.Models
{
    [TestClass]
    public class DummyParser : Parser
    {
        string message = null;
        public void Parse(string message)
        {
            this.message = message;
        }
    }
}