using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSt
{
    class Assignment
    {
        public string ProcessName { get; set; }
        public Track Track { get; set; }

        public Assignment (string processName, string trackPath)
        {
            this.ProcessName = processName;
            this.Track = new Track(trackPath);
        }

        public override string ToString()
        {
            return this.ProcessName.ToUpper();
        }
    }
}
