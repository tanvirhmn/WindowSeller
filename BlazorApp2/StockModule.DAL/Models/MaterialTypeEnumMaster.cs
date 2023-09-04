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
    public class MaterialTypeEnumMaster
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public MaterialColumn? MaterialColumn { get; set; }
        [JsonIgnore]
        public ICollection<MaterialTypeEnumDetail>? MaterialTypeEnumDetails { get; set; }
    }
}
