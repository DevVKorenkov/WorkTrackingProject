using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WorkTrackingSite.Controllers
{
    public class DownloadSkySearcherController : Controller
    {
        public IActionResult DownloadSkySearcher()
        {
            return View();
        }
    }
}