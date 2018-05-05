using System;
using System.Diagnostics;
using System.IO;
using Elton.Nest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elton.Nest.Tests.Models
{
    [TestClass]
    public class MetadataTest : AbstractModelTest
    {
        public const string TEST_METADATA_JSON = "/test-metadata.json";
        public const string TEST_EMPTY_METADATA_JSON = "/test-empty-metadata.json";
        public const string TEST_METADATA_UNKNOWN_JSON = "/test-metadata-unknown.json";

        [TestMethod]
        public void testCreateMetadataWithJacksonMapper_shouldSetAllValuesCorrectly()
        {
            try
            {
                string json = LoadString(TEST_METADATA_JSON);
                Metadata metadata = Parse<Metadata>(json);

                Assert.AreEqual(metadata.AccessToken, "c.FmDPkzyzaQeX");
                Assert.AreEqual(metadata.ClientVersion, 1);
            }
            catch (IOException e)
            {
                Trace.WriteLine(e.StackTrace);
                Assert.Fail();
            }
        }

        [TestMethod]
        public void testCreateMetadataWithJacksonMapperUnknownProperties_shouldSetAllValuesCorrectly()
        {
            try
            {
                string json = LoadString(TEST_METADATA_UNKNOWN_JSON);
                Metadata metadata = Parse<Metadata>(json);

                Assert.AreEqual(metadata.AccessToken, "c.FmDPkzyzaQeX");
                Assert.AreEqual(metadata.ClientVersion, 1);
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
            Metadata metadata = new Metadata();
            //Assert.AreEqual(metadata.describeContents(), 0);
        }

        [TestMethod]
        public void testNewArray_shouldReturnArrayOfCorrectSize()
        {
            int metadatasSize = new Random().Next(9) + 1;

            //Metadata[] metadatas = Metadata.CREATOR.newArray(metadatasSize);
            //Assert.AreEqual(metadatas.Length, metadatasSize);
        }

        [TestMethod]
        public void testEquals_shouldReturnFalseWithNonMetadata()
        {
            Object o = new Object();
            Metadata metadata = new Metadata();
            Assert.IsFalse(metadata.Equals(o));
        }

        [TestMethod]
        public void testToString_shouldReturnNicelyFormattedString()
        {
            Metadata metadata = new Metadata();
            try
            {
                string json = LoadString(TEST_EMPTY_METADATA_JSON);
                Assert.AreEqual(json.Trim(), metadata.ToString());
            }
            catch (IOException e)
            {
                Trace.WriteLine(e.StackTrace);
                Assert.Fail();
            }
        }
    }
}
