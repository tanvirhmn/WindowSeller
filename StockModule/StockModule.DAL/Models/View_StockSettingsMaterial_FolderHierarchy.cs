using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.DAL.Models
{
    public class View_StockSettingsMaterial_FolderHierarchy
    {
        public int Id { get; set; }
        public string Code { get; set; } = String.Empty;
        public string Alias { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string Color { get; set; } = String.Empty;
        public string Type { get; set; } = String.Empty;
        public Single BarLength { get; set; } = 0;
        public int? StockSettingsId { get; set; }
        public int? FolderHierarchyId { get; set; } = null;
    }
}
