using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace OSt
{
    static class Configuration
    {
        private static dynamic currentConfig;
        public static void Load()
        {
            using (StreamReader file = File.OpenText(@"config.json"))
            {
                currentConfig = JsonConvert.DeserializeObject(file.ReadToEnd());
            }
            
        }
    }
}
