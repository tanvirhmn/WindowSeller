using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.BLL.Logic
{
    public class RequestMoveStock
    {
      

        public string Sender { get; set; } = string.Empty;

        public int TaskId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string SelectCycleOrders { get; set; } = string.Empty;
        public EnumTaskEvent TaskEvent { get; set; } = EnumTaskEvent.None;
        public bool? ReturnWarehouse { get; set; } = null;
        public IList<MoveStock> MoveStocks { get; set; } = new List<MoveStock>();

    }

    public enum EnumTaskEvent
    {
        Delete = -1,
        None = 0,
        Update = 1
    }

    public class MoveStock
    {
        public int DocumentNumber { get; set; }
        public int EmployeeId { get; set; }
        public string? TypeKey { get; set; }    
        public string? Reference { get; set; }
        public double Length { get; set; }
        public double Height { get; set; }
        public double Quantity { get; set; }
        public string? Comment { get; set; }
        public string? Option { get; set; }  


    }


}
