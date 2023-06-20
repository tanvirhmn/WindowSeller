using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StockModule.BLL.SubClasses.RivileModels
{


    public class RivileResponse
    {
        public RivileReTDok? retDoK { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public RivileResponse()
        {
            IsSuccess = false;
        }
    }


    [XmlRoot("RET_DOK")]
    public class RivileReTDok
    {

        [XmlElement("id")]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } = string.Empty;

        [XmlElement("durationMs")]
        [JsonProperty(PropertyName = "durationMs")]
        public string DurationMs { get; set; } = string.Empty;

        [XmlElement("errorMessage")]
        [JsonProperty(PropertyName = "errorMessage")]
        public string ErrorMessage { get; set; } = string.Empty;

        [XmlElement("I09")]
        [JsonProperty(PropertyName = "I09")]
        public RivileDataI09? I09 { get; set; }

        [XmlElement("N17")]
        [JsonProperty(PropertyName = "N17")]
        public RivileDataN17? N17 { get; set; }

        [XmlElement("errors")]
        [JsonProperty(PropertyName = "errors")]
        public List<RivileErrors> Errors { get; set; } = new List<RivileErrors> ();
    }





}
