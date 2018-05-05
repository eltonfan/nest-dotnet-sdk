using System;
using System.Diagnostics;
using System.IO;
using Elton.Nest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elton.Nest.Tests.Models
{
    [TestClass]
    public class ThermostatAndroidTest : AbstractModelTest
    {
        public const string TEST_THERMOSTAT_JSON = "/test-thermostat.json";

        [TestMethod]
        public void testThermostatToParcel()
        {
            try
            {
                var json = LoadString(TEST_THERMOSTAT_JSON);
                Thermostat thermostat = Parse<Thermostat>(json);

                //Parcel parcel = Parcel.obtain();
                //thermostat.writeToParcel(parcel, 0);

                //parcel.setDataPosition(0);

                //Thermostat thermostatFromParcel = Thermostat.CREATOR.createFromParcel(parcel);
                //Assert.AreEqual(thermostat, thermostatFromParcel);

            }
            catch (IOException e)
            {
                Trace.WriteLine(e.StackTrace);
                Assert.Fail();
            }
        }
    }
}