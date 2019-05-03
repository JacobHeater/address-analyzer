using Microsoft.AspNetCore.Mvc;


namespace AddressAnalyzer.Ping.Api.Controllers
{
    [ControllerName("Heartbeat")]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class HeartbeatV1Controller : Controller
    {
        [HttpGet]
        public string Get()
        {
            return "Address Analyzer Ping Api Version 1";
        }
    }
}
