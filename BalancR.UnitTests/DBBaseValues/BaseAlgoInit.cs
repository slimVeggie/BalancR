using BalancR.Models.CosmosDb;
using BalancR.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BalancR.UnitTests.DBBaseValues
{
    [TestClass]
    public class BaseAlgoInit
    {
        private readonly WalletManagementService _walletManagementService;
        private readonly CosmosContext _cosmosContext;
        private readonly BinanceOracleService _binanceOracleService;

        public BaseAlgoInit()
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
            dbcontext.Database.EnsureDeleted();
            dbcontext.Database.EnsureCreated();

            _walletManagementService = new WalletManagementService(dbcontext);
        }

        [TestMethod]
        public async Task DefineStartingAccountBalanceToCosmos()
        {
            //Arrange
            var pairRate = await _binanceOracleService.GetETHUSDCPair();

            var accountBalance = new AccountBalance()
            {
                Id = Guid.NewGuid().ToString(),
                EthBalance = (double)(500 / pairRate.Price),
                USDCBalance = 500,
                UsdcToEth = pairRate.Price
            };

            //Act
            await _cosmosContext.AccountBalances.AddAsync(accountBalance);
            await _cosmosContext.SaveChangesAsync();

            //Assert
            Assert.IsNotNull(accountBalance);
        }

        [TestMethod]
        public async Task DefineStartingBenchmarkToCosmos()
        {
            //Arrange
            var pairRate = await _binanceOracleService.GetETHUSDCPair();
            var benchmark = new Benchmark()
            {
                Id = Guid.NewGuid().ToString(),
                EthValue = pairRate.Price
            };

            //Act
            await _cosmosContext.Benchmarks.AddAsync(benchmark);
            await _cosmosContext.SaveChangesAsync();

            //Assert
            Assert.IsNotNull(benchmark);
        }
    }
}