using System;
using System.Collections.Generic;
using Elton.Nest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elton.Nest.Tests.Models
{
    [TestClass]
    public class GlobalUpdateTest : AbstractModelTest
    {
        [TestMethod]
        public void testNestGlobalUpdate_shouldReturnSameObjectInGetters()
        {
            var testThermos = new List<Thermostat>();
            var testSmokeAlarms = new List<SmokeCOAlarm>();
            var testCams = new List<Camera>();
            var testStructures = new List<Structure>();
            var testMetadata = new Metadata();

            var update = new GlobalUpdate(testThermos, testSmokeAlarms, testCams,
                    testStructures, testMetadata);

            Assert.AreSame(testThermos, update.Thermostats);
            Assert.AreSame(testSmokeAlarms, update.SmokeCOAlarms);
            Assert.AreSame(testCams, update.Cameras);
            Assert.AreSame(testStructures, update.Structures);
            Assert.AreSame(testMetadata, update.Metadata);
        }
    }
}
