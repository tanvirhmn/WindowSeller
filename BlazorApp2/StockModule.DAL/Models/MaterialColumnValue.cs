using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.DAL.Models
{
    public class MaterialColumnValue
    {
        public int Id { get; set; }
        public int RowNo { get; set; }
        public string Value { get; set; } = String.Empty;

        public int MaterialColumnId { get; set; }
        public MaterialColumn? MaterialColumn { get; set; }
    }
}
