using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Install_Printers_Lib.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using WorkTrackingSite.Attributes;
using WorkTrackingSite.Context;

namespace WorkTrackingSite.Controllers
{
    public class InstallPrintersIndexController : Controller
    {
        public IActionResult InstallPrintersIndex()
        {
            return View();
        }      
    }
}