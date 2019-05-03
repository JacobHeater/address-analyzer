using System;
using System.Threading.Tasks;
using AddressAnalyzer.Common.HttpClients;
using Newtonsoft.Json;

namespace AddressAnalyzer.Common.Workers
{
    public abstract class AnalysisWorkerBase<T> where T : class, new()
    {
        protected AnalysisWorkerBase(Uri sourceUrl)
        {
            SourceUrl = sourceUrl ?? throw new ArgumentNullException(nameof(sourceUrl));
        }

        protected Uri SourceUrl { get; }
        private readonly RestClient _restClient = new RestClient();

        public async Task<T> FetchDataAsync()
        {
            string json = await _restClient.GetAsync(SourceUrl);

            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
