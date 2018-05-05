using System;
using System.Diagnostics;
using System.IO;
using Elton.Nest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elton.Nest.Tests.Models
{
    [TestClass]
    public class SmokeCOAlarmAndroidTest : AbstractModelTest
    {
        private const string TEST_SMOKE_ALARM_JSON = "/test-smoke-alarm.json";
        [TestMethod]
        public void testSmokeCoAlarmToParcel()
        {
            try
            {
                var json = LoadString(TEST_SMOKE_ALARM_JSON);
                SmokeCOAlarm smokeCOAlarm = Parse<SmokeCOAlarm>(json);

                //Parcel parcel = Parcel.obtain();
                //smokeCOAlarm.writeToParcel(parcel, 0);

                //parcel.setDataPosition(0);

                //SmokeCOAlarm cameraFromParcel = SmokeCOAlarm.CREATOR.createFromParcel(parcel);
                //Assert.AreEqual(smokeCOAlarm, cameraFromParcel);
            }
            catch (IOException e)
            {
                Trace.WriteLine(e.StackTrace);
                Assert.Fail();
            }
        }
    }
}