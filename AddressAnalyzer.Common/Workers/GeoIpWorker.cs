using System;
using AddressAnalyzer.Common.DataContracts;

namespace AddressAnalyzer.Common.Workers
{
    public class GeoIpWorker : AnalysisWorkerBase<GeoIpAnalysisResult>
    {
        public GeoIpWorker(Uri resourceUrl) : base(resourceUrl)
        {
        }
    }
}
