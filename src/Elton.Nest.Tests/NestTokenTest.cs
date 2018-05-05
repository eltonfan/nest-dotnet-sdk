using System;
using System.IO;
using Elton.Nest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elton.Nest.Tests.Models
{
    [TestClass]
    public class NestTokenTest : AbstractModelTest
    {

        public const string TEST_TOKEN = "test-token";
        public const long TEST_EXPIRES_IN = 123;

        [TestMethod]
        public void testNestToken_shouldReturnCorrectValuesWhenSet()
        {
            var token = new NestToken(TEST_TOKEN, TEST_EXPIRES_IN);

            Assert.AreEqual(TEST_TOKEN, token.Token);
            Assert.AreEqual(TEST_EXPIRES_IN, token.ExpiresInSecs);
        }

        [TestMethod]
        public void testNestToken_shouldReturnNullTokenWhenNotSet()
        {
            var token = new NestToken();

            Assert.AreEqual(null, token.Token);
        }

        [TestMethod]
        public void testNestToken_shouldReturnZeroExpiresInWhenNotSet()
        {
            var token = new NestToken();

            Assert.AreEqual(0, token.ExpiresInSecs);
        }

        [TestMethod]
        public void testDescribeContents_shouldReturnZero()
        {
            var t = new NestToken();
            //Assert.AreEqual(t.describeContents(), 0);
        }

        [TestMethod]
        public void testNewArray_shouldReturnArrayOfCorrectSize()
        {
            var tokenSize = new Random().Next(9) + 1;

            //NestToken[] tokens = NestToken.CREATOR.newArray(tokenSize);
            //Assert.AreEqual(tokens.Length, tokenSize);
        }

        [TestMethod]
        public void testEquals_shouldReturnFalseWithNonNestToken()
        {
            var o = new Object();
            var t = new NestToken();
            Assert.IsFalse(t.Equals(o));
        }
    }
}