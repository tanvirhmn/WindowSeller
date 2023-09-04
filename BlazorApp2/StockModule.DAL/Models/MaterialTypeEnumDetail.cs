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
    public class MaterialTypeEnumDetail
    {
        public int Id { get; set; }
        public int Key { get; set; } = 0;
        public string Value { get; set; } = String.Empty;

        public int MaterialTypeEnumMasterId { get; set; }
        public MaterialTypeEnumMaster? MaterialTypeEnumMaster { get; set; }
    }
}
