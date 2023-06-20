using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockModule.DAL.Models
{
    [JsonObject(IsReference = true)]
    public partial class Material
    {
        public Material()
        {
            if(StockSetting == null)
            {
                StockSetting = new StockSetting() { Material = this};
            }
        }

        public int Id { get; set; }
        public string Code { get; set; } = String.Empty;
        public string Alias { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string Color { get; set; } = String.Empty;
        public string Type { get; set; } = String.Empty;
        public Single BarLength { get; set; } = 0;
        public virtual StockSetting? StockSetting { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<Stock>? Stocks { get; set; }
    }
}
