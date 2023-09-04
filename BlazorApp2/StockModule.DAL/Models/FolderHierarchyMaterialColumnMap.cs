using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.DAL.Models
{
    [JsonObject(IsReference = true)]
    public class FolderHierarchyMaterialColumnMap
    {
        public int Id { get; set; }
        public bool IsRequired { get; set; }
        public bool IsVisible { get; set; }

        public int FolderHierarchyId { get; set; }
        [ForeignKey("FolderHierarchyId")]
        public FolderHierarchy FolderHierarchy { get; set; } = null!;

        public int MaterialColumnId { get; set; }
        [ForeignKey("MaterialColumnId")]
        public MaterialColumn? MaterialColumn { get; set; }
    }
}
