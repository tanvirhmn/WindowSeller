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
    public class StockMovementReason
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string GroupName { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string RivileCenter { get; set; } = String.Empty;
        public int? FromWarehouseId { get; set; }

        [ForeignKey("FromWarehouseId")]
        public virtual Warehouse? FromWarehouse { get; set; }
        public int? ToWarehouseId { get; set; }

        [ForeignKey("ToWarehouseId")]
        public virtual Warehouse? ToWarehouse { get; set; } 
        public bool Active { get; set; }

        public bool IsGenerateAccountingEvent { get; set; } = false;
        public int? AccountingEventCanceledReasonId { get; set; }


        [JsonIgnore]
        public virtual IEnumerable<StockMovement>? StockMovements { get; set; }
    }
}
