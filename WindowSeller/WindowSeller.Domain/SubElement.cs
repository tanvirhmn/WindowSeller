using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowSeller.Domain
{
    public partial class SubElement
    {
        public int SubElementId { get; set; }
        public int Element { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public string Type { get; set; }

        public Window Window { get; set; }  
        public long WindowId { get; set; }
    }
}
