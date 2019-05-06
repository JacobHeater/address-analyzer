using System;
using AddressAnalyzer.Common.DataContracts;

namespace AddressAnalyzer.Common.Workers
{
    /// <summary>
    /// This class is responsible for talking to the Ping
    /// API that is running in the background for the
    /// aggregrator API to utilize.
    /// </summary>
    public class PingWorker : AnalysisWorkerBase<PingAnalysisResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:AddressAnalyzer.Common.Workers.PingWorker"/> class.
        /// </summary>
        /// <param name="sourceUrl">Location of the Ping API.</param>
        public PingWorker(Uri sourceUrl) : base(sourceUrl)
        {
        }
    }
}
