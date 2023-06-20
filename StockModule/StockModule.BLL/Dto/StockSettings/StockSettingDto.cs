using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.BLL.StockSettings
{
    public class StockSettingDto
    {
        public int Id { get; set; }
        public string CollectionType { get; set; } = "Warehouse";
        public bool? Reproducible { get; set; } = true;
    }
}
