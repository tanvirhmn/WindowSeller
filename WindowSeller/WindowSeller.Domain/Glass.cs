using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowSeller.Domain
{
    [JsonObject(IsReference = true)]
    public class Glass
    {
        public long GlassId { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }

        public SubElement SubElement { get; set; }
        public long SubElementId { get; set; }
    }
}
