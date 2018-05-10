using Elton.Nest;
using Elton.Nest.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NestMonitoringConsole
{
    [Example("Test 2")]
    public class Example2 : IExample
    {
        static readonly Common.Logging.ILog log = Common.Logging.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void Execute(NestClient nest)
        {
            nest.StreamingError += (args) =>
            {
                log.Error("NEST ERROR.", args);
            };
            nest.Notifier.Error += (sender, args) =>
            {
                log.Warn($"NEST ERROR: {args?.Error}");
            };
            nest.Notifier.GlobalUpdated += (sender, args) =>
            {
                log.Info($"NEST UPDATE: {args?.Data}");
            };

            nest.WhenValueAdded()
                .Subscribe(args =>
                {
                    log.Info($"VALUE ADDED: {args?.Path} = {args?.Data}");
                });

            nest.WhenValueChanged()
                .Subscribe(args =>
                {
                    log.Info($"VALUE CHANGED: {args?.Path} = {args?.OldData} -> {args?.Data}");
                });

            nest.WhenValueRemoved()
                .Subscribe(args =>
                {
                    log.Info($"VALUE REMOVED: {args?.Path}");
                });



            string structureId = "<your structure id>";
            nest.Structures.SetAway(structureId, AwayState.Away);
            //nest.Thermostats.setHVACMode(thermostatId_LivingRoom, "cool");
            //nest.Thermostats.setTargetTemperatureC(thermostatId_LivingRoom, 23.5);//in half degrees Celsius (0.5℃).

            log.Info("Monitor running. Presss ENTER to exit.");
            Console.ReadLine();
            log.Info("Monitor exit.");
        }
    }
}
