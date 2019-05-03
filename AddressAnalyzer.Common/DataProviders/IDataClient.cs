using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddressAnalyzer.Common.DataProviders
{
    public interface IDataClient
    {
        Task<string> GetDataAsync(string address);
        Task<string> GetDataAsync(Dictionary<string, string> parameters);
        Task<string> PostDataAsync<TData>(TData payload) where TData : class, new();
    }
}
