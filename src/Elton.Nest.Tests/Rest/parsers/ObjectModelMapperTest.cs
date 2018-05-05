using System;
using System.IO;
using Elton.Nest.Models;
using Elton.Nest.Rest.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elton.Nest.Tests.Models
{
    [TestClass]
    public class ObjectModelMapperTest : AbstractModelTest
    {
        const string TEST_GLOBAL_UPDATE_JSON = "/test-global-update.json";

        private class DummyEventHandler : StreamingEventHandler
        {

            internal ErrorMessage error = null;
            internal GlobalUpdate updateEvent = null;
            internal Boolean authRevokedEvent = false;


            public void handleData(GlobalUpdate eventData)
            {
                updateEvent = eventData;
            }


            public void handleError(ErrorMessage errorMessage)
            {
                error = errorMessage;
            }


            public void handleAuthRevoked()
            {
                authRevokedEvent = true;
            }
        }

        [TestMethod]
        public void testMap_shouldSkipUnknownEvent()
        {
            var handler = new DummyEventHandler();
            var mapper = new ObjectModelMapper(handler);

            mapper.map(new StreamingEvent("unknown", ""));
            Assert.IsNull(handler.error);
            Assert.IsNull(handler.updateEvent);
            Assert.IsFalse(handler.authRevokedEvent);
        }

        [TestMethod]
        public void testMap_shouldTriggerAuthRevokedEvent()
        {
            var handler = new DummyEventHandler();
            var mapper = new ObjectModelMapper(handler);

            mapper.map(new StreamingEvent("auth_revoked", ""));
            Assert.IsNull(handler.error);
            Assert.IsNull(handler.updateEvent);
            Assert.IsTrue(handler.authRevokedEvent);
        }

        [TestMethod]
        public void testMap_shouldParseErrorEvent()
        {
            //Should also ignore unknown fields
            var handler = new DummyEventHandler();
            var mapper = new ObjectModelMapper(handler);

            String json = "{\"error\":\"unauthorized\",\"type\":\"auth-error\",\"message\":\"unauthorized\",\"instance\":\"2372e4af-c774-495f-b485-5e6a81aa27fe\",\"unknown\":\"ignore\"}";
            mapper.map(new StreamingEvent("error", json));
            Assert.IsNotNull(handler.error);
            Assert.AreEqual(handler.error.Error, "unauthorized");
            Assert.AreEqual(handler.error.Type, "auth-error");
            Assert.IsNull(handler.updateEvent);
            Assert.IsFalse(handler.authRevokedEvent);
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void testMap_shouldFailToParseErrorEvent()
        {
            var handler = new DummyEventHandler();
            var mapper = new ObjectModelMapper(handler);

            String json = "{\"error\":\"unauthorized\"";
            mapper.map(new StreamingEvent("error", json));
        }

        [TestMethod]
        public void testMap_shouldParseStructureEvent()
        {
            var handler = new DummyEventHandler();
            var mapper = new ObjectModelMapper(handler);

            String json = LoadString(TEST_GLOBAL_UPDATE_JSON);

            mapper.map(new StreamingEvent("put", json));
            Assert.IsNull(handler.error);
            Assert.IsFalse(handler.authRevokedEvent);
            GlobalUpdate eventData = handler.updateEvent;
            Assert.IsNotNull(eventData);
            Assert.IsNotNull(eventData.Devices);
            Assert.IsNotNull(eventData.Metadata);
            Assert.IsNotNull(eventData.Structures);
            Assert.IsNotNull(eventData.Devices.Cameras);
            Assert.IsNotNull(eventData.Devices.SmokeCOAlarms);
            Assert.IsNotNull(eventData.Devices.Thermostats);
            Assert.AreEqual(eventData.Devices.Cameras.Count, 1);
            Assert.AreEqual(eventData.Devices.SmokeCOAlarms.Count, 1);
            Assert.AreEqual(eventData.Devices.Thermostats.Count, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void testMap_shouldFaileToParseStructureEvent()
        {
            var handler = new DummyEventHandler();
            var mapper = new ObjectModelMapper(handler);

            mapper.map(new StreamingEvent("put", "{\"devices\":\"cameras\":{}"));
        }
    }
}