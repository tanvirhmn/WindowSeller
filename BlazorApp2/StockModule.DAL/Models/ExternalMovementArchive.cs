using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.DAL.Models
{
    public class ExternalMovementArchive
    {
        public int Id { get; set; }
        public string MaterialCode { get; set; } = "";
        public double Length { get; set; }
        public double Height { get; set; }
        public string ReasonName { get; set; } = "";
        public double Quantity { get; set; }
        public string Comment { get; set; } = "";
        public int DocumentNumber { get; set; }
        public string DocumentName { get; set; } = "";
        public string UserName { get; set; } = "";
        public DateTime DocumentDate { get; set; }
    }
}
