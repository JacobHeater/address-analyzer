using System;
using System.Threading.Tasks;
using AddressAnalyzer.Common.DataContracts;
using AddressAnalyzer.Common.HttpClients;
using Newtonsoft.Json;

namespace AddressAnalyzer.Common.Workers
{
    public abstract class AnalysisWorkerBase<T> where T : ServiceAnalysisResultBase, new()
    {
        protected AnalysisWorkerBase(Uri sourceUrl)
        {
            SourceUrl = sourceUrl ?? throw new ArgumentNullException(nameof(sourceUrl));
        }

        protected Uri SourceUrl { get; }
        private readonly RestClient _restClient = new RestClient();

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
