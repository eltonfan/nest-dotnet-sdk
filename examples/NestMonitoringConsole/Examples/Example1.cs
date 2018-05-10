using Elton.Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace NestMonitoringConsole
{
    [Example("test 1")]
    public class Example1 : IExample
    {
        static readonly Common.Logging.ILog log = Common.Logging.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void Execute(NestClient nest)
        {
            nest.StreamingError += (args) =>
            {
                Console.WriteLine("NEST ERROR.", args);
            };
            nest.Notifier.Error += (sender, args) =>
            {
                Console.WriteLine($"NEST ERROR: {args?.Error}");
            };
            nest.Notifier.GlobalUpdated += (sender, args) =>
            {
                Console.WriteLine($"NEST UPDATE: {args?.Data}");
            };

            Console.ReadLine();
        }
    }
}
