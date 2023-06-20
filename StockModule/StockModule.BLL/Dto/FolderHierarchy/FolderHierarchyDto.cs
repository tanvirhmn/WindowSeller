using StockModule.BLL.StockSettings;
using StockModule.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.BLL.Dto.FolderHierarchy
{
    public class FolderHierarchyDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public string Icon { get; set; } = string.Empty;
        public int HierarchyType { get; set; }

        public List<FolderHierarchyDto>? Children { get; set; }

    }
}
