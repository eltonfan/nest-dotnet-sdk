using System;
using System.IO;
using Elton.Nest.Models;
using Elton.Nest.Rest.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elton.Nest.Tests.Models
{
    [TestClass]
    public class MessageParserTest : AbstractModelTest
    {
        private class DummyMapper : Mapper
        {
            internal StreamingEvent eventData = null;

            public void Map(StreamingEvent eventData)
            {
                this.eventData = eventData;
            }
        }

        [TestMethod]
        public void testParse_shouldExitIfEmpty()
        {
            var mapper = new DummyMapper();
            var parser = new MessageParser(mapper);

            parser.Parse(null);
            Assert.IsNull(mapper.eventData);

            parser.Parse("");
            Assert.IsNull(mapper.eventData);
        }

        [TestMethod]
        public void testParse_shouldSkipMalformedMessages()
        {
            var mapper = new DummyMapper();
            var parser = new MessageParser(mapper);

            parser.Parse("garbage test");
            Assert.IsNull(mapper.eventData);

            parser.Parse("event: dummy");
            Assert.IsNull(mapper.eventData);

            parser.Parse("event: put");
            Assert.IsNull(mapper.eventData);
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void testParse_throwParserExceptionOnInvalidEventFormat()
        {
            var mapper = new DummyMapper();
            var parser = new MessageParser(mapper);

            parser.Parse("event: \ndata: {}");
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void testParse_throwParserExceptionOnInvalidDataFormat()
        {
            var mapper = new DummyMapper();
            var parser = new MessageParser(mapper);

            parser.Parse("event: put\ndata: ");
        }

        [TestMethod]
        public void testParse_shouldParsePutRevokeEvents()
        {
            var mapper = new DummyMapper();
            var parser = new MessageParser(mapper);

            string data = "{data}";

            parser.Parse("event: put\ndata: " + data);
            Assert.IsNotNull(mapper.eventData);
            Assert.AreEqual(mapper.eventData.EventType, "put");
            Assert.AreEqual(mapper.eventData.Message, data);

            parser.Parse("event: auth_revoked\ndata: " + data);
            Assert.IsNotNull(mapper.eventData);
            Assert.AreEqual(mapper.eventData.EventType, "auth_revoked");
            Assert.AreEqual(mapper.eventData.Message, data);
        }

        [TestMethod]
        public void testParse_shouldParseErrorEvent()
        {
            var mapper = new DummyMapper();
            var parser = new MessageParser(mapper);

            string error = "{\"error\":";

            parser.Parse(error);
            Assert.IsNotNull(mapper.eventData);
            Assert.AreEqual(mapper.eventData.EventType, "error");
            Assert.AreEqual(mapper.eventData.Message, error);
        }
    }
}
