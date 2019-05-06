using System;
using AddressAnalyzer.Common.DataContracts;

namespace AddressAnalyzer.Common.Workers
{
    /// <summary>
    /// This class is responsible for talking to the
    /// RDAP API that is running in the background
    /// for the aggregator API to use.
    /// </summary>
    public class RdapWorker : AnalysisWorkerBase<RdapAnalysisResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:AddressAnalyzer.Common.Workers.RdapWorker"/> class.
        /// </summary>
        /// <param name="sourceUrl">Location of the RDAP API.</param>
        public RdapWorker(Uri sourceUrl) : base(sourceUrl)
        {
        }
    }
}
