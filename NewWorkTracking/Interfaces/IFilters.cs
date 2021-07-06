using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewWorkTracking.Interfaces
{
    /// <summary>
    /// Object filtering interface 

    /// </summary>
    public interface IFilters
    {
        DateTime? DateOne { get; set; }

        DateTime? DateTwo { get; set; }

        string OsTypeFilter { get; set; }

        string SearchLine { get; set; }
    }
}
