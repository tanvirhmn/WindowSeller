using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.DAL.Models
{
    
    public class StockAccountingMovement
    {
        public int Id { get; set; }
        public int StockMovementId { get; set; }
        public int? ChangedByStockMovementId { get; set; }
        public int Status { get; set; }  
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        [ForeignKey("StockMovementId")]
        public virtual StockMovement StockMovement { get; set; } = null!;

        [ForeignKey("ChangedByStockMovementId")]
        public virtual StockMovement? ChangedByStockMovement { get; set; }

        [JsonIgnore]
        public virtual ICollection<StockAccountingAction>? StockAccountingActions { get; set; }

    }
}
