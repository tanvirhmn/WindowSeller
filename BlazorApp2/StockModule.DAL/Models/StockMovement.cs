using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.DAL.Models
{
    [Index(nameof(DocumentNumber))]
    public class StockMovement
    {
        public int Id { get; set; }
        public int? FromStockId { get; set; }
        public int? ToStockId { get; set; }
        public double Quantity { get; set; }
        public double FromTotalQuantity { get; set; }
        public double ToTotalQuantity { get; set; }
        public int EmployeeId { get; set; }
        public int ReasonId { get; set; }
        public string Comment { get; set; } = String.Empty;
        public int DocumentId { get; set; }
        public int DocumentNumber { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime DocumentDate { get; set; }


        [ForeignKey("FromStockId")]
        public virtual Stock? FromStock { get; set; }

        [ForeignKey("ToStockId")]
        public virtual Stock? ToStock { get; set; }

        [ForeignKey("DocumentId")]
        public virtual StockDocument StockDocument { get; set; } = null!;

        [ForeignKey("ReasonId")]
        public virtual StockMovementReason StockMovementReason { get; set; } = null!;

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; } = null!;
    }
}
