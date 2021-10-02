using Binance.Net.Objects.Spot.MarketData;
using System.Threading.Tasks;

namespace BalancR.Services.Interfaces
{
    public interface IWalletManagementService
    {
        Task BuyAtMarketPriceAsync(BinancePrice binancePrice, decimal volatility);
        Task SellAtMarketPriceAsync(BinancePrice binancePrice, decimal volatility);
    }
}