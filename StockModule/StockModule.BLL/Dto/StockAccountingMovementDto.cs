using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.BLL.Dto
{
    public class StockAccountingMovementDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string MaterialCode { get; set; } = String.Empty;
        public string MaterialDescription { get; set; } = String.Empty;
        public string Movement { get; set; } = String.Empty;
        public string Type { get; set; } = String.Empty;
        public string Measure { get; set; } = String.Empty;
        public double Quantity { get; set; }       
        public int Status { get; set; }
        public string? LastResponseMessage { get; set; }       
     
    }  
}
