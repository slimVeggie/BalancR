using Microsoft.VisualStudio.TestTools.UnitTesting;
using BalancR.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BalancR.Services.Tests
{
    [TestClass]
    public class BinanceOracleServiceTests
    {
        private BinanceOracleService _binanceOracleService { get; set; }

        public BinanceOracleServiceTests()
        {
            Mock<ILogger> _loggerMock = new Mock<ILogger>();
            _binanceOracleService = new BinanceOracleService(_loggerMock.Object);
        }

        [TestMethod]
        public async Task GetETHUSDCPairTest()
        {
            //Arrange

            //Act
            var result = await _binanceOracleService.GetETHUSDCPair();
            //Assert
            Assert.IsNotNull(result);
        }
    }
}