using BalancR.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BalancR.Models.CosmosDb
{
    public class Transaction : BaseCosmosObject
    {
        public decimal USDAmountValue { get; set; }
        public decimal StrikePrice { get; set; }
        public DateTime timestamp { get; set; }
        public OperationType Operation { get; set; }
    }
}
