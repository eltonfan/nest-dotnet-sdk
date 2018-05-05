using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Elton.Nest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elton.Nest.Tests.Models
{
    [TestClass]
    public class CameraAndroidTest : AbstractModelTest
    {
        private const string TEST_CAMERA_JSON = "/test-camera.json";

        [TestMethod]
        public void testCameraToParcel()
        {
            try
            {
                string json = LoadString(TEST_CAMERA_JSON);
                Camera camera = Parse<Camera>(json);

                //Parcel parcel = Parcel.obtain();
                //camera.writeToParcel(parcel, 0);

                //parcel.setDataPosition(0);

                //Camera cameraFromParcel = Camera.CREATOR.createFromParcel(parcel);
                //Assert.AreEqual(camera, cameraFromParcel);
            }
            catch (IOException e)
            {
                Trace.WriteLine(e.StackTrace);
                Assert.Fail();
            }
        }

        [TestClass]
        public class CameraTest : AbstractModelTest
        {

            public const string TEST_CAMERA_JSON = "/test-camera.json";
            public const string TEST_EMPTY_CAMERA = "/test-empty-camera.json";
            public const string TEST_CAMERA_UNKNOWN_JSON = "/test-camera-unknown.json";

            [TestMethod]
            public void testCameraConstructor_shouldCreateCameraFromJsonUsingJacksonBindings()
            {
                try
                {
                    Camera camera = Parse<Camera>("{}");

                    Assert.IsFalse(camera.IsStreaming);
                    Assert.IsFalse(camera.IsAudioInputEnabled);
                    Assert.IsNull(camera.LastIsOnlineChange);
                    Assert.IsFalse(camera.IsVideoHistoryEnabled);
                    Assert.IsNull(camera.WebUrl);
                    Assert.IsNull(camera.AppUrl);
                    Assert.IsFalse(camera.IsPublicShareEnabled);
                    Assert.IsTrue(camera.ActivityZones.Count < 1);
                    Assert.IsNull(camera.PublicShareUrl);
                    Assert.IsNull(camera.SnapshotUrl);
                    Assert.IsNull(camera.LastEventValue);
                }
                catch (IOException e)
                {
                    Trace.WriteLine(e.StackTrace);
                    Assert.Fail();
                }
            }

            [TestMethod]
            public void testCreateCameraWithJacksonMapper_shouldSetAllValuesCorrectly()
            {
                try
                {
                    var json = LoadString(TEST_CAMERA_JSON);
                    Camera camera = Parse<Camera>(json);

                    Assert.AreEqual(camera.DeviceId, "awJo6rH");
                    Assert.AreEqual(camera.SoftwareVersion, "4.0");
                    Assert.AreEqual(camera.StructureId, "VqFabWH21nwV...");
                    Assert.AreEqual(camera.WhereId, "d6reb_OZTM...");
                    Assert.AreEqual(camera.Name, "Hallway (upstairs)");
                    Assert.AreEqual(camera.NameLong, "Hallway Camera (upstairs)");
                    Assert.AreEqual(camera.IsOnline, true);

                    Assert.AreEqual(camera.LastIsOnlineChange, "2015-12-29T18:42:00.000Z");
                    Assert.AreEqual(camera.IsVideoHistoryEnabled, true);
                    Assert.AreEqual(camera.IsStreaming, true);
                    Assert.AreEqual(camera.IsAudioInputEnabled, true);
                    Assert.AreEqual(camera.WebUrl,
                            "https://home.nest.com/cameras/device_id?auth=access_token");
                    Assert.AreEqual(camera.AppUrl, "nestmobile://cameras/device_id?auth=access_token");
                    Assert.AreEqual(camera.IsPublicShareEnabled, true);

                    Assert.AreEqual(camera.ActivityZones[0].Name, "Walkway");
                    Assert.AreEqual(camera.ActivityZones[0].Id, "244083");

                    Assert.AreEqual(camera.ActivityZones[1].Name, "Porch");
                    Assert.AreEqual(camera.ActivityZones[1].Id, "237084");

                    Assert.AreEqual(camera.PublicShareUrl, "https://video.nest.com/live/STRING1?STRING2");
                    Assert.AreEqual(camera.SnapshotUrl, "STRING1/device_id/STRING2?auth=access_token");

                    Camera.LastEvent lastEvent = camera.LastEventValue;

                    string expectedWebUrl =
                            "https://home.nest.com/cameras/device_id/cuepoints/STRING?auth=access_token";
                    string expectedAppUrl =
                            "nestmobile://cameras/device_id/cuepoints/STRING?auth=access_token";
                    string expectedImageUrl = "STRING1/device_id/STRING2?auth=access_token";
                    string expectedAnimatedImageUrl = "STRING1/device_id/STRING2?auth=access_token";

                    var expectedActivityZoneIds = new List<string>();
                    expectedActivityZoneIds.Add("244083");
                    expectedActivityZoneIds.Add("237084");

                    Assert.AreEqual(lastEvent.HasSound, true);
                    Assert.AreEqual(lastEvent.HasMotion, true);
                    Assert.AreEqual(lastEvent.StartTime, "2015-12-29T00:00:00.000Z");
                    Assert.AreEqual(lastEvent.EndTime, "2015-12-29T18:42:00.000Z");
                    Assert.AreEqual(lastEvent.UrlsExpireTime, "2015-10-31T23:59:59.000Z");
                    Assert.AreEqual(lastEvent.WebUrl, expectedWebUrl);
                    Assert.AreEqual(lastEvent.AppUrl, expectedAppUrl);
                    Assert.AreEqual(lastEvent.ImageUrl, expectedImageUrl);
                    Assert.AreEqual(lastEvent.AnimatedImageUrl, expectedAnimatedImageUrl);
                    CollectionAssert.AreEqual(expectedActivityZoneIds, lastEvent.ActivityZoneIds);
                }
                catch (IOException e)
                {
                    Trace.WriteLine(e.StackTrace);
                    Assert.Fail();
                }
            }

            [TestMethod]
            public void testCreateCameraWithJacksonMapperUnknownProperties_shouldSetAllValuesCorrectly()
            {
                try
                {
                    var json = LoadString(TEST_CAMERA_UNKNOWN_JSON);
                    Camera camera = Parse<Camera>(json);

                    Assert.AreEqual(camera.DeviceId, "awJo6rH");
                    Assert.AreEqual(camera.SoftwareVersion, "4.0");
                    Assert.AreEqual(camera.StructureId, "VqFabWH21nwV...");
                    Assert.AreEqual(camera.WhereId, "d6reb_OZTM...");
                    Assert.AreEqual(camera.Name, "Hallway (upstairs)");
                    Assert.AreEqual(camera.NameLong, "Hallway Camera (upstairs)");
                    Assert.AreEqual(camera.IsOnline, true);

                    Assert.AreEqual(camera.LastIsOnlineChange, "2015-12-29T18:42:00.000Z");
                    Assert.AreEqual(camera.IsVideoHistoryEnabled, true);
                    Assert.AreEqual(camera.IsStreaming, true);
                    Assert.AreEqual(camera.IsAudioInputEnabled, true);
                    Assert.AreEqual(camera.WebUrl,
                            "https://home.nest.com/cameras/device_id?auth=access_token");
                    Assert.AreEqual(camera.AppUrl, "nestmobile://cameras/device_id?auth=access_token");

                    Assert.AreEqual(camera.IsPublicShareEnabled, true);

                    Assert.AreEqual(camera.ActivityZones[0].Name, "Walkway");
                    Assert.AreEqual(camera.ActivityZones[0].Id, "244083");

                    Assert.AreEqual(camera.ActivityZones[1].Name, "Porch");
                    Assert.AreEqual(camera.ActivityZones[1].Id, "237084");

                    Assert.AreEqual(camera.PublicShareUrl, "https://video.nest.com/live/STRING1?STRING2");
                    Assert.AreEqual(camera.SnapshotUrl, "STRING1/device_id/STRING2?auth=access_token");

                    Camera.LastEvent lastEvent = camera.LastEventValue;

                    string expectedWebUrl =
                            "https://home.nest.com/cameras/device_id/cuepoints/STRING?auth=access_token";
                    string expectedAppUrl =
                            "nestmobile://cameras/device_id/cuepoints/STRING?auth=access_token";
                    string expectedImageUrl = "STRING1/device_id/STRING2?auth=access_token";
                    string expectedAnimatedImageUrl = "STRING1/device_id/STRING2?auth=access_token";

                    var expectedActivityZoneIds = new List<string>();
                    expectedActivityZoneIds.Add("244083");
                    expectedActivityZoneIds.Add("237084");

                    Assert.AreEqual(lastEvent.HasSound, true);
                    Assert.AreEqual(lastEvent.HasMotion, true);
                    Assert.AreEqual(lastEvent.StartTime, "2015-12-29T00:00:00.000Z");
                    Assert.AreEqual(lastEvent.EndTime, "2015-12-29T18:42:00.000Z");
                    Assert.AreEqual(lastEvent.UrlsExpireTime, "2015-10-31T23:59:59.000Z");
                    Assert.AreEqual(lastEvent.WebUrl, expectedWebUrl);
                    Assert.AreEqual(lastEvent.AppUrl, expectedAppUrl);
                    Assert.AreEqual(lastEvent.ImageUrl, expectedImageUrl);
                    Assert.AreEqual(lastEvent.AnimatedImageUrl, expectedAnimatedImageUrl);

                    CollectionAssert.AreEqual(lastEvent.ActivityZoneIds, expectedActivityZoneIds);
                }
                catch (IOException e)
                {
                    Trace.WriteLine(e.StackTrace);
                    Assert.Fail();
                }
            }

            [TestMethod]
            public void testDescribeContents_shouldReturnZero()
            {
                Camera c = new Camera();
                //Assert.AreEqual(c.describeContents(), 0);
            }

            [TestMethod]
            public void testLastEventDescribeContents_shouldReturnZero()
            {
                try
                {
                    var json = LoadString(TEST_CAMERA_JSON);
                    Camera camera = Parse<Camera>(json);
                    //Assert.AreEqual(camera.LastEventValue.describeContents(), 0);
                }
                catch (IOException e)
                {
                    Trace.WriteLine(e.StackTrace);
                    Assert.Fail();
                }
            }

            [TestMethod]
            public void testNewArray_shouldReturnArrayOfCorrectSize()
            {
                int cameraSize = new Random().Next(9) + 1;

                //Camera[] cameras = Camera.CREATOR.newArray(cameraSize);
                //Assert.AreEqual(cameras.Length, cameraSize);
            }

            [TestMethod]
            public void testLastEventNewArray_shouldReturnArrayOfCorrectSize()
            {
                int cameraLastEventSize = new Random().Next(9) + 1;

                //Camera.LastEvent[] lastEvents = Camera.LastEvent.CREATOR.newArray(cameraLastEventSize);
                //Assert.AreEqual(lastEvents.Length, cameraLastEventSize);
            }

            [TestMethod]
            public void testEquals_shouldReturnFalseWithNonCamera()
            {
                Object o = new Object();
                Camera c = new Camera();
                Assert.IsFalse(c.Equals(o));
            }

            [TestMethod]
            public void testToString_shouldReturnNicelyFormattedString()
            {
                Camera c = new Camera();
                try
                {
                    var json = LoadString(TEST_EMPTY_CAMERA);
                    Assert.AreEqual(json, c.ToString());
                }
                catch (IOException e)
                {
                    Trace.WriteLine(e.StackTrace);
                    Assert.Fail();
                }
            }
        }
    }
}