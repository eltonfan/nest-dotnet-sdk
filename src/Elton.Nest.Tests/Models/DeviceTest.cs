using System;
using System.IO;
using Elton.Nest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elton.Nest.Tests.Models
{
    [TestClass]
    public class DeviceTest : AbstractModelTest
    {
        [TestMethod]
        public void testDescribeContents_shouldReturnZero()
        {
            Device device = new Device();
            //Assert.AreEqual(device.describeContents(), 0);
        }

        [TestMethod]
        public void testNewArray_shouldReturnArrayOfCorrectSize()
        {
            int devicesSize = new Random().Next(9) + 1;

            //Device[] devices = Device.CREATOR.newArray(devicesSize);
            //Assert.AreEqual(devices.Length, devicesSize);
        }

        [TestMethod]
        public void testEquals_shouldReturnFalseWithNonNestDevice()
        {
            Object o = new Object();
            Device device = new Device();
            Assert.IsFalse(device.Equals(o));
        }
    }
}