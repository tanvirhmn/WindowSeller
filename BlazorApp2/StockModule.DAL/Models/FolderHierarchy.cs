using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.DAL.Models
{
    public partial class FolderHierarchy
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public string Icon { get; set; } = string.Empty;

        [ForeignKey("ParentId")]
        public FolderHierarchy? ParentFolder { get; set; } = null;
    }
}
