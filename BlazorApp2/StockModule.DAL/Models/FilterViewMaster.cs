using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StockModule.DAL.Models
{
    public partial class FilterViewMaster
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string azureUserID { get; set; } = string.Empty;

        public ICollection<FilterViewDetail>? FilterViewDetails { get; set; }
    }
}
