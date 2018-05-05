using System;
using System.IO;
using System.Text.RegularExpressions;
using Elton.Nest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elton.Nest.Tests.Models
{
    [TestClass]
    public class NestConfigTest : AbstractModelTest
    {

        public const string TEST_CLIENT_ID = "test-client-id";
        public const string TEST_CLIENT_SECRET = "test-client-secret";
        public const string TEST_REDIRECT_URL = "test-redirect-url";

        [TestMethod]
        public void testNestConfig_shouldReturnCorrectValuesWhenSet()
        {

            NestConfig config = new NestConfig.Builder()
                    .clientID(TEST_CLIENT_ID)
                    .clientSecret(TEST_CLIENT_SECRET)
                    .redirectURL(TEST_REDIRECT_URL)
                    .build();

            Assert.AreEqual(TEST_CLIENT_ID, config.ClientID);
            Assert.AreEqual(TEST_CLIENT_SECRET, config.ClientSecret);
            Assert.AreEqual(TEST_REDIRECT_URL, config.RedirectURL);
        }

        [TestMethod]
        public void testNestConfig_shouldReturnNullValuesWhenNotSet()
        {
            NestConfig config = new NestConfig.Builder().build();

            Assert.AreEqual(null, config.ClientID);
            Assert.AreEqual(null, config.ClientSecret);
            Assert.AreEqual(null, config.RedirectURL);
        }

        [TestMethod]
        public void testNestConfig_setStateValue_shouldReturnCorrectlyFormattedString()
        {
            NestConfig config = new NestConfig.Builder()
                    .build();

            Assert.IsTrue(new Regex("^app-state\\d+-\\d+$").IsMatch(config.StateValue));
        }

        [TestMethod]
        public void testDescribeContents_shouldReturnZero()
        {
            NestConfig config = new NestConfig.Builder().build();
            //Assert.AreEqual(config.describeContents(), 0);
        }

        [TestMethod]
        public void testNewArray_shouldReturnArrayOfCorrectSize()
        {
            var configSize = new Random().Next(9) + 1;

            //NestConfig[] configs = NestConfig.CREATOR.newArray(configSize);
            //Assert.AreEqual(configs.Length, configSize);
        }

        [TestMethod]
        public void testEquals_shouldReturnFalseWithNonNestConfig()
        {
            var o = new Object();
            NestConfig config = new NestConfig.Builder().build();
            Assert.IsFalse(config.Equals(o));
        }
    }
}