using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace AddressAnalyzer.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var host = WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

            string PORT = Environment.GetEnvironmentVariable("PORT");

            if (!string.IsNullOrWhiteSpace(PORT))
            {
                host.UseUrls($"http://0.0.0.0:{PORT}/");
            }

            return host;
        }
    }
}
