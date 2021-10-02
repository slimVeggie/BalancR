using BalancR.Models.CosmosDb;
using BalancR.Services.Interfaces;
using Binance.Net.Objects.Spot.MarketData;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BalancR.Services
{
    

    public class WalletManagementService : IWalletManagementService
    {
        private readonly CosmosContext _cosmosContext;

        public WalletManagementService(CosmosContext cosmosService)
        {
            _cosmosContext = cosmosService;
        }

        public async Task BuyAtMarketPriceAsync(BinancePrice binancePrice, decimal volatility)
        {
            var accountBalance = _cosmosContext.AccountBalances
                                    .OrderByDescending(AB => AB.Timestamp)
                                    .FirstOrDefault();

            var amount = (accountBalance.USDCBalance * Math.Abs(volatility)) * 2;

            var transaction = new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                USDAmountValue = amount,
                StrikePrice = binancePrice.Price,
                Operation = Common.OperationType.Buy,
                timestamp = DateTime.Now
            };

            await _cosmosContext.Transactions.AddAsync(transaction);

            var benchmark = new Benchmark()
            {
                Id = Guid.NewGuid().ToString(),
                EthValue = binancePrice.Price,
                Timestamp = DateTime.Now
            };
            await _cosmosContext.Benchmarks.AddAsync(benchmark);

            await _cosmosContext.SaveChangesAsync();
        }

        public async Task SellAtMarketPriceAsync(BinancePrice binancePrice, decimal volatility)
        {
            var accountBalance = _cosmosContext.AccountBalances
                                    .OrderByDescending(AB => AB.Timestamp)
                                    .FirstOrDefault();

            var amount = (accountBalance.USDCBalance * Math.Abs(volatility)) * 2;

            var transaction = new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                USDAmountValue = amount,
                StrikePrice = binancePrice.Price,
                Operation = Common.OperationType.Sell,
                timestamp = DateTime.Now
            };

            await _cosmosContext.Transactions.AddAsync(transaction);

            var benchmark = new Benchmark()
            {
                Id = Guid.NewGuid().ToString(),
                EthValue = binancePrice.Price,
                Timestamp = DateTime.Now
            };
            await _cosmosContext.Benchmarks.AddAsync(benchmark);

            await _cosmosContext.SaveChangesAsync();
        }
    }
}
