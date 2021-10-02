using BalancR.Services;
using BalancR.Services.Interfaces;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

[assembly: FunctionsStartup(typeof(BalancR.Startup))]
namespace BalancR
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            IConfiguration config;

            config = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .AddJsonFile("local.settings.json", optional: true)
                   .Build();

            builder.Services.AddSingleton(ServiceDescriptor.Singleton(typeof(IConfiguration), config));
            builder.Services.AddLogging();

            builder.Services.AddTransient<IBinanceOracleService, BinanceOracleService>();
            builder.Services.AddSingleton<CosmosContext>();
        }
    }
}