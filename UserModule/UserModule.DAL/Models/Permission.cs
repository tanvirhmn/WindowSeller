using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModule.DAL.Models
{
    [JsonObject(IsReference = true)]
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string MenuCode { get; set; } = String.Empty;
        public string AzureGroup { get; set; } = String.Empty;

    }
}
