using System;
using System.Collections.Generic;
using System.Text;

namespace BalancR.Models.CosmosDb
{
    public class AccountBalance : BaseCosmosObject
    {
        public decimal USDCBalance { get; set; }
        public double EthBalance { get; set; }
        public decimal UsdcToEth { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
