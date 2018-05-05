using System;
using System.Diagnostics;
using System.IO;
using Elton.Nest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace Elton.Nest.Tests.Models
{
    [TestClass]
    public class NestConfigAndroidTest : AbstractModelTest
    {
        public const string TEST_CLIENT_ID = "test-client-id";
        public const string TEST_CLIENT_SECRET = "test-client-secret";
        public const string TEST_REDIRECT_URL = "test-redirect-url";
        public const string TEST_CONFIG_JSON = "/test-nest-config.json";

        [TestMethod]
        public void testNestConfigToParcel()
        {
            String testClientId = "test-id";
            String testClientSecret = "test-secret";
            String testRedirectURL = "test-redirect-url";

            NestConfig config = new NestConfig.Builder()
                    .clientID(testClientId)
                    .clientSecret(testClientSecret)
                    .redirectURL(testRedirectURL)
                    .build();

            //Parcel parcel = Parcel.obtain();
            //config.writeToParcel(parcel, 0);

            //parcel.setDataPosition(0);

            //NestConfig configFromParcel = NestConfig.CREATOR.createFromParcel(parcel);
            //Assert.AreEqual(config, configFromParcel);
        }

        [TestMethod]
        public void testToString_shouldReturnNicelyFormattedString()
        {
            NestConfig config = new NestConfig.Builder()
                    .clientID(TEST_CLIENT_ID)
                    .clientSecret(TEST_CLIENT_SECRET)
                    .redirectURL(TEST_REDIRECT_URL)
                    .build();

            try
            {
                var jsonString = LoadString(TEST_CONFIG_JSON);
                var obj = JObject.Parse(jsonString);
                obj[NestConfig.KEY_STATE_VALUE] = config.StateValue; // Set ephemeral state.
                Assert.AreEqual(obj.ToString(), config.ToString());
            }
            catch (IOException ex)
            {
                Debug.WriteLine(ex.StackTrace);
                Assert.Fail();
            }
        }
    }
}