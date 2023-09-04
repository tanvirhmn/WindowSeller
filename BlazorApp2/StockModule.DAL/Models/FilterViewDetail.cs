using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.DAL.Models
{
    public partial class FilterViewDetail
    {
        public int Id { get; set; }
        public string Property { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public string FilterValue { get; set; } = string.Empty;
        public string FilterOperator { get; set; } = string.Empty;
        public string LogicalFilterOperator { get; set; } = string.Empty;
        
        public int FilterViewMasterID { get; set; }
        [ForeignKey("FilterViewMasterID")]
        public FilterViewMaster? FilterViewMaster { get; set; }
        

        [ForeignKey("ParentId")]
        public FilterViewDetail? ParentFilterViewDetail { get; set; } = null;

        public ICollection<FilterViewDetail>? ChildFilterViewDetails { get; set; }
    }
}
