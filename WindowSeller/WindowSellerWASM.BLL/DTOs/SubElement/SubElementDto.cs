using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowSellerWASM.BLL.DTOs
{
    public class SubElementDto
    {
        public long SubElementId { get; set; }
        public int Element { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public string Type { get; set; }

        public long WindowId { get; set; }
    }
}
