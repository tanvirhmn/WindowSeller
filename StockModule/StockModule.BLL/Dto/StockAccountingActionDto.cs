using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.BLL.Dto
{

    //create clas by json: {"Map":{"timestamp":"2020-02-05T12:34:26.890+0000","status":"500",	"error":"Internal Server Error",	"message":"No message available","path":"/v2"}}
    public class Map
    {
        [JsonProperty(PropertyName = "timestamp")]
        public string Timestamp { get; set; }= String.Empty;

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }= String.Empty;

        [JsonProperty(PropertyName = "error")]
        public string Error { get; set; } = String.Empty;

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; } = String.Empty;

        [JsonProperty(PropertyName = "path")]
        public string Path { get; set; } = String.Empty;
    }
    public class StockAccountingActionResponseDto
    {
        public StockAccountingActionResponseDto()
        {
            Map = new Map();
        }
        public Map Map { get; set; }
    }


    


}
