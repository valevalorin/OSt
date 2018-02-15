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
        public List<Track> Tracks { get; set; }

        public Assignment (string processName, string trackPath)
        {
            this.ProcessName = processName;
            this.Track = new Track(trackPath);
        }

        public Assignment(string processName, List<string> trackPaths)
        {
            this.ProcessName = processName;
            foreach(string trackPath in trackPaths)
            {
                Track t = new Track(trackPath);
                this.Tracks.Add(t);
            }
        }

        public override string ToString()
        {
            return this.ProcessName.ToUpper();
        }
    }
}
