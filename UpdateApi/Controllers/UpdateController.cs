using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WorkTrackingLib.Models;

namespace UpdateApi.Controllers
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
                return JsonConvert.DeserializeObject<ProgramInfo>(System.IO.File.ReadAllText($@"{AppDomain.CurrentDomain.BaseDirectory}/wwwroot/version.json"));
            }
            catch
            {
                return null;
            }        
        }

        //[HttpGet("GetNewVersion")]
        //public NewVersionClass GetNewVersion()
        //{
        //    return new NewVersionClass { NewVersionArray = System.IO.File.ReadAllBytes($@"{AppDomain.CurrentDomain.BaseDirectory}/wwwroot/worktrackingupdate.zip") };
        //}
    }
}
