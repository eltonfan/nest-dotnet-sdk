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
                    .SetClientId(TEST_CLIENT_ID)
                    .SetClientSecret(TEST_CLIENT_SECRET)
                    .SetRedirectUrl(TEST_REDIRECT_URL)
                    .Build();

            Assert.AreEqual(TEST_CLIENT_ID, config.ClientId);
            Assert.AreEqual(TEST_CLIENT_SECRET, config.ClientSecret);
            Assert.AreEqual(TEST_REDIRECT_URL, config.RedirectUrl);
        }

        [TestMethod]
        public void testNestConfig_shouldReturnNullValuesWhenNotSet()
        {
            NestConfig config = new NestConfig.Builder().Build();

            Assert.AreEqual(null, config.ClientId);
            Assert.AreEqual(null, config.ClientSecret);
            Assert.AreEqual(null, config.RedirectUrl);
        }

        [TestMethod]
        public void testNestConfig_setStateValue_shouldReturnCorrectlyFormattedString()
        {
            NestConfig config = new NestConfig.Builder()
                    .Build();

            Assert.IsTrue(new Regex("^app-state\\d+-\\d+$").IsMatch(config.StateValue));
        }

        [TestMethod]
        public void testDescribeContents_shouldReturnZero()
        {
            NestConfig config = new NestConfig.Builder().Build();
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
            NestConfig config = new NestConfig.Builder().Build();
            Assert.IsFalse(config.Equals(o));
        }
    }
}