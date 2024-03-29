﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowSeller.Domain
{
    [JsonObject(IsReference = true)]
    public partial class SubElement
    {
        public long SubElementId { get; set; }
        public int Element { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public string Type { get; set; }

        public Window Window { get; set; }
        public long WindowId { get; set; }


        [JsonIgnore]
        public ICollection<Glass>? Glasses { get; set; }
    }
}
