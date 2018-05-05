using System;
using System.Diagnostics;
using System.IO;
using Elton.Nest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elton.Nest.Tests.Models
{
    [TestClass]
    public class SmokeCOAlarmTest : AbstractModelTest
    {
        public const string TEST_SMOKEALARM_JSON = "/test-smoke-alarm.json";
        public const string TEST_SMOKEALARM_UNKNOWN_JSON = "/test-smoke-alarm-unknown.json";
        public const string TEST_EMPTY_THERMOSTAT = "/test-empty-smoke-alarm.json";

        [TestMethod]
        public void testCreateSmokeAlarmWithJacksonMapper_shouldSetAllValuesCorrectly()
        {
            try
            {
                var json = LoadString(TEST_SMOKEALARM_JSON);
                SmokeCOAlarm smokeCOAlarm = Parse<SmokeCOAlarm>(json);

                Assert.AreEqual(smokeCOAlarm.DeviceId, "RTMTKxsQTCxzVcsySOHPxKoF4OyCifrs");
                Assert.AreEqual(smokeCOAlarm.SoftwareVersion, "1.01");
                Assert.AreEqual(smokeCOAlarm.StructureId, "VqFabWH21nwVyd4RWgJgNb292wa7hG");
                Assert.AreEqual(smokeCOAlarm.WhereId, "UNCBGUnN24");
                Assert.AreEqual(smokeCOAlarm.Name, "Hallway (upstairs)");
                Assert.AreEqual(smokeCOAlarm.NameLong, "Hallway Protect (upstairs)");
                Assert.AreEqual(smokeCOAlarm.IsOnline, true);
                Assert.AreEqual(smokeCOAlarm.WhereId, "UNCBGUnN24");
                Assert.AreEqual(smokeCOAlarm.Locale, "en-US");

                Assert.AreEqual(smokeCOAlarm.BatteryHealth, "ok");
                Assert.AreEqual(smokeCOAlarm.CoAlarmState, "ok");
                Assert.AreEqual(smokeCOAlarm.SmokeAlarmState, "ok");
                Assert.AreEqual(smokeCOAlarm.UiColorState, "gray");
                Assert.AreEqual(smokeCOAlarm.IsManualTestActive, true);
                Assert.AreEqual(smokeCOAlarm.LastManualTestTime, "2015-10-31T23:59:59.000Z");

            }
            catch (IOException e)
            {
                Trace.WriteLine(e.StackTrace);
                Assert.Fail();
            }
        }

        [TestMethod]
        public void testCreateSmokeAlarmWithJacksonMapperUnknownProperty_shouldSetAllValuesCorrectly()
        {
            try
            {
                var json = LoadString(TEST_SMOKEALARM_UNKNOWN_JSON);
                SmokeCOAlarm smokeCOAlarm = Parse<SmokeCOAlarm>(json);

                Assert.AreEqual(smokeCOAlarm.DeviceId, "RTMTKxsQTCxzVcsySOHPxKoF4OyCifrs");
                Assert.AreEqual(smokeCOAlarm.SoftwareVersion, "1.01");
                Assert.AreEqual(smokeCOAlarm.StructureId, "VqFabWH21nwVyd4RWgJgNb292wa7hG");
                Assert.AreEqual(smokeCOAlarm.WhereId, "UNCBGUnN24");
                Assert.AreEqual(smokeCOAlarm.Name, "Hallway (upstairs)");
                Assert.AreEqual(smokeCOAlarm.NameLong, "Hallway Protect (upstairs)");
                Assert.AreEqual(smokeCOAlarm.IsOnline, true);
                Assert.AreEqual(smokeCOAlarm.WhereId, "UNCBGUnN24");
                Assert.AreEqual(smokeCOAlarm.Locale, "en-US");

                Assert.AreEqual(smokeCOAlarm.BatteryHealth, "ok");
                Assert.AreEqual(smokeCOAlarm.CoAlarmState, "ok");
                Assert.AreEqual(smokeCOAlarm.SmokeAlarmState, "ok");
                Assert.AreEqual(smokeCOAlarm.UiColorState, "gray");
                Assert.AreEqual(smokeCOAlarm.IsManualTestActive, true);
                Assert.AreEqual(smokeCOAlarm.LastManualTestTime, "2015-10-31T23:59:59.000Z");

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
            SmokeCOAlarm smokeCOAlarm = new SmokeCOAlarm();
            //Assert.AreEqual(smokeCOAlarm.describeContents(), 0);
        }

        [TestMethod]
        public void testNewArray_shouldReturnArrayOfCorrectSize()
        {
            int smokealarmsSize = new Random().Next(9) + 1;

            //SmokeCOAlarm[] smokeCOAlarms = SmokeCOAlarm.CREATOR.newArray(smokealarmsSize);
            //Assert.AreEqual(smokeCOAlarms.Length, smokealarmsSize);
        }

        [TestMethod]
        public void testEquals_shouldReturnFalseWithNonSmokeCoAlarm()
        {
            Object o = new Object();
            SmokeCOAlarm t = new SmokeCOAlarm();
            Assert.IsFalse(t.Equals(o));
        }

        [TestMethod]
        public void testToString_shouldReturnNicelyFormattedString()
        {
            SmokeCOAlarm t = new SmokeCOAlarm();
            try
            {
                var json = LoadString(TEST_EMPTY_THERMOSTAT);
                Assert.AreEqual(json, t.ToString());
            }
            catch (IOException e)
            {
                Trace.WriteLine(e.StackTrace);
                Assert.Fail();
            }
        }
    }
}