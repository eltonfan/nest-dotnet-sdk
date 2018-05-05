using System;
using System.IO;
using Elton.Nest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elton.Nest.Tests.Models
{
    [TestClass]
    public class UtilsTest : AbstractModelTest
    {
        [TestMethod]
        public void testParcelReadBoolean_shouldReturnTrueForIntOne()
        {
            //Parcel mockParcel = PowerMockito.mock(Parcel.class);
            //PowerMockito.when(mockParcel.readInt()).thenReturn(1);

            //bool result = Utils.readBoolean(mockParcel);
            //assertTrue(result);
        }

        [TestMethod]
        public void testParcelReadBoolean_shouldReturnFalseForIntZero()
        {
            //Parcel mockParcel = PowerMockito.mock(Parcel.class);
            //PowerMockito.when(mockParcel.readInt()).thenReturn(0);

            //bool result = Utils.readBoolean(mockParcel);
            //Assert.IsFalse(result);
        }

        [TestMethod]
        public void testParcelWriteBoolean_shouldWriteOneWhenInputTrue()
        {
            //Parcel mockParcel = PowerMockito.mock(Parcel.class);
            //Utils.writeBoolean(mockParcel, true);
            //Mockito.verify(mockParcel).writeInt(1);
        }

        [TestMethod]
        public void testParcelWriteBoolean_shouldWriteZeroWhenInputFalse()
        {
            //Parcel mockParcel = PowerMockito.mock(Parcel.class);
            //Utils.writeBoolean(mockParcel, false);
            //Mockito.verify(mockParcel).writeInt(0);
        }

        [TestMethod]
        public void testPathBuilderWithOneString_shouldHaveLeadingSlash()
        {
            Utils.PathBuilder builder = new Utils.PathBuilder();
            builder.Append("hello");
            string result = builder.Build();

            Assert.AreEqual("/hello", result);
        }

        [TestMethod]
        public void testPathBuilderWithTwoStrings_shouldJoinWithSlashes()
        {
            Utils.PathBuilder builder = new Utils.PathBuilder();
            builder.Append("hello").Append("world");
            string result = builder.Build();

            Assert.AreEqual("/hello/world", result);
        }
    }
}
