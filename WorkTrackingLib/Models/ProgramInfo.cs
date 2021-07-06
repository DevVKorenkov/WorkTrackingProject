using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTrackingLib.Models
{
    public class ProgramInfo
    {
        public string ProgramName { get; set; }

        public int Version { get; set; }

        public DateTime VersionReleaseDate { get; set; }
    }
}
