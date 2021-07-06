using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WorkTrackingLib.Models;

namespace WorkTrackingSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateController : ControllerBase
    {
        [HttpGet("Check")]
        public ProgramInfo Check()
        {
            try
            {
                return JsonConvert.DeserializeObject<ProgramInfo>(System.IO.File.ReadAllText($@"{AppDomain.CurrentDomain.BaseDirectory}/wwwroot/Update/version.json"));
            }
            catch
            {
                return null;
            }
        }
    }


}