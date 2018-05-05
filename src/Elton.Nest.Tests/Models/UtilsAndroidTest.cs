using System;
using System.IO;
using Elton.Nest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elton.Nest.Tests.Models
{
    [TestClass]
    public class UtilsAndroidTest : AbstractModelTest
    {
        [TestMethod]
        public void testIsAnyEmptyWithNoArgs_shouldReturnFalse()
        {
            Assert.IsFalse(Utils.isAnyEmpty());
        }

        [TestMethod]
        public void testIsAnyEmptyWithEmptyArgs_shouldReturnTrue()
        {
            Assert.IsTrue(Utils.isAnyEmpty("", ""));
        }

        [TestMethod]
        public void testIsAnyEmptyWithNoEmptyArgs_shouldReturnFalse()
        {
            Assert.IsFalse(Utils.isAnyEmpty("not-empty", "also-not-empty"));
        }
    }
}
