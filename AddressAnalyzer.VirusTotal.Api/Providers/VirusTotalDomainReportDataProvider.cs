using AddressAnalyzer.Common.DataProviders;

namespace AddressAnalyzer.VirusTotal.Api.Providers
{
    public class VirusTotalDomainReportDataProvider : DataProviderBase
    {
        public override string ProviderUrl => "http://www.virustotal.com/vtapi/v2/domain/report";
    }
}
