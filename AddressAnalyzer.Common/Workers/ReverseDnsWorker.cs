using System;
using AddressAnalyzer.Common.DataContracts;

namespace AddressAnalyzer.Common.Workers
{
    /// <summary>
    /// This class is responsible for talking to the
    /// Reverse DNS API that is running in the background
    /// for the aggregator API to use.
    /// </summary>
    public class ReverseDnsWorker : AnalysisWorkerBase<ReverseDnsAnalysisResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:AddressAnalyzer.Common.Workers.ReverseDnsWorker"/> class.
        /// </summary>
        /// <param name="sourceUrl">Location of the Reverse DNS API.</param>
        public ReverseDnsWorker(Uri sourceUrl) : base(sourceUrl)
        {
        }
    }
}
