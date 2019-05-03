using System;
using AddressAnalyzer.Common.DataContracts;

namespace AddressAnalyzer.Common.Workers
{
    public class ReverseDnsWorker : AnalysisWorkerBase<ReverseDnsAnalysisResult>
    {
        public ReverseDnsWorker(Uri sourceUrl) : base(sourceUrl)
        {
        }
    }
}
