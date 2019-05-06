using System;
using System.Threading.Tasks;
using AddressAnalyzer.Common.DataContracts;
using AddressAnalyzer.Common.HttpClients;
using Newtonsoft.Json;

namespace AddressAnalyzer.Common.Workers
{ 
    public abstract class AnalysisWorkerBase<T> where T : ServiceAnalysisResultBase, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:AddressAnalyzer.Common.Workers.AnalysisWorkerBase`1"/> class.
        /// </summary>
        /// <param name="sourceUrl">Location of the endpoint.</param>
        protected AnalysisWorkerBase(Uri sourceUrl)
        {
            SourceUrl = sourceUrl ?? throw new ArgumentNullException(nameof(sourceUrl));
        }

        /// <summary>
        /// The location of the API.
        /// </summary>
        /// <value>The source URL.</value>
        protected Uri SourceUrl { get; }
        private readonly RestClient _restClient = new RestClient();

        /// <summary>
        /// Fetches the given data as defined in the type parameter.
        /// </summary>
        /// <returns>The data from the API.</returns>
        public async Task<T> FetchDataAsync()
        {
            string apiResult = await _restClient.GetAsync(SourceUrl);

            if (string.IsNullOrWhiteSpace(apiResult))
            {
                ServiceAnalysisResultBase result = (ServiceAnalysisResultBase)Activator.CreateInstance<T>();

                result.IsSuccessful = false;
                result.ResultText = string.Empty;

                return result as T;
            }

            return JsonConvert.DeserializeObject<T>(apiResult);
        }
    }
}
