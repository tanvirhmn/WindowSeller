﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowSellerWASM.BLL.DTOs
{
    public class OrderDto
    {
        public long OrderId { get; set; }
        public string OrderName { get; set; }
        public string State { get; set; }
    }
}
