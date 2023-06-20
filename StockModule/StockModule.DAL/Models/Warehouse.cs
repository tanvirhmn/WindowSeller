using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.DAL.Models
{
    [JsonObject(IsReference = true)]
    public class Warehouse
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;

        [JsonIgnore]
        public virtual IEnumerable<Stock>? Stocks { get; set; }
    }
}
