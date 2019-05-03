using System;
using AddressAnalyzer.Common.DataContracts;

namespace AddressAnalyzer.Common.Workers
{
    public class PingWorker : AnalysisWorkerBase<PingAnalysisResult>
    {
        public PingWorker(Uri sourceUrl) : base(sourceUrl)
        {
        }
    }
}
