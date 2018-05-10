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

            //var nestConfig = new NestConfig(
            //    clientId: "<client_id>",
            //    clientSecret: "<client_secret>",
            //    redirectUrl: "<redirect_url>");
            var nestConfig = NestConfig.FromJson(settings.ReadJson("nest"));

            using (var nest = new NestClient(nestConfig))
            {
                //load or create token
                var tokenConfig = settings.Read<NestToken>("nest.token");
                if (string.IsNullOrEmpty(tokenConfig?.Token))
                {//
                    tokenConfig = nest.CreateToken(nestConfig);
                    settings.Write("nest.token", tokenConfig);
                }

                nest.StartWithToken(tokenConfig.Token);

                ExecuteExample(nest, new Example1());

                ExecuteAllExamples(nest);
            }
        }

        static void ExecuteExample(NestClient client, IExample example)
        {
            var exampleName = example.GetType().GetCustomAttribute<ExampleAttribute>()?.Name ?? example.GetType().Name;

            log.Info($"--------------------------------------------------------------------------------");
            log.Info($"---------------- EXAMPLE \"{exampleName}\" START");

            try
            {
                example.Execute(client);
            }
            catch(Exception ex)
            {
                log.Error("Failed to execute \"{exampleName}\".", ex);
            }
            finally
            {
                log.Info($"---------------- EXAMPLE \"{exampleName}\" END");
                log.Info($"--------------------------------------------------------------------------------");
                log.Info($"Presss ENTER to continue.");

                Console.ReadLine();
            }
        }

        static void ExecuteAllExamples(NestClient nest)
        {
            foreach (var type in Assembly.GetExecutingAssembly().GetExportedTypes())
            {
                if (type.GetCustomAttribute<ExampleAttribute>() == null)
                    continue;
                var instance = Activator.CreateInstance(type) as IExample;
                if (instance == null)
                    continue;

                ExecuteExample(nest, instance);
            }
        }
    }
}