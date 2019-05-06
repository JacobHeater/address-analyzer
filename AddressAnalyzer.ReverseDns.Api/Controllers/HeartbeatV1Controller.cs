using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AddressAnalyzer.ReverseDns.Api.Controllers
{
    [ControllerName("Heartbeat")]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class HeartbeatV1Controller : Controller
    {
        [HttpGet]
        public string Get()
        {
            return "Address Analyzer Reverse DNS API Version 1";
        }
    }
}
