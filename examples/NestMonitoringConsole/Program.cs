using Common.Logging;
using Elton;
using Elton.Nest;
using Elton.Nest.Models;
using System;
using System.IO;
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
            var instance = new Program();

            var tokenConfig = instance.settings.Read<NestToken>("nest.token");
            if (tokenConfig == null)
            {
                instance.Login();
                tokenConfig = instance.settings.Read<NestToken>("nest.token");
            }

            instance.StartMonitoring(tokenConfig);
        }

        readonly Settings settings;
        readonly NestConfig nestConfig;
        readonly NestClient nest;
        public Program()
        {
            log.Info($"Nest DotNet SDK v{NestClient.SdkVersion}");

            var configPath = Path.Combine(
                Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"data\");
            settings = new Settings(configPath);

            //var nestConfig = new NestConfig.Builder()
            //    .clientID("** client_id **")
            //    .clientSecret("** client_secret **")
            //    .redirectURL("** redirect_url **")
            //    .build();
            nestConfig = new NestConfig.Builder()
                 .FromJsonString(settings.ReadJson("nest"))
                 .Build();


            nest = new NestClient();
            nest.oauth2.setConfig(
               nestConfig.ClientId, nestConfig.ClientSecret, nestConfig.RedirectUrl);
            nest.Error += (args) =>
            {
                log.Info($"NEST ERROR: {args?.StackTrace}");
            };
            nest.Notifier.Error += (sender, args) =>
            {
                log.Info($"NEST ERROR: {args?.Error}");
            };
            nest.Notifier.GlobalUpdated += (sender, args) =>
            {
                log.Info($"NEST UPDATE: {args?.Data}");
            };

            nest.Notifier.ValueAdded += (sender, args) =>
            {
                log.Info($"VALUE ADDED: {args?.Path} = {args?.Data}");
            };
            nest.Notifier.ValueChanged += (sender, args) =>
            {
                log.Info($"VALUE CHANGED: {args?.Path} = {args?.OldData} -> {args?.Data}");
            };
            nest.Notifier.ValueRemoved += (sender, args) =>
            {
                log.Info($"VALUE REMOVED: {args?.Path}");
            };
        }

        public void StartMonitoring(NestToken tokenConfig)
        {
            nest.startWithToken(tokenConfig.Token);
            log.Info("Monitor running. Presss ENTER to exit.");

            //nest.Thermostats.setHVACMode(thermostatId_LivingRoom, "cool");
            //nest.Thermostats.setTargetTemperatureC(thermostatId_LivingRoom, 23.5);//in half degrees Celsius (0.5℃).

            Console.ReadLine();
            log.Info("Monitor exit.");
        }
    }
}
