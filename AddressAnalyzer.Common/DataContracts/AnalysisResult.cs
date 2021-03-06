﻿using Newtonsoft.Json;

namespace AddressAnalyzer.Common.DataContracts
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class AnalysisResult
    {
        public GeoIpAnalysisResult GeoIp { get; set; }
        public ReverseDnsAnalysisResult ReverseDns { get; set; }
        public RdapAnalysisResult Rdap { get; set; }
        public VirusTotalAnalysisResult VirusTotal { get; set; }
        public PingAnalysisResult Ping { get; set; }
    }
}
