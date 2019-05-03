namespace AddressAnalyzer.Common.DataContracts
{
    public class AnalysisResult
    {
        public GeoIpAnalysisResult GeoIp { get; set; }
        public ReverseDnsAnalysisResult ReverseDns { get; set; }
        public RdapAnalysisResult Rdap { get; set; }
        public VirusTotalAnalysisResult VirusTotal { get; set; }
        public PingAnalysisResult Ping { get; set; }
    }
}
