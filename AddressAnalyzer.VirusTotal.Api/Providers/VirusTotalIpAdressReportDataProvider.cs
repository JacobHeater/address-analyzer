using AddressAnalyzer.Common.DataProviders;

namespace AddressAnalyzer.VirusTotal.Api.Providers
{
    public class VirusTotalIpAdressReportDataProvider : DataProviderBase
    {
        public override string ProviderUrl => "http://www.virustotal.com/vtapi/v2/ip-address/report";
    }
}
