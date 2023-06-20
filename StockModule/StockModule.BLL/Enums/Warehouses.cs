using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.BLL
{
    public enum Warehouses
    {
        NoWarehouse = 0,
        Material = 1,
        Production = 2,
        ReservedRemnants = 3,
        FreeRemnants = 4,
        Shipping = 5,
        Quality = 6,
        Purchase = 7
    }
}
