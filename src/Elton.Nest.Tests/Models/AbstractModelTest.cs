using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elton.Nest.Tests.Models
{
    public class AbstractModelTest
    {
        static readonly string basePath = Path.Combine(
            Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
            @"data\resources\");

        public static string LoadString(string fileName)
        {
            var fullName = Path.Combine(basePath, fileName?.TrimStart('/'));
            if (!File.Exists(fullName))
                return null;

            return File.ReadAllText(fullName, Encoding.UTF8);
        }

        public static T Parse<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}
