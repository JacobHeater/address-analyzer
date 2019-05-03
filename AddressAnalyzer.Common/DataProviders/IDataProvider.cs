using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddressAnalyzer.Common.DataProviders
{
    public interface IDataProvider
    {
        Uri GetUrl(string path);
        Uri GetUrl();
        Task<string> GetResultAsync(Uri uri);
        Task<string> GetResultAsync(Uri uri, Dictionary<string, string> parameters);
        Task<string> PostDataAsync<TData>(Uri uri, TData payload) where TData : class, new();
    }
}
