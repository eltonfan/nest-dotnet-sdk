using System;
using System.Diagnostics;
using System.IO;
using Elton.Nest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elton.Nest.Tests.Models
{
    [TestClass]
    public class StructureAndroidTest : AbstractModelTest
    {
        public const string TEST_STRUCTURE_JSON = "/test-structure.json";

        [TestMethod]
        public void testStructureToParcel()
        {
            try
            {
                var json = LoadString(TEST_STRUCTURE_JSON);
                Structure structure = Parse<Structure>(json);
                /*
                Parcel parcel = Parcel.obtain();
                structure.writeToParcel(parcel, 0);

                parcel.setDataPosition(0);

                Structure structureFromParcel = Structure.CREATOR.createFromParcel(parcel);
                Assert.AreEqual(structure, structureFromParcel);

                Assert.AreEqual(structureFromParcel.StructureId, "VqFabWH21nwVyd4RWgJgNb292wa7hG");
                Assert.AreEqual(structureFromParcel.Thermostats.Count, 1);
                Assert.AreEqual(structureFromParcel.Thermostats[0], "peyiJNo0IldT2YlIVtYaGQ");
                Assert.AreEqual(structureFromParcel.SmokeCoAlarms.Count, 1);
                Assert.AreEqual(structureFromParcel.SmokeCoAlarms[0],
                        "RTMTKxsQTCxzVcsySOHPxKoF4OyCifrs");
                Assert.AreEqual(structureFromParcel.Cameras.Count, 1);
                Assert.AreEqual(structureFromParcel.Cameras[0], "awJo6rHX");
                Assert.AreEqual(structureFromParcel.Devices.Count, 1);

                Assert.AreEqual(structureFromParcel.Away, "home");
                Assert.AreEqual(structureFromParcel.Name, "Home");
                Assert.AreEqual(structureFromParcel.CountryCode, "US");
                Assert.AreEqual(structureFromParcel.PostalCode, "94304");
                Assert.AreEqual(structureFromParcel.PeakPeriodStartTime, "2015-10-31T23:59:59.000Z");
                Assert.AreEqual(structureFromParcel.PeakPeriodEndTime, "2015-10-31T23:59:59.000Z");
                Assert.AreEqual(structureFromParcel.TimeZone, "America/Los_Angeles");
                Assert.IsNotNull(structureFromParcel.Eta);

                Structure.ETA eta = structureFromParcel.Eta;
                Assert.AreEqual(eta.TripId, "myTripHome1024");
                Assert.AreEqual(eta.EstimatedArrivalWindowBegin, "2015-10-31T22:42:59.000Z");
                Assert.AreEqual(eta.EstimatedArrivalWindowEnd, "2015-10-31T23:59:59.000Z");

                Assert.AreEqual(structureFromParcel.RhrEnrollment, true);

                Assert.IsNotNull(structureFromParcel.Wheres);
                Assert.AreEqual(structureFromParcel.Wheres.Count, 1);

                Structure.Where where = structureFromParcel.Wheres["Fqp6wJIX"];
                Assert.IsNotNull(where);
                Assert.AreEqual(where.WhereId, "Fqp6wJIX");
                Assert.AreEqual(where.Name, "Bedroom");
                */
            }
            catch (IOException e)
            {
                Trace.WriteLine(e.StackTrace);
                Assert.Fail();
            }
        }
    }
}