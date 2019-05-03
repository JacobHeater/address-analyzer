using System;
using AddressAnalyzer.Common.DataContracts;

namespace AddressAnalyzer.Common.Workers
{
    public class VirusTotalWorker : AnalysisWorkerBase<VirusTotalAnalysisResult>
    {
        public VirusTotalWorker(Uri sourceUrl) : base(sourceUrl)
        {
        }
    }
}
