using Elton.Nest;
using Elton.Nest.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NestMonitoringConsole
{
    [Example("Re-using an existing access token")]
    public class ReusingAccessToken : IExample
    {
        public void Execute(NestClient nest)
        {
            // Get existing instance.
            //nest = new NestClient();

            // Get the token string from your safe place.
            var token = "abc123";

            // Authenticate with an existing token.
            nest.StartWithToken(token);
            nest.WhenAuthFailure()
                .Subscribe(args =>
                {
                    // Handle exceptions here.
                });
            nest.WhenAuthRevoked()
                .Subscribe(args =>
                {
                    // Your previously authenticated connection has become unauthenticated.
                    // Recommendation: Relaunch an auth flow with nest.launchAuthFlow().
                });

            //onAuthSuccess
            // Handle success here. Start pulling from Nest API.

            //
        }
    }

    [Example("Listen for changes to everything")]
    public class ListenForAllChanges : IExample
    {
        public void Execute(NestClient nest)
        {
            //Subscribe to event
            var subscription = nest.WhenAnyUpdated().Subscribe(update =>
            {
                Metadata metadata = update.Metadata;
                var cameras = update.Cameras;
                var smokeCOAlarms = update.SmokeCOAlarms;
                var thermostats = update.Thermostats;
                var structures = update.Structures;

                // Handle updates here.
            });

            //Unsubscribe from event
            subscription.Dispose();
        }
    }


    [Example("Listen for changes to all devices")]
    public class ListenForDeviceChanges : IExample
    {
        public void Execute(NestClient nest)
        {
            //Subscribe to event
            var subscription = nest.WhenDeviceUpdated().Subscribe(update =>
            {
                var cameras = update.Cameras;
                var smokeCOAlarms = update.SmokeCOAlarms;
                var thermostats = update.Thermostats;

                // Handle updates here.
            });

            //Unsubscribe from event
            subscription.Dispose();
        }
    }

    [Example("Listen for changes to specific device types")]
    public class ListenForThermostatChanges : IExample
    {
        public void Execute(NestClient nest)
        {
            var listSubscriptions = new List<IDisposable>();
            IDisposable subscription;

            //All thermostats:
            subscription = nest.WhenThermostatUpdated().Subscribe(thermostats =>
            {
                // Handle thermostat update...
            });

            //All smoke alarms:
            subscription = nest.WhenSmokeCOAlarmUpdated().Subscribe(thermostats =>
            {
                // Handle smoke+co alarm update...
            });

            //All cameras:
            subscription = nest.WhenCameraUpdated().Subscribe(thermostats =>
            {
                // Handle camera update...
            });

            //Unsubscribe from events
            foreach (var item in listSubscriptions)
                subscription.Dispose();
        }
    }

    [Example("Listen to changes to all structures")]
    public class ListenForStructureChanges : IExample
    {
        public void Execute(NestClient nest)
        {
            //Subscribe to event
            var subscription = nest.WhenStructureUpdated().Subscribe(structures =>
            {
                // Handle structure update...
            });

            //Unsubscribe from event
            subscription.Dispose();
        }
    }


    [Example("Listen to metadata changes")]
    public class ListenForMetadataChanges : IExample
    {
        public void Execute(NestClient nest)
        {
            //Subscribe to event
            var subscription = nest.WhenMetadataUpdated().Subscribe(data =>
            {
                // Handle metadata update... do action.
            });

            //Unsubscribe from event
            subscription.Dispose();
        }
    }


    [Example("Thermostat example")]
    public class UpdateThermostat : IExample
    {
        public void Execute(NestClient nest)
        {
            var thermostat = new Thermostat();

            // Get id from Thermostat#getDeviceId
            string thermostatId = thermostat.DeviceId;

            // The temperature in Farhenheit to set. (Note: type long)
            long newTempF = 75;

            // Set thermostat target temp (in degrees F).
            nest.Thermostats.SetTargetTemperatureF(thermostatId, newTempF);

            float newTempC = 23.5F;

            //Set thermostat target temp, in half degrees Celsius
            nest.Thermostats.SetTargetTemperatureC(thermostatId, newTempC);
        }
    }

    [Example("Thermostat example with callback")]
    public class UpdateThermostatWithCallback : IExample
    {
        public void Execute(NestClient nest)
        {
            var myThermostat = new Thermostat();

            // Get id from Thermostat#getDeviceId
            string thermostatId = myThermostat.DeviceId;

            // The temperature in Celsius to set. (Note: type double)
            double newTempC = 22.5;

            // Set thermostat target temp (in degrees C) with an optional success callback.
            nest.Thermostats.SetTargetTemperatureC(thermostatId, newTempC, new Callback()
            {
                OnSuccess = () =>
                {
                    // The update to the thermostat succeeded.
                },
                OnFailure = (ex) =>
                {
                    // The update to the thermostat failed.
                },
            });
        }
    }

    [Example("Camera example")]
    public class UpdateCamera : IExample
    {
        public void Execute(NestClient nest)
        {
            var myCamera = new Camera();

            // Get id from Camera#getDeviceId.
            string camId = myCamera.DeviceId;

            // Set camera to start streaming.
            nest.Cameras.SetIsStreaming(camId, true);

        }
    }


    [Example("Camera example with callback")]
    public class UpdateCameraWithCallback : IExample
    {
        public void Execute(NestClient nest)
        {
            var myCamera = new Camera();

            // Get id from Camera#getDeviceId.
            string camId = myCamera.DeviceId;

            // Set camera to start streaming with an optional success callback.
            nest.Cameras.SetIsStreaming(camId, true, new Callback()
            {
                OnSuccess = () =>
                {
                    // The update to the camera succeeded.
                },
                OnFailure = (ex) =>
                {
                    // The update to the camera failed.
                },
            });
        }
    }
}