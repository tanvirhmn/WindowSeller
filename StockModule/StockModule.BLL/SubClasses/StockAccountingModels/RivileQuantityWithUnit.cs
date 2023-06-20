using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.BLL.SubClasses.RivileModels
{
    public class RivileQuantityWithUnit
    {
        public double Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;

        public int Fraction
        {
            get
            {
                return Unit switch
                {
                    "KG" => 1000,
                    "KOMPL." => 1,
                    "L" => 1000,
                    "M" => 100,
                    "M*2" => 100,
                    "M*3" => 1000,
                    "M2" => 100,
                    "PCS" => 1,
                    "RUL" => 1,
                    "T" => 1000,
                    "VAL" => 60,
                    "VNT" => 1,
                    _ => 1
                };
            }
        }
        public double QuantityF
        {
            get
            {  
                 return Math.Round(Fraction * Quantity, 0, MidpointRounding.AwayFromZero);
            }
        }

    }
}
