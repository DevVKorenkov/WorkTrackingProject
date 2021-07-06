using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WorkTrackingSite.Controllers
{
    public class DownloadController : Controller
    {
        public IActionResult Download()
        {
            return View();
        }
    }
}