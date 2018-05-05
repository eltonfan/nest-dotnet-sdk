using System;
using System.Diagnostics;
using System.IO;
using Elton.Nest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elton.Nest.Tests.Models
{
    [TestClass]
    public class ThermostatTest : AbstractModelTest
    {
        public const string TEST_THERMOSTAT_JSON = "/test-thermostat.json";
        public const string TEST_THERMOSTAT_UNKNOWN_JSON = "/test-thermostat-unknown.json";
        public const string TEST_EMPTY_THERMOSTAT = "/test-empty-thermostat.json";

        [TestMethod]
        public void testCreateThermostatWithJacksonMapper_shouldSetAllValuesCorrectly()
        {
            try
            {
                var json = LoadString(TEST_THERMOSTAT_JSON);
                Thermostat thermostat = Parse<Thermostat>(json);

                Assert.AreEqual(thermostat.DeviceId, "peyiJNo0IldT2YlIVtYaGQ");
                Assert.AreEqual(thermostat.SoftwareVersion, "4.0");
                Assert.AreEqual(thermostat.StructureId, "VqFabWH21nwVyd4R...");
                Assert.AreEqual(thermostat.WhereId, "UNCBGUnN24");
                Assert.AreEqual(thermostat.Name, "Hallway (upstairs)");
                Assert.AreEqual(thermostat.NameLong, "Hallway Thermostat (upstairs)");
                Assert.AreEqual(thermostat.IsOnline, true);

                Assert.AreEqual(thermostat.Locale, "en-US");
                Assert.AreEqual(thermostat.LastConnection, "2015-10-31T23:59:59.000Z");
                Assert.AreEqual(thermostat.CanCool, true);
                Assert.AreEqual(thermostat.CanHeat, true);
                Assert.AreEqual(thermostat.IsUsingEmergencyHeat, true);
                Assert.AreEqual(thermostat.HasFan, true);
                Assert.AreEqual(thermostat.FanTimerActive, true);
                Assert.AreEqual(thermostat.FanTimerTimeout, "2015-10-31T23:59:59.000Z");
                Assert.AreEqual(thermostat.HasLeaf, true);
                Assert.AreEqual(thermostat.TemperatureScale, "C");
                Assert.AreEqual(thermostat.TargetTemperatureF, 72);
                Assert.AreEqual(thermostat.TargetTemperatureC, 21.5, 0.01);
                Assert.AreEqual(thermostat.TargetTemperatureHighF, 72);
                Assert.AreEqual(thermostat.TargetTemperatureHighC, 21.5, 0.01);
                Assert.AreEqual(thermostat.TargetTemperatureLowF, 64);
                Assert.AreEqual(thermostat.TargetTemperatureLowC, 17.5, 0.01);
                Assert.AreEqual(thermostat.AwayTemperatureHighF, 72);
                Assert.AreEqual(thermostat.AwayTemperatureHighC, 21.5, 0.01);
                Assert.AreEqual(thermostat.AwayTemperatureLowF, 64);
                Assert.AreEqual(thermostat.AwayTemperatureLowC, 17.5, 0.01);
                Assert.AreEqual(thermostat.HvacMode, "heat");
                Assert.AreEqual(thermostat.AmbientTemperatureF, 72);
                Assert.AreEqual(thermostat.AmbientTemperatureC, 21.5, 0.01);
                Assert.AreEqual(thermostat.Humidity, 40);
                Assert.AreEqual(thermostat.HvacState, "heating");
                Assert.AreEqual(thermostat.IsLocked, true);
                Assert.AreEqual(thermostat.LockedTempMinF, "65");
                Assert.AreEqual(thermostat.LockedTempMaxF, "80");
                Assert.AreEqual(thermostat.LockedTempMinC, "19.5");
                Assert.AreEqual(thermostat.LockedTempMaxC, "24.5");
                Assert.AreEqual(thermostat.Label, "upstairs");
            }
            catch (IOException e)
            {
                Trace.WriteLine(e.StackTrace);
                Assert.Fail();
            }
        }

        [TestMethod]
        public void testCreateThermostatWithJacksonMapperUnknownProperty_shouldSetAllValuesCorrectly()
        {
            try
            {
                var json = LoadString(TEST_THERMOSTAT_UNKNOWN_JSON);
                Thermostat thermostat = Parse<Thermostat>(json);

                Assert.AreEqual(thermostat.DeviceId, "peyiJNo0IldT2YlIVtYaGQ");
                Assert.AreEqual(thermostat.SoftwareVersion, "4.0");
                Assert.AreEqual(thermostat.StructureId, "VqFabWH21nwVyd4R...");
                Assert.AreEqual(thermostat.WhereId, "UNCBGUnN24");
                Assert.AreEqual(thermostat.Name, "Hallway (upstairs)");
                Assert.AreEqual(thermostat.NameLong, "Hallway Thermostat (upstairs)");
                Assert.AreEqual(thermostat.IsOnline, true);

                Assert.AreEqual(thermostat.Locale, "en-US");
                Assert.AreEqual(thermostat.LastConnection, "2015-10-31T23:59:59.000Z");
                Assert.AreEqual(thermostat.CanCool, true);
                Assert.AreEqual(thermostat.CanHeat, true);
                Assert.AreEqual(thermostat.IsUsingEmergencyHeat, true);
                Assert.AreEqual(thermostat.HasFan, true);
                Assert.AreEqual(thermostat.FanTimerActive, true);
                Assert.AreEqual(thermostat.FanTimerTimeout, "2015-10-31T23:59:59.000Z");
                Assert.AreEqual(thermostat.HasLeaf, true);
                Assert.AreEqual(thermostat.TemperatureScale, "C");
                Assert.AreEqual(thermostat.TargetTemperatureF, 72);
                Assert.AreEqual(thermostat.TargetTemperatureC, 21.5, 0.01);
                Assert.AreEqual(thermostat.TargetTemperatureHighF, 72);
                Assert.AreEqual(thermostat.TargetTemperatureHighC, 21.5, 0.01);
                Assert.AreEqual(thermostat.TargetTemperatureLowF, 64);
                Assert.AreEqual(thermostat.TargetTemperatureLowC, 17.5, 0.01);
                Assert.AreEqual(thermostat.AwayTemperatureHighF, 72);
                Assert.AreEqual(thermostat.AwayTemperatureHighC, 21.5, 0.01);
                Assert.AreEqual(thermostat.AwayTemperatureLowF, 64);
                Assert.AreEqual(thermostat.AwayTemperatureLowC, 17.5, 0.01);
                Assert.AreEqual(thermostat.HvacMode, "heat");
                Assert.AreEqual(thermostat.AmbientTemperatureF, 72);
                Assert.AreEqual(thermostat.AmbientTemperatureC, 21.5, 0.01);
                Assert.AreEqual(thermostat.Humidity, 40);
                Assert.AreEqual(thermostat.HvacState, "heating");
                Assert.AreEqual(thermostat.IsLocked, true);
                Assert.AreEqual(thermostat.LockedTempMinF, "65");
                Assert.AreEqual(thermostat.LockedTempMaxF, "80");
                Assert.AreEqual(thermostat.LockedTempMinC, "19.5");
                Assert.AreEqual(thermostat.LockedTempMaxC, "24.5");
                Assert.AreEqual(thermostat.Label, "upstairs");
            }
            catch (IOException e)
            {
                Trace.WriteLine(e.StackTrace);
                Assert.Fail();
            }
        }

        [TestMethod]
        public void testDescribeContents_shouldReturnZero()
        {
            Thermostat t = new Thermostat();
            //Assert.AreEqual(t.describeContents(), 0);
        }

        [TestMethod]
        public void testNewArray_shouldReturnArrayOfCorrectSize()
        {
            int thermostatsSize = new Random().Next(9) + 1;

            //Thermostat[] thermostats = Thermostat.CREATOR.newArray(thermostatsSize);
            //Assert.AreEqual(thermostats.Length, thermostatsSize);
        }

        [TestMethod]
        public void testEquals_shouldReturnFalseWithNonThermostat()
        {
            Object o = new Object();
            Thermostat t = new Thermostat();
            Assert.IsFalse(t.Equals(o));
        }

        [TestMethod]
        public void testToString_shouldReturnNicelyFormattedString()
        {
            Thermostat t = new Thermostat();
            try
            {
                var json = LoadString(TEST_EMPTY_THERMOSTAT);
                Assert.AreEqual(json.Trim(), t.ToString());
            }
            catch (IOException e)
            {
                Trace.WriteLine(e.StackTrace);
                Assert.Fail();
            }
        }
    }
}