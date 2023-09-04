using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.DAL.Models
{
    [JsonObject(IsReference = true)]
    public class MaterialColumn
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Block { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public bool IsLocked { get; set; }





        public int? MaterialTypeEnumMasterId { get; set; }
        public MaterialTypeEnumMaster? MaterialTypeEnumMaster { get; set; }
        public List<MaterialColumnValue>? MaterialColumnValues { get; set; }
    }
}
