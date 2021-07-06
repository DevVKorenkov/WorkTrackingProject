using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTrackingLib.ProcessClasses
{
    class NotForUserData : Attribute
    {
        public string PropertyName { get; set; }

        public NotForUserData()
        {
        }
    }
}
