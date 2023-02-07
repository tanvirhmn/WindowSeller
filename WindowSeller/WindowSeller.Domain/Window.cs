using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowSeller.Domain
{
    public partial class Window
    {
        public long WindowId { get; set; }
        public string WindowName { get; set; }
        public int QuantityOfWindows { get; set; }
        public int TotalSubELements { get; set; }

        public Order Order { get; set; }
        public long OrderId { get; set; }
        public ICollection<SubElement>? SubElements { get; set; }
    }
}
