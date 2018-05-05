using System;
using System.Diagnostics;
using System.IO;
using Elton.Nest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elton.Nest.Tests.Models
{
    [TestClass]
    public class DeviceAndroidTest : AbstractModelTest
    {
        public const string TEST_DEVICE_JSON = "/test-nest-device.json";

        [TestMethod]
        public void testNestDeviceToParcel()
        {
            try
            {
                var json = LoadString(TEST_DEVICE_JSON);
                Device device = Parse<Device>(json);

                //Parcel parcel = Parcel.obtain();
                //device.writeToParcel(parcel, 0);

                //parcel.setDataPosition(0);

                //Device deviceFromParcel = Device.CREATOR.createFromParcel(parcel);
                //Assert.AreEqual(device, deviceFromParcel);
            }
            catch (IOException e)
            {
                Trace.WriteLine(e.StackTrace);
                Assert.Fail();
            }
        }
    }
}