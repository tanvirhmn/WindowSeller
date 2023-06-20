using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.BLL
{
    public class CollectionMovement
    {
        public string TypeKey { get; set; } = "";
        public int TaskId { get; set; }
        public string Reference { get; set; } = "";
        public double Quantity { get; set; }
        public double Length { get; set; }
        public double Height { get; set; }
        public int Cycle { get; set; }
        public int EmployeeId { get; set; }
    }
}
