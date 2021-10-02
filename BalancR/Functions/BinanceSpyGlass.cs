using System;
using System.Threading.Tasks;
using BalancR.Services;
using BalancR.Services.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using BalancR.Models.CosmosDb;

namespace BalancR.Functions
{
    public class BinanceSpyGlass
    {
        private readonly IBinanceOracleService _binanceOracleService;
        private readonly IWalletManagementService _walletManagementService;
        private readonly CosmosContext _cosmosContext;

        public BinanceSpyGlass(IBinanceOracleService binanceOracleService, IWalletManagementService walletManagementService, CosmosContext cosmosContext)
        {
            _binanceOracleService = binanceOracleService;
            _walletManagementService = walletManagementService;
            _cosmosContext = cosmosContext;
            _cosmosContext.Database.EnsureCreated();
        }

        [FunctionName("BinanceSpyGlass")]
        //Real 5 mins timer: 0 */5 * * * *
        public async Task RunAsync([TimerTrigger("0 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"Starting Alogrythm execution");

            var pairRates = await _binanceOracleService.GetETHUSDCPair();
            log.LogInformation(pairRates.ToString());

            var latestBenchmark = _cosmosContext.Benchmarks
                                    .OrderByDescending(b => b.Timestamp)
                                    .FirstOrDefault();

            var volatility = (latestBenchmark.EthValue - pairRates.Price) / ((latestBenchmark.EthValue + pairRates.Price) / 2); //https://www.calculatorsoup.com/calculators/algebra/percent-difference-calculator.php for equation
            if (0.02M < volatility)
            {
                await _walletManagementService.SellAtMarketPriceAsync(pairRates, volatility);

            }
            else if (volatility < - 0.02M)
            {
                await _walletManagementService.BuyAtMarketPriceAsync(pairRates, volatility);
            }



            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
