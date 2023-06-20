using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.BLL.SubClasses.RivileModels
{
    public class RivileData
    {
        public RivileData(RivileDataI09? i09)
        {
            I09 = i09;
        }
        public RivileDataI09? I09 { get; set; }
    }
}
