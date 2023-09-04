using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.DAL.Models
{
  
    public class StockAccountingAction
    {
        public int Id { get; set; }
        public int StockAccountingMovementId { get; set; }
        public string Method { get; set; } = String.Empty;
        public string Request { get; set; } = String.Empty;
        public string Response { get; set; } = String.Empty;           
        public bool IsSuccess { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey("StockAccountingMovementId")]
        public virtual StockAccountingMovement StockAccountingMovement { get; set; } = null!;

    }
}
