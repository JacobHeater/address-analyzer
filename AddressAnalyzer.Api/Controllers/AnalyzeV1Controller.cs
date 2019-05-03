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

namespace AddressAnalyzer.Api.Controllers
{
    [ControllerName("Analyze")]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AnalyzeV1Controller : Controller
    {
        public AnalyzeV1Controller(IConfiguration config)
        {
            Config = config;
        }

        private IConfiguration Config { get; }

        [HttpGet("{address}")]
        public async Task<AnalysisResult> Get(string address)
        {
            this.ValidateAddressInput(address);

            return await Get("ping,geo,rdap", address);
        }

        [HttpGet("{servicelist}/{address}")]
        public async Task<AnalysisResult> Get(string servicelist, string address)
        {
            this.ValidateAddressInput(address);

            if (string.IsNullOrWhiteSpace(servicelist))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            string[] servicesToQuery = servicelist.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

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

        private async Task<GeoIpAnalysisResult> GetGeoIpDataAsync(string address)
        {
            string geoIpApiUrl = Config.GetValue<string>("geoIpApiUrl");
            Uri geoIpAnalyzeRoute = new Uri($"{geoIpApiUrl}/api/v1/analyze/{address}");

            GeoIpWorker geoIpWorker = new GeoIpWorker(geoIpAnalyzeRoute);
            GeoIpAnalysisResult result = await geoIpWorker.FetchDataAsync();

            return result;
        }

        private async Task<PingAnalysisResult> GetPingDataAsync(string address)
        {
            string pingApiUrl = Config.GetValue<string>("pingApiUrl");
            Uri pingAnalyzeRoute = new Uri($"{pingApiUrl}/api/v1/analyze/{address}");

            PingWorker pingWorker = new PingWorker(pingAnalyzeRoute);
            PingAnalysisResult result = await pingWorker.FetchDataAsync();

            return result;
        }

        private async Task<ReverseDnsAnalysisResult> GetReverseDnsDataAsync(string address)
        {
            string reverseDnsApiUrl = Config.GetValue<string>("reverseDnsApiUrl");
            Uri reverseDnsAnalyzeRoute = new Uri($"{reverseDnsApiUrl}/api/v1/analyze/{address}");

            ReverseDnsWorker reverseDnsWorker = new ReverseDnsWorker(reverseDnsAnalyzeRoute);
            ReverseDnsAnalysisResult result = await reverseDnsWorker.FetchDataAsync();

            return result;
        }

        private async Task<VirusTotalAnalysisResult> GetVirusTotalDataAsync(string address)
        {
            string virusTotalApiUrl = Config.GetValue<string>("virusTotalApiUrl");
            string virusTotalApiKey = Config.GetValue<string>("virusTotalKey");
            Uri virusTotalAnalyzeRoute = new Uri($"{virusTotalApiUrl}/api/v1/analyze/{virusTotalApiKey}/{address}");

            VirusTotalWorker virusTotalWorker = new VirusTotalWorker(virusTotalAnalyzeRoute);
            VirusTotalAnalysisResult result = await virusTotalWorker.FetchDataAsync();

            return result;
        }

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
