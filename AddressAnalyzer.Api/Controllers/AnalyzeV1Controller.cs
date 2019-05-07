using AddressAnalyzer.Common.DataContracts;
using Microsoft.AspNetCore.Mvc;
using AddressAnalyzer.Common.Extensions;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using AddressAnalyzer.Common.Workers;
using System;
using AddressAnalyzer.Common.Validation;
using System.Web.Http;
using System.Net;
using Microsoft.Extensions.Primitives;
using System.Linq;
using Swashbuckle.AspNetCore.Annotations;

namespace AddressAnalyzer.Api.Controllers
{
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ControllerName("Analyze")]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AnalyzeV1Controller : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:AddressAnalyzer.Api.Controllers.AnalyzeV1Controller"/> class.
        /// </summary>
        /// <param name="config">Config.</param>
        public AnalyzeV1Controller(IConfiguration config)
        {
            Config = config;
        }

        private IConfiguration Config { get; }

        /// <summary>
        /// Gets the data for the given address by querying
        /// the Ping, GeoIP, and RDAP endpoints by default.
        /// </summary>
        /// <returns>The aggregated data.</returns>
        /// <param name="address">Address to query.</param>
        [HttpGet("{address}")]
        [SwaggerOperation(Summary = "Gets information about the provided address from the default Ping and RDAP services.", Produces = new string[] { "application/json" })]
        public async Task<AnalysisResult> Get(string address)
        {
            this.ValidateAddressInput(address);

            AnalysisResult result = await Get("ping,rdap", address);

            return result;
        }

        /// <summary>
        /// Gets the data for the given address by querying
        /// the services presented in the service list.
        /// </summary>
        /// <returns>The aggregated data.</returns>
        /// <param name="servicelist">The services to utilize for the query.</param>
        /// <param name="address">Address to query.</param>
        [HttpGet("{servicelist}/{address}")]
        [SwaggerOperation(Summary = "Gets information about the provided address from the supplied list of services to query.", Produces = new string[] { "application/json" })]
        public async Task<AnalysisResult> Get(string servicelist, string address)
        {
            this.ValidateAddressInput(address);

            if (string.IsNullOrWhiteSpace(servicelist))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var servicesToQuery = servicelist
                .Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .Distinct();

            AnalysisResult result = new AnalysisResult();

            foreach (string service in servicesToQuery)
            {
                switch(service)
                {
                    case "vt":
                        result.VirusTotal = await GetVirusTotalDataAsync(address);
                        break;
                    case "rdap":
                        result.Rdap = await GetRdapDataAsync(address);
                        break;
                    case "rdns":
                        result.ReverseDns = await GetReverseDnsDataAsync(address);
                        break;
                    case "ping":
                        result.Ping = await GetPingDataAsync(address);
                        break;
                    case "geo":
                        result.GeoIp = await GetGeoIpDataAsync(address);
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the Geo IP information from the Geo IP endpoint.
        /// </summary>
        /// <returns>The geo ip data.</returns>
        /// <param name="address">Address to query.</param>
        private async Task<GeoIpAnalysisResult> GetGeoIpDataAsync(string address)
        {
            string geoIpApiUrl = Config.GetValue<string>("geoIpApiUrl");
            Uri geoIpAnalyzeRoute = new Uri($"{geoIpApiUrl}/api/v1/analyze/{address}");

            GeoIpWorker geoIpWorker = new GeoIpWorker(geoIpAnalyzeRoute);
            GeoIpAnalysisResult result = await geoIpWorker.FetchDataAsync();

            return result;
        }

        /// <summary>
        /// Gets the Ping information from the Ping endpoint.
        /// </summary>
        /// <returns>The ping data.</returns>
        /// <param name="address">Address to query.</param>
        private async Task<PingAnalysisResult> GetPingDataAsync(string address)
        {
            string pingApiUrl = Config.GetValue<string>("pingApiUrl");
            Uri pingAnalyzeRoute = new Uri($"{pingApiUrl}/api/v1/analyze/{address}");

            PingWorker pingWorker = new PingWorker(pingAnalyzeRoute);
            PingAnalysisResult result = await pingWorker.FetchDataAsync();

            return result;
        }

        /// <summary>
        /// Gets the Reverse DNS information from the Reverse DNS endpoint.
        /// </summary>
        /// <returns>The reverse dns data.</returns>
        /// <param name="address">Address to query.</param>
        private async Task<ReverseDnsAnalysisResult> GetReverseDnsDataAsync(string address)
        {
            string reverseDnsApiUrl = Config.GetValue<string>("reverseDnsApiUrl");
            Uri reverseDnsAnalyzeRoute = new Uri($"{reverseDnsApiUrl}/api/v1/analyze/{address}");

            ReverseDnsWorker reverseDnsWorker = new ReverseDnsWorker(reverseDnsAnalyzeRoute);
            ReverseDnsAnalysisResult result = await reverseDnsWorker.FetchDataAsync();

            return result;
        }

        /// <summary>
        /// Gets the Virus Total information from the Virus Total endpoint.
        /// </summary>
        /// <returns>The Virus Total data.</returns>
        /// <param name="address">Address to query.</param>
        private async Task<VirusTotalAnalysisResult> GetVirusTotalDataAsync(string address)
        {
            string virusTotalApiUrl = Config.GetValue<string>("virusTotalApiUrl");
            string virusTotalApiKey = string.Empty;

            Request.Headers.TryGetValue("X-VT-Key", out StringValues virusTotalApiKeyValues);

            if (virusTotalApiKeyValues.Count == 1) {
                virusTotalApiKey = virusTotalApiKeyValues.FirstOrDefault();
            }
            else
            {
                // Don't even bother to query since we don't
                // have an API key.
                return null;
            }

            Uri virusTotalAnalyzeRoute = new Uri($"{virusTotalApiUrl}/api/v1/analyze/{virusTotalApiKey}/{address}");

            VirusTotalWorker virusTotalWorker = new VirusTotalWorker(virusTotalAnalyzeRoute);
            VirusTotalAnalysisResult result = await virusTotalWorker.FetchDataAsync();

            return result;
        }

        /// <summary>
        /// Gets the RDAP information from the RDAP endpoint.
        /// </summary>
        /// <returns>The rdap data.</returns>
        /// <param name="address">Address to query.</param>
        private async Task<RdapAnalysisResult> GetRdapDataAsync(string address)
        {
            string rdapApiUrl = Config.GetValue<string>("rdapApiUrl");
            string type = string.Empty;

            switch (AddressValidator.GetAddressType(address))
            {
                case AddressType.Domain:
                    type = "domain";
                    break;
                case AddressType.IPAddress:
                    type = "ip";
                    break;
                default:
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            Uri rdapAnalyzeRoute = new Uri($"{rdapApiUrl}/api/v1/analyze/{type}/{address}");
            RdapWorker rdapWorker = new RdapWorker(rdapAnalyzeRoute);
            RdapAnalysisResult result = await rdapWorker.FetchDataAsync();

            return result;
        }
    }
}
