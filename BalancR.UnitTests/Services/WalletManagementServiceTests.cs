using Microsoft.VisualStudio.TestTools.UnitTesting;
using BalancR.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using Binance.Net.Objects.Spot.MarketData;
using BalancR.Models.CosmosDb;
using Moq;
using Microsoft.Extensions.Logging;

namespace BalancR.Services.Tests
{
    [TestClass]
    public class WalletManagementServiceTests
    {
        private readonly WalletManagementService _walletManagementService;
        private readonly CosmosContext _cosmosContext;
        private readonly BinanceOracleService _binanceOracleService;

        public WalletManagementServiceTests()
        {
            Mock<ILogger> _loggerMock = new Mock<ILogger>();
            _binanceOracleService = new BinanceOracleService(_loggerMock.Object);

            IConfiguration config;
            config = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .AddJsonFile("local.settings.json", optional: true)
                   .Build();

            var dbcontext = new CosmosContext(config);
            _cosmosContext = dbcontext;
            dbcontext.Database.EnsureCreated();

            _walletManagementService = new WalletManagementService(dbcontext);
        }

        [TestMethod]
        public async Task BuyAtMarketPriceAsyncTestAsync()
        {
            //Arrange
            var binancePrice = new BinancePrice()
            {
                Price = 3301.47M,
                Symbol = "ETHUSDC",
                Timestamp = DateTime.Now
            };

            //Act
            await _walletManagementService.BuyAtMarketPriceAsync(binancePrice, 0.03M);

            //Assert
            Assert.IsNotNull(binancePrice);
        }
        
        


    }
}