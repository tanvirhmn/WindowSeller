using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowSellerWASM.BLL.DTOs
{
    public class WindowDto
    {
        public long WindowId { get; set; }
        public string WindowName { get; set; }
        public int QuantityOfWindows { get; set; }
        public int TotalSubELements { get; set; }

        public long OrderId { get; set; }
    }
}
