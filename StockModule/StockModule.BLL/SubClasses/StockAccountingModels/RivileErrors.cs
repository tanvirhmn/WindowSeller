using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.BLL.SubClasses.RivileModels
{
    public class RivileErrors
    {


        [JsonProperty(PropertyName = "data")]
        public string? Data { get; set; } = null;
        
        [JsonProperty(PropertyName = "dataErrors")]
        public List<RivileDataErrors> DataErrors { get; set; }= new List<RivileDataErrors>();

    }

    public class RivileDataErrors
    {

        [JsonProperty(PropertyName = "tag")]
        public string? Tag { get; set; }

        [JsonProperty(PropertyName = "code")]
        public string? Code { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string? Message { get; set; }

    }
}
