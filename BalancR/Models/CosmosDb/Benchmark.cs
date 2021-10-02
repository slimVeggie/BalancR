using System;
using System.Collections.Generic;
using System.Text;

namespace BalancR.Models.CosmosDb
{
    public class Benchmark : BaseCosmosObject
    {
        public decimal EthValue { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
