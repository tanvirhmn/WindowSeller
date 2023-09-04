using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockModule.DAL.Models
{
    public partial class StockSetting
    {
        public int Id { get; set; }
        public int MaterialId { get; set; }
        public string CollectionType { get; set; } = "Warehouse";
        public bool? Reproducible { get; set; } = true;
        public int? FolderHierarchyId { get; set; } = null;

        [ForeignKey("MaterialId")]
        public virtual Material Material { get; set; } = null!;

        [ForeignKey("FolderHierarchyId")]
        public FolderHierarchy FolderHierarchy { get; set; } = null!;
    }
}
