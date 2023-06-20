using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StockModule.BLL.SubClasses.RivileModels
{
    public class RivileDataN17
    {

        public int? Row_number { get; set; }
        /// <summary>
        ///N17_KODAS_PS C(12)   Prekės kodas  Privalomas
        /// </summary>
        public string? N17_KODAS_PS { get; set; }
        /// <summary>
        ///N17_TIPAS N(1)    Tipas:1-prekė,2-paslauga Privalomas
        /// </summary>
        public int? N17_TIPAS { get; set; }

        /// <summary>
        /// N17_KODAS_US  C(12)  Pagrindinis matavimo vieneto kodas Privalomas
        /// </summary>
        public string? N17_KODAS_US { get; set; }

        /// <summary>
        /// N17_KODAS_DS  C(12) Sąskaitos ryšio kodas Privalomas
        /// </summary>
        public string? N17_KODAS_DS { get; set; }


    }
}
