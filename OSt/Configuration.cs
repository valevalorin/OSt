using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace OSt
{
    class Configuration
    {
        private const string DEFAULT_CONFIGURATION_FILE_LOCATION = @"config.json";
        private string configurationFileLocation;
        private dynamic currentConfig;
        public dynamic Current { get { return currentConfig; } }
        private List<Assignment> assignments;
        public List<Assignment> Assignments { get { return this.assignments; } }

        #region REGION - IO
        public static Configuration Load()
        {
            Configuration config = new Configuration();
            config.configurationFileLocation = DEFAULT_CONFIGURATION_FILE_LOCATION;
            config._Load();
            return config;
        }

        public static Configuration Load(string otherConfigurationFile)
        {
            Configuration config = new Configuration();
            config.configurationFileLocation = otherConfigurationFile;
            config._Load();
            return config;
        }

        private void _Load()
        {
            using (StreamReader file = File.OpenText(configurationFileLocation))
            {
                this.currentConfig = JsonConvert.DeserializeObject(file.ReadToEnd());
            }
        }

        public void SaveToDisk()
        {
            //always write back to the root config file
            using (StreamWriter writer = new StreamWriter(File.OpenWrite(DEFAULT_CONFIGURATION_FILE_LOCATION)))
            {
                writer.Write(
                    JsonConvert.SerializeObject(
                        this.currentConfig,
                        Formatting.Indented
                    )
                );
            }
        }
        #endregion

        #region REGION - Configuration Modification
        public void SetAssignments(List<Assignment> assignments)
        {
            this.assignments = assignments;
        }

        public void AddAssignment(Assignment a)
        {
            this.assignments.Add(a);
        }

        public int RemoveAssignment(string processName)
        {
            int index = FindAssignment(processName);
            if(index != -1)
            {
                this.assignments.RemoveAt(index);
            }
            return index;
        }
        #endregion

        #region REGION - Utils
        private int FindAssignment(string processName)
        {
            for(int i = 0; i < this.assignments.Count; i++)
            {
                Assignment a = this.assignments[i];
                if(a.ProcessName.Equals(processName))
                {
                    return i;
                }
            }
            return -1;
        }
        #endregion
    }
}
