using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BalancR.Models.CosmosDb
{
    public class BaseCosmosObject
    {
        [JsonProperty(PropertyName = "partitionkey")]
        public string Id { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
