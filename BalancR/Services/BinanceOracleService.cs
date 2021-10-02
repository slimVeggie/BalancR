using BalancR.Common.Exceptions;
using BalancR.Services.Interfaces;
using Binance.Net;
using Binance.Net.Objects;
using Binance.Net.Objects.Spot.MarketData;
using CryptoExchange.Net.Authentication;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BalancR.Services
{
    public class BinanceOracleService : IBinanceOracleService
    {
        public ILogger _logger { get; set; }
        public BinanceOracleService(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<BinancePrice> GetETHUSDCPair()
        {
            _logger.LogInformation("Getting ETH-USDC Pair");
            var client = GetBinanceClient();

            var result = await client.Spot.Market.GetPriceAsync("ETHUSDC");

            if (!result.Success)
            {
                _logger.LogError(result.Error.Message);
                throw new BalancrException("Binance call to ETH-USDC Pair failed with error " + result.Error.Message);
            }

            _logger.LogInformation(result.Data.ToString());

            return result.Data;
        }

        private BinanceClient GetBinanceClient()
        {
            var client = new BinanceClient(new BinanceClientOptions() {
                ApiCredentials = new ApiCredentials("bZN1bBxkemleuMsR8qvB0LvBeG3ogj9khpKNyfoIhyzwLCLG5hv6eCVzWkqMtQI2", "VbZ4UVJRGEtTMhzN4lGZ4MpvW8azf8MgA9uXoId0W4UNEhh55GwzQpi69aYLoIUt"),
                AutoTimestamp = true
            });

            return client;
        }
    }
}
