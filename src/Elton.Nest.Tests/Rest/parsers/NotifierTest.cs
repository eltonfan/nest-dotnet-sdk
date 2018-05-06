using System;
using System.Collections.Generic;
using System.IO;
using Elton.Nest.Models;
using Elton.Nest.Rest.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elton.Nest.Tests.Models
{
    [TestClass]
    public class NotifierTest : AbstractModelTest
    {
        [TestMethod]
        public void testHandleError_shouldReceiveErrorNotification()
        {
            var error = "genericError";

            ErrorMessage errorMessage = null;
            bool authFailure = false;
            bool authRevoked = false;

            EventHandler<ErrorEventArgs> dummyErrorHandler = (sender, args) => { errorMessage = args.Data; };
            EventHandler<AuthFailureEventArgs> dummyAuthFailureHandler = (sender, args) => { authFailure = true; };
            EventHandler<AuthRevokedEventArgs> dummyAuthRevokedHandler = (sender, args) => { authRevoked = true; };

            var notifier = new Notifier();
            notifier.AuthFailure += dummyAuthFailureHandler;
            notifier.AuthRevoked += dummyAuthRevokedHandler;
            notifier.Error += dummyErrorHandler;

            notifier.handleError(new ErrorMessage() { Error = error });

            Assert.IsFalse(authFailure);
            Assert.IsFalse(authRevoked);
            Assert.AreEqual(errorMessage.Error, error);
        }

        [TestMethod]
        public void testHandleError_shouldReceiveAuthErrorNotification()
        {
            var error = "unauthorized";

            ErrorMessage errorMessage = null;
            bool authFailure = false;
            bool authRevoked = false;

            EventHandler<ErrorEventArgs> dummyErrorHandler = (sender, args) => { errorMessage = args.Data; };
            EventHandler<AuthFailureEventArgs> dummyAuthFailureHandler = (sender, args) => { authFailure = true; };
            EventHandler<AuthRevokedEventArgs> dummyAuthRevokedHandler = (sender, args) => { authRevoked = true; };

            var notifier = new Notifier();
            notifier.AuthFailure += dummyAuthFailureHandler;
            notifier.AuthRevoked += dummyAuthRevokedHandler;
            notifier.Error += dummyErrorHandler;

            notifier.handleError(new ErrorMessage() { Error = error });

            Assert.IsTrue(authFailure);
            Assert.IsFalse(authRevoked);
            Assert.IsNull(errorMessage);

            notifier.handleAuthRevoked();
            Assert.IsTrue(authRevoked);
        }


        [TestMethod]
        public void testHandleData_shouldReceiveGlobalNotification()
        {
            GlobalUpdate globalUpdate = null;
            DeviceUpdate deviceUpdate = null;
            List<Camera> cameras  = null;
            List<Thermostat> thermostats = null;
            List<SmokeCOAlarm> smokeCOAlarms = null;
            List<Structure> structures = null;
            Metadata metadata = null;

            var notifier = new Notifier();
            notifier.GlobalUpdated += (sender, args) => { globalUpdate = args.Data; };
            notifier.DeviceUpdated += (sender, args) => { deviceUpdate = args.Data; };
            notifier.CameraUpdated += (sender, args) => { cameras = args.Data; };
            notifier.ThermostatUpdated += (sender, args) => { thermostats = args.Data; };
            notifier.SmokeCOAlarmUpdated += (sender, args) => { smokeCOAlarms = args.Data; };
            notifier.StructureUpdated += (sender, args) => { structures = args.Data; };
            notifier.MetadataUpdated += (sender, args) => { metadata = args.Data; };

            notifier.handleData(new GlobalUpdate(
                    thermostats: new List<Thermostat>(),
                    smokeCOAlarms: new List<SmokeCOAlarm>(),
                    cameras: new List<Camera>(),
                    structures: new List<Structure>(),
                    metadata: new Metadata()));

            Assert.IsNotNull(globalUpdate);
            Assert.IsNotNull(deviceUpdate);
            Assert.IsNotNull(cameras);
            Assert.IsNotNull(thermostats);
            Assert.IsNotNull(smokeCOAlarms);
            Assert.IsNotNull(structures);
            Assert.IsNotNull(metadata);
        }

        [TestMethod]
        public void testRemoveListener_shouldNotReceiveNotification()
        {
            String error = "unauthorized";
            bool authFailure = false;
            bool authRevoked = false;

            EventHandler<AuthFailureEventArgs> dummyAuthFailureHandler = (sender, args) => { authFailure = true; };
            EventHandler<AuthRevokedEventArgs> dummyAuthRevokedHandler = (sender, args) => { authRevoked = true; };

            var notifier = new Notifier();
            notifier.AuthFailure += dummyAuthFailureHandler;
            notifier.AuthRevoked += dummyAuthRevokedHandler;

            notifier.AuthFailure -= dummyAuthFailureHandler;
            notifier.AuthRevoked -= dummyAuthRevokedHandler;

            notifier.handleError(new ErrorMessage() { Error = error });

            Assert.IsFalse(authFailure);
            Assert.IsFalse(authRevoked);
        }
    }
}