using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.BLL
{
    public class ReproductionMovement
    {
        public double Quantity { get; set; }
        public int Employee { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public int DocumentId { get; set; }
        public string Document { get; set; } = string.Empty;
        public string WarehouseFrom { get; set; } = string.Empty;
        public string WarehouseTo { get; set; } = string.Empty;
        public string MaterialCode { get; set; } = string.Empty;
        public double Length { get; set; }
    }
}
