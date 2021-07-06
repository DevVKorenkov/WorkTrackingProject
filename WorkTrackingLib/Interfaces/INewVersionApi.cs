using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTrackingLib.Interfaces
{
    public interface INewVersionApi
    {
        bool CheckUpdate(int version);
    }
}
