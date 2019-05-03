using System;
using System.Threading.Tasks;

namespace AddressAnalyzer.Common.DataProviders
{
    public interface IDataProvider
    {
        Uri GetUrl(string path);
        Task<string> GetResultAsync(Uri uri);
    }
}
