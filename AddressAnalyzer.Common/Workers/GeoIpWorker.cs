using System;
using AddressAnalyzer.Common.DataContracts;

namespace AddressAnalyzer.Common.Workers
{
    /// <summary>
    /// This class is responsible for talking to the
    /// Geo IP API that is running in the background
    /// for the aggregator API to use.
    /// </summary>
    public class GeoIpWorker : AnalysisWorkerBase<GeoIpAnalysisResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:AddressAnalyzer.Common.Workers.GeoIpWorker"/> class.
        /// </summary>
        /// <param name="resourceUrl">Location of the API.</param>
        public GeoIpWorker(Uri resourceUrl) : base(resourceUrl)
        {
        }
    }
}
