using System;
using System.IO;
using Elton.Nest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elton.Nest.Tests.Models
{
    [TestClass]
    public class NestTokenAndroidTest : AbstractModelTest
    {
        [TestMethod]
        public void testCameraToParcel()
        {
            string testToken = "test-token";
            long testExpiresIn = 123;

            NestToken token = new NestToken(testToken, testExpiresIn);

            //Parcel parcel = Parcel.obtain();
            //token.writeToParcel(parcel, 0);

            //parcel.setDataPosition(0);

            //NestToken tokenFromParcel = NestToken.CREATOR.createFromParcel(parcel);
            //Assert.AreEqual(token, tokenFromParcel);
        }
    }
}