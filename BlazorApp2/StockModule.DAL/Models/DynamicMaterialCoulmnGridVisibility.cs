using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.DAL.Models
{
    public class DynamicMaterialCoulmnGridHiding
    {
        public int Id { get; set; }
        public string? UserID { get; set; }
        public int MaterialColumnId { get; set; }
        [ForeignKey("MaterialColumnId")]
        public MaterialColumn? MaterialColumn { get; set; }
    }
}
