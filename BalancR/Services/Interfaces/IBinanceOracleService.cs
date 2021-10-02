using Binance.Net.Objects.Spot.MarketData;
using System.Threading.Tasks;

namespace BalancR.Services.Interfaces
{
    public interface IBinanceOracleService
    {
        Task<BinancePrice> GetETHUSDCPair();
    }
}