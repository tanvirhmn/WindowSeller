using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowSeller.Domain
{
    [JsonObject(IsReference = true)]
    public partial class Order
    {
        public long OrderId { get; set; }
        public string OrderName { get; set; }
        public string State { get; set; }

        [JsonIgnore]
        public ICollection<Window>? Windows { get; set; }
    }
}
