using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.BLL.StockSettings
{
    public class MaterialDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = String.Empty;
        public string Alias { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string Color { get; set; } = String.Empty;
        public StockSettingDto? StockSetting { get; set; }
    }
}
