using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkTrackingLib.Models;

namespace WorkTracking_Server.Models
{
    public class CheckClass
    {
        //public int CheckAccess(AccessModel access)
        //{
        //    if(access.Access < 1)
        //    {
        //        foreach(var a in access.GetType().GetProperties())
        //        {
        //            if(a.Name.Contains("Visibility"))
        //            {
        //                a.SetValue(access, false);
        //            }
        //        }

        //        return 0;
        //    }
        //    else if(access.Access == 1)
        //    {
        //        foreach (var a in access.GetType().GetProperties())
        //        {
        //            if (a.Name.Contains("Visibility") && a.Name != "VisibilityControlSql")
        //            {
        //                a.SetValue(access, true);
        //            }
        //        }

        //        access.VisibilityControlSql = false;

        //        return 1;
        //    }
        //    else
        //    {
        //        foreach (var a in access.GetType().GetProperties())
        //        {
        //            if (a.Name.Contains("Visibility"))
        //            {
        //                a.SetValue(access, true);
        //            }
        //        }

        //        return 2;
        //    }
        //}
    }
}
