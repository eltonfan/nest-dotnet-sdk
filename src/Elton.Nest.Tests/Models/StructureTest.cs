using System;
using System.Diagnostics;
using System.IO;
using Elton.Nest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elton.Nest.Tests.Models
{
    [TestClass]
    public class StructureTest : AbstractModelTest
    {
        public const string TEST_STRUCTURE_JSON = "/test-structure.json";
        public const string TEST_STRUCTURE_UNKNOWN_JSON = "/test-structure-unknown.json";

        [TestMethod]
        public void testCreateStructureWithJacksonMapper_shouldSetAllValuesCorrectly()
        {
            try
            {
                var json = LoadString(TEST_STRUCTURE_JSON);
                Structure structure = Parse<Structure>(json);

                Assert.AreEqual(structure.StructureId, "VqFabWH21nwVyd4RWgJgNb292wa7hG");
                Assert.AreEqual(structure.Thermostats.Count, 1);
                Assert.AreEqual(structure.Thermostats[0], "peyiJNo0IldT2YlIVtYaGQ");
                Assert.AreEqual(structure.SmokeCoAlarms.Count, 1);
                Assert.AreEqual(structure.SmokeCoAlarms[0], "RTMTKxsQTCxzVcsySOHPxKoF4OyCifrs");
                Assert.AreEqual(structure.Cameras.Count, 1);
                Assert.AreEqual(structure.Cameras[0], "awJo6rHX");
                Assert.AreEqual(structure.Devices.Count, 1);

                Assert.AreEqual(structure.Away, "home");
                Assert.AreEqual(structure.Name, "Home");
                Assert.AreEqual(structure.CountryCode, "US");
                Assert.AreEqual(structure.PostalCode, "94304");
                Assert.AreEqual(structure.PeakPeriodStartTime, "2015-10-31T23:59:59.000Z");
                Assert.AreEqual(structure.PeakPeriodEndTime, "2015-10-31T23:59:59.000Z");
                Assert.AreEqual(structure.TimeZone, "America/Los_Angeles");
                Assert.IsNotNull(structure.Eta);

                Structure.ETA eta = structure.Eta;
                Assert.AreEqual(eta.TripId, "myTripHome1024");
                Assert.AreEqual(eta.EstimatedArrivalWindowBegin, "2015-10-31T22:42:59.000Z");
                Assert.AreEqual(eta.EstimatedArrivalWindowEnd, "2015-10-31T23:59:59.000Z");

                Assert.AreEqual(structure.RhrEnrollment, true);

                Assert.IsNotNull(structure.Wheres);
                Assert.AreEqual(structure.Wheres.Count, 1);

                Structure.Where where = structure.Wheres["Fqp6wJIX"];
                Assert.IsNotNull(where);
                Assert.AreEqual(where.WhereId, "Fqp6wJIX");
                Assert.AreEqual(where.Name, "Bedroom");
            }
            catch (IOException e)
            {
                Trace.WriteLine(e.StackTrace);
                Assert.Fail();
            }
        }

        [TestMethod]
        public void testCreateStructureWithJacksonMapperUnknownProperty_shouldSetAllValuesCorrectly()
        {
            try
            {
                var json = LoadString(TEST_STRUCTURE_UNKNOWN_JSON);
                Structure structure = Parse<Structure>(json);

                Assert.AreEqual(structure.StructureId, "VqFabWH21nwVyd4RWgJgNb292wa7hG");
                Assert.AreEqual(structure.Thermostats.Count, 1);
                Assert.AreEqual(structure.Thermostats[0], "peyiJNo0IldT2YlIVtYaGQ");
                Assert.AreEqual(structure.SmokeCoAlarms.Count, 1);
                Assert.AreEqual(structure.SmokeCoAlarms[0], "RTMTKxsQTCxzVcsySOHPxKoF4OyCifrs");
                Assert.AreEqual(structure.Cameras.Count, 1);
                Assert.AreEqual(structure.Cameras[0], "awJo6rHX");
                Assert.AreEqual(structure.Devices.Count, 1);

                Assert.AreEqual(structure.Away, "home");
                Assert.AreEqual(structure.Name, "Home");
                Assert.AreEqual(structure.CountryCode, "US");
                Assert.AreEqual(structure.PostalCode, "94304");
                Assert.AreEqual(structure.PeakPeriodStartTime, "2015-10-31T23:59:59.000Z");
                Assert.AreEqual(structure.PeakPeriodEndTime, "2015-10-31T23:59:59.000Z");
                Assert.AreEqual(structure.TimeZone, "America/Los_Angeles");
                Assert.IsNotNull(structure.Eta);

                Structure.ETA eta = structure.Eta;
                Assert.AreEqual(eta.TripId, "myTripHome1024");
                Assert.AreEqual(eta.EstimatedArrivalWindowBegin, "2015-10-31T22:42:59.000Z");
                Assert.AreEqual(eta.EstimatedArrivalWindowEnd, "2015-10-31T23:59:59.000Z");

                Assert.AreEqual(structure.RhrEnrollment, true);

                Assert.IsNotNull(structure.Wheres);
                Assert.AreEqual(structure.Wheres.Count, 1);

                Structure.Where where = structure.Wheres["Fqp6wJIX"];
                Assert.IsNotNull(where);
                Assert.AreEqual(where.WhereId, "Fqp6wJIX");
                Assert.AreEqual(where.Name, "Bedroom");
            }
            catch (IOException e)
            {
                Trace.WriteLine(e.StackTrace);
                Assert.Fail();
            }
        }

        [TestMethod]
        public void testWhereNewArray_shouldReturnArrayOfCorrectSize()
        {
            int structureWheresSize = new Random().Next(9) + 1;

            //Structure.Where[] wheres = Structure.Where.CREATOR.newArray(structureWheresSize);
            //Assert.AreEqual(wheres.Length, structureWheresSize);
        }

        [TestMethod]
        public void testETANewArray_shouldReturnArrayOfCorrectSize()
        {
            int structureEtasSize = new Random().Next(9) + 1;

            //Structure.ETA[] etas = Structure.ETA.CREATOR.newArray(structureEtasSize);
            //Assert.AreEqual(etas.Length, structureEtasSize);
        }

        [TestMethod]
        public void testDescribeContents_shouldReturnZero()
        {
            Structure structure = new Structure();
            //Assert.AreEqual(structure.describeContents(), 0);
        }

        [TestMethod]
        public void testEtaDescribeContents_shouldReturnZero()
        {
            try
            {
                var json = LoadString(TEST_STRUCTURE_JSON);
                Structure structure = Parse<Structure>(json);
                //Assert.AreEqual(structure.Eta.describeContents(), 0);
            }
            catch (IOException e)
            {
                Trace.WriteLine(e.StackTrace);
                Assert.Fail();
            }
        }

        [TestMethod]
        public void testWhereDescribeContents_shouldReturnZero()
        {
            try
            {
                var json = LoadString(TEST_STRUCTURE_JSON);
                Structure structure = Parse<Structure>(json);
                var wheres = structure.Wheres;
                foreach (var whereId in wheres.Keys)
                {
                    //Assert.AreEqual(wheres[whereId].describeContents(), 0);
                }
            }
            catch (IOException e)
            {
                Trace.WriteLine(e.StackTrace);
                Assert.Fail();
            }
        }

        [TestMethod]
        public void testNewArray_shouldReturnArrayOfCorrectSize()
        {
            int structuresSize = new Random().Next(9) + 1;

            //Structure[] structures = Structure.CREATOR.newArray(structuresSize);
            //Assert.AreEqual(structures.Length, structuresSize);
        }

        [TestMethod]
        public void testEquals_shouldReturnFalseWithNonStructure()
        {
            Object o = new Object();
            Structure s = new Structure();
            Assert.IsFalse(s.Equals(o));
        }
    }
}