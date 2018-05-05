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
        private class ErrorListener : Elton.Nest.Listeners.ErrorListener
        {
            internal ErrorMessage errorMessage = null;


            public void onError(ErrorMessage errorMessage)
            {
                this.errorMessage = errorMessage;
            }
        }

        private class AuthListener : Elton.Nest.Listeners.AuthListener
        {
            internal Boolean authFailure = false;
            internal Boolean authRevoked = false;


            public void onAuthFailure(NestException exception)
            {
                authFailure = true;
            }


            public void onAuthRevoked()
            {
                authRevoked = true;
            }
        }

        [TestMethod]
        public void testHandleError_shouldReceiveErrorNotification()
        {

            String error = "genericError";
            var dummyErrorListener = new ErrorListener();
            var dummyAuthListener = new AuthListener();

            var notifier = new Notifier();
            notifier.addListener(dummyAuthListener);
            notifier.addListener(dummyErrorListener);
            notifier.handleError(new ErrorMessage() { Error = error });

            Assert.IsFalse(dummyAuthListener.authFailure);
            Assert.IsFalse(dummyAuthListener.authRevoked);
            Assert.AreEqual(dummyErrorListener.errorMessage.Error, error);
        }

        [TestMethod]
        public void testHandleError_shouldReceiveAuthErrorNotification()
        {

            String error = "unauthorized";
            var dummyErrorListener = new ErrorListener();
            var dummyAuthListener = new AuthListener();

            var notifier = new Notifier();
            notifier.addListener(dummyAuthListener);
            notifier.addListener(dummyErrorListener);
            notifier.handleError(new ErrorMessage() { Error = error });

            Assert.IsTrue(dummyAuthListener.authFailure);
            Assert.IsFalse(dummyAuthListener.authRevoked);
            Assert.IsNull(dummyErrorListener.errorMessage);

            notifier.handleAuthRevoked();
            Assert.IsTrue(dummyAuthListener.authRevoked);
        }

        class GlobalListener : Elton.Nest.Listeners.GlobalListener
        {
            internal GlobalUpdate update;


            public void onUpdate(GlobalUpdate update)
            {
                this.update = update;
            }
        }

        class DeviceListener : Elton.Nest.Listeners.DeviceListener
        {
            internal DeviceUpdate update;


            public void onUpdate(DeviceUpdate update)
            {
                this.update = update;
            }
        }

        class CameraListener : Elton.Nest.Listeners.CameraListener
        {
            internal List<Camera> cameras;


            public void onUpdate(List<Camera> cameras)
            {
                this.cameras = cameras;
            }
        }

        class ThermostatListener : Elton.Nest.Listeners.ThermostatListener
        {
            internal List<Thermostat> thermostats;


            public void onUpdate(List<Thermostat> thermostats)
            {
                this.thermostats = thermostats;
            }
        }

        class SmokeCOAlarmListener : Elton.Nest.Listeners.SmokeCOAlarmListener
        {
            internal List<SmokeCOAlarm> smokeCOAlarms;


            public void onUpdate(List<SmokeCOAlarm> smokeCOAlarms)
            {
                this.smokeCOAlarms = smokeCOAlarms;
            }
        }

        class StructureListener : Elton.Nest.Listeners.StructureListener
        {
            internal List<Structure> structures;


            public void onUpdate(List<Structure> structures)
            {
                this.structures = structures;
            }
        }

        class MetadataListener : Elton.Nest.Listeners.MetadataListener
        {
            internal Metadata metadata;


            public void onUpdate(Metadata metadata)
            {
                this.metadata = metadata;
            }
        }

        [TestMethod]
        public void testHandleData_shouldReceiveGlobalNotification()
        {

            var dummyGlobalListener = new GlobalListener();
            var dummyDeviceListener = new DeviceListener();
            var dummyCameraListener = new CameraListener();
            var dummyThermostatListener = new ThermostatListener();
            var dummySmokeCOAlarmListener = new SmokeCOAlarmListener();
            var dummyStructureListener = new StructureListener();
            var dummyMetadataListener = new MetadataListener();

            var notifier = new Notifier();
            notifier.addListener(dummyGlobalListener);
            notifier.addListener(dummyDeviceListener);
            notifier.addListener(dummyCameraListener);
            notifier.addListener(dummyThermostatListener);
            notifier.addListener(dummySmokeCOAlarmListener);
            notifier.addListener(dummyStructureListener);
            notifier.addListener(dummyMetadataListener);

            List<Thermostat> thermostats = new List<Thermostat>();
            List<Camera> cameras = new List<Camera>();
            List<SmokeCOAlarm> smokeAlarms = new List<SmokeCOAlarm>();
            List<Structure> structures = new List<Structure>();

            notifier.handleData(new GlobalUpdate(
                    thermostats, smokeAlarms, cameras, structures, new Metadata()));

            Assert.IsNotNull(dummyGlobalListener.update);
            Assert.IsNotNull(dummyDeviceListener.update);
            Assert.IsNotNull(dummyCameraListener.cameras);
            Assert.IsNotNull(dummyThermostatListener.thermostats);
            Assert.IsNotNull(dummySmokeCOAlarmListener.smokeCOAlarms);
            Assert.IsNotNull(dummyStructureListener.structures);
            Assert.IsNotNull(dummyMetadataListener.metadata);
        }

        [TestMethod]
        public void testRemoveListener_shouldNotReceiveNotification()
        {
            String error = "unauthorized";
            var dummyAuthListener = new AuthListener();

            var notifier = new Notifier();
            notifier.addListener(dummyAuthListener);
            notifier.removeListener(dummyAuthListener);
            notifier.handleError(new ErrorMessage() { Error = error });

            Assert.IsFalse(dummyAuthListener.authFailure);
            Assert.IsFalse(dummyAuthListener.authRevoked);
        }
    }
}