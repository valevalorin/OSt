using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OSt
{
    class Track
    {
        public string FullPath { get; set; }
        public string Name { get; set; }

        public Track (string fullPath, string name)
        {
            this.FullPath = fullPath;
            this.Name = name;
        }

        public Track(string fullPath)
        {
            this.FullPath = fullPath;
            this.Name = Path.GetFileName(this.FullPath);
        }
    }
}
