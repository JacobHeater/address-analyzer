using System;
using AddressAnalyzer.Common.DataContracts;

namespace AddressAnalyzer.Common.Workers
{
    /// <summary>
    /// This class is responsible for talking to the
    /// Virus Total API that is running in the background
    /// for the aggregator API to use.
    /// </summary>
    public class VirusTotalWorker : AnalysisWorkerBase<VirusTotalAnalysisResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:AddressAnalyzer.Common.Workers.VirusTotalWorker"/> class.
        /// </summary>
        /// <param name="sourceUrl">Location of the Virus Total API.</param>
        public VirusTotalWorker(Uri sourceUrl) : base(sourceUrl)
        {
        }
    }
}
