using System;
using System.Collections.Generic;
using System.Text;

namespace BalancR.Models.CosmosDb
{
    public class TriggerAlert : BaseCosmosObject
    {
        public float MarketVolatility { get; set; }
        public float TriggerVolatility { get; set; }
    }
}
