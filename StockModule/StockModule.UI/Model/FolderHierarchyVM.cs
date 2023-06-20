using StockModule.UI.Data;
using System.ComponentModel.DataAnnotations;

namespace StockModule.UI.Model
{
    public class FolderHierarchyVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public string Icon { get; set; } = string.Empty;
        public int HierarchyType { get; set; }
        public List<FolderHierarchyVM>? Children { get; set; }

    }
}
