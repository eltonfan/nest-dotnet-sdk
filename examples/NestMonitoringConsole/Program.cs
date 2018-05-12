using Common.Logging;
using Elton;
using Elton.Nest;
using Elton.Nest.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NestMonitoringConsole
{
    partial class Program
    {
        static readonly ILog log;
        static Program()
        {
            //Setup logging. 
            //Must be done (just once!) before any loggers are instantiated
            var props = new Common.Logging.Configuration.NameValueCollection
            {
                { "configType", "FILE" },
                { "configFile", "./nlog.config" }
            };
            LogManager.Adapter = new Common.Logging.NLog.NLogLoggerFactoryAdapter(props);

            //Create logger instance
            log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }

        static void Main(string[] args)
        {
            log.Info($"Nest DotNet SDK v{NestClient.SdkVersion}");

            var configPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"data\");
            var settings = new Settings(configPath);

            var tokenConfig = settings.Read<NestToken>("nest.token");
            if (string.IsNullOrEmpty(tokenConfig?.Token))
            {//
                var nestConfig = NestConfig.FromJson(settings.ReadJson("nest"));

                var getter = new TokenGetter(nestConfig);
                tokenConfig = getter.GetToken();
                settings.Write("nest.token", tokenConfig);
            }

            using (var nest = new NestClient())
            {
                nest.StartWithToken(tokenConfig.Token);

                //string structureId = "<your structure id>";
                //nest.Structures.SetAway(structureId, AwayState.Away);
                //nest.Thermostats.setHVACMode(thermostatId_LivingRoom, "cool");
                //nest.Thermostats.setTargetTemperatureC(thermostatId_LivingRoom, 23.5);//in half degrees Celsius (0.5℃).

                //ExampleExecutor.Execute(nest, new Example1());
                //ExecuteAllExamples(nest);

                nest.StreamingError += (ex) => { Console.WriteLine("NEST ERROR.", ex); };
                nest.WhenError().Subscribe(error =>
                {
                    log.Warn($"NEST ERROR: {error.Type} {error.Error}:{error.Message}");
                });
                nest.WhenAnyUpdated().Subscribe(data =>
                {
                    log.Info($"NEST UPDATE: {data}");
                });

                nest.WhenValueAdded()
                    .Subscribe(e =>
                    {
                        log.Info($"VALUE ADDED: {e?.Path} = {e?.Data}");
                    });

                nest.WhenValueChanged()
                    .Subscribe(e =>
                    {
                        log.Info($"VALUE CHANGED: {e?.Path} = {e?.OldData} -> {e?.Data}");
                    });

                nest.WhenValueRemoved()
                    .Subscribe(e =>
                    {
                        log.Info($"VALUE REMOVED: {e?.Path}");
                    });


                log.Info("Monitor running. Presss ENTER to exit.");
                Console.ReadLine();
                log.Info("Monitor exit.");
            }
        }
    }
}