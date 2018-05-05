using System;
using System.Collections.Generic;
using System.IO;
using Elton.Nest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elton.Nest.Tests.Models
{
    [TestClass]
    public class DeviceUpdateTest : AbstractModelTest
    {
        [TestMethod]
        public void testDeviceUpdate_shouldReturnSameObjectInGetters()
        {
            var testThermos = new List<Thermostat>();
            var testSmokeAlarms = new List<SmokeCOAlarm>();
            var testCams = new List<Camera>();

            var update = new DeviceUpdate(testThermos, testSmokeAlarms, testCams);

            Assert.AreSame(testThermos, update.Thermostats);
            Assert.AreSame(testSmokeAlarms, update.SmokeCOAlarms);
            Assert.AreSame(testCams, update.Cameras);
        }
    }
}