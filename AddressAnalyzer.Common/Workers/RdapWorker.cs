using System;
using AddressAnalyzer.Common.DataContracts;

namespace AddressAnalyzer.Common.Workers
{
    public class RdapWorker : AnalysisWorkerBase<RdapAnalysisResult>
    {
        public RdapWorker(Uri sourceUrl) : base(sourceUrl)
        {
        }
    }
}
