using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StockModule.BLL.SubClasses.RivileModels
{
       

    public class RivileRequest
    {
        [JsonProperty(PropertyName = "method")]
        public string Method { get; set; } = "";

        [JsonProperty(PropertyName = "params")]
        public RivileParams? Params { get; set; }

        [JsonProperty(PropertyName = "data")]
        public RivileData? Data { get; set; }

    }

}
