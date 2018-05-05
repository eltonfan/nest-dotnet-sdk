using System;
using System.Diagnostics;
using System.IO;
using Elton.Nest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elton.Nest.Tests.Models
{
    [TestClass]
    public class MetadataAndroidTest : AbstractModelTest
    {
        public const string TEST_METADATA_JSON = "/test-metadata.json";

        [TestMethod]
        public void testMetadataToParcel()
        {
            try
            {
                var json = LoadString(TEST_METADATA_JSON);
                Metadata metadata = Parse<Metadata>(json);

                //Parcel parcel = Parcel.obtain();
                //metadata.writeToParcel(parcel, 0);

                //parcel.setDataPosition(0);

                //Metadata metadataFromParcel = Metadata.CREATOR.createFromParcel(parcel);
                //Assert.AreEqual(metadata, metadataFromParcel);
            }
            catch (IOException e)
            {
                Trace.WriteLine(e.StackTrace);
                Assert.Fail();
            }
        }
    }
}
