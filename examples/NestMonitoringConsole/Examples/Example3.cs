using Elton.Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace NestMonitoringConsole
{
    [Example("Monitor device value")]
    public class Example3 : IExample
    {
        static readonly Common.Logging.ILog log = Common.Logging.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void Execute(NestClient nest)
        {
            nest.StreamingError += (args) => { Console.WriteLine("NEST ERROR.", args); };
            nest.WhenError().Subscribe(args =>
            {
                Console.WriteLine($"NEST ERROR: {args?.Error}");
            });

            nest.WhenValueChanged().Subscribe(args =>
            {
                Console.WriteLine($"VALUE CHANGED: {args?.Path} = {args?.OldData} -> {args?.Data}");
            });

            Console.ReadLine();
        }
    }
}
