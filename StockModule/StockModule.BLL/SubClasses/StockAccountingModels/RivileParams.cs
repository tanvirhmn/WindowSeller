using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.BLL.SubClasses.RivileModels
{
    public class RivileParams
    {

        /// <summary>
        /// I - naujo įrašo sukūrimas
        /// U - įrašo koregavimas
        /// D - įrašo ištrynimas
        /// P - informacijos perkėlimas
        /// </summary>
        [JsonProperty(PropertyName = "oper")]
        public string Oper { get; set; } = "";
        /// <summary>
        /// ROLLBACK (default reikšmė) - Įvykus klaidai tolimesnės procedūros nebus vykdamos, bus ištrinamas dokumentas (I06) ir grąžinama klaida. Klaidos statusas 400
        ///EXIT - Įvykus klaidai tolimesnės procedūros nebus vykdamos, bus grąžinamas sukurtas dokumentas(I06) ir klaidų sąrašas.Klaidos statusas 207
        ///CONTINUE - Įvykus klaidai procedūros bus vykdamos iki pabaigos, o klaidos dedamos į sąrašą.Grąžinamas sukurtas dokumentas (I06) ir klaidų sąrašas pabaigoje.Klaidos statusas 207
        /// </summary>
        [JsonProperty(PropertyName = "errorAction")]
        public string? ErrorAction { get; set; }


        [JsonProperty(PropertyName = "list")]
        public string? List { get; set; }

        [JsonProperty(PropertyName = "fil")]
        public string? Filter { get; set; }



    }
}
