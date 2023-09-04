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
    public class Stock
    {
        public int Id { get; set; }
        public int MaterialId { get; set; }
        public int WarehouseID { get; set; }
        public double Length { get; set; }
        public double Height { get; set; }
        public double Quantity { get; set; }
        public DateTime LastDocumentDate { get; set; }

        [ForeignKey("MaterialId")]
        public virtual Material Material { get; set; } = null!;

        [ForeignKey("WarehouseID")]
        public virtual Warehouse Warehouse { get; set; } = null!;

        [JsonIgnore]
        [InverseProperty("FromStock")]
        public virtual IEnumerable<StockMovement>? FromStockMovemenets { get; set; }

        [JsonIgnore]
        [InverseProperty("ToStock")]
        public virtual IEnumerable<StockMovement>? ToStockMovemenets { get; set; }
    }
}
