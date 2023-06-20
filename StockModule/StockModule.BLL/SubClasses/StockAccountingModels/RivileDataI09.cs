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
    public class RivileDataI09
    {

        public int? Row_number { get; set; }
        /// <summary>
        /// I09_KODAS_VD C(12)   Operacijos numeris  Privalomas(kai oper = U, D, P)
        /// </summary>
        public string? I09_KODAS_VD { get; set; }
        /// <summary>
        ///I09_TIPAS N(1)    Dokumento tipas:1-važtaraštis,2-užsakymas Privalomas, kai oper=I
        /// </summary>

        public int? I09_TIPAS { get; set; }
        /// <summary>
        /// I09_DOK_NR  C(12)   Dokumento numeris   Privalomas, kai oper = I
        /// </summary>
        public string? I09_DOK_NR { get; set; }
        /// <summary>
        /// I09_IS_DATA T(8)    Išvežimo data   Privalomas, kai oper = I
        /// </summary>
        public string? I09_IS_DATA { get; set; }
        /// <summary>
        /// I09_GAV_POZ N(1)    Rezervas
        /// </summary>
        public int? I09_GAV_POZ { get; set; }
        /// <summary>
        /// I09_GAV_DATA    T(8)    Prekių gavimo data Privalomas, kai oper=I
        /// </summary>
        public string? I09_GAV_DATA { get; set; }
        /// <summary>
        /// I09_KODAS_IS1   C(12)   Padalinio kodas iš kurio veža Privalomas, kai oper=I
        /// </summary>
        public string? I09_KODAS_IS1 { get; set; }
        /// <summary>
        /// I09_KODAS_SS_T  C(12)   Analitinės operacijos numeris
        /// </summary>
        public string? I09_KODAS_SS_T { get; set; }
        /// <summary>
        /// I09_NUTOL1  N(1)    Padalinys iš kurio veža nutolęs?:0-ne,1-taip
        /// </summary>
        public int? I09_NUTOL1 { get; set; }
        /// <summary>
        /// I09_EIL1    C(40)   Padalinio aprašymo 1 eilutė
        /// </summary>
        public string? I09_EIL1 { get; set; }
        /// <summary>
        /// I09_EIL2    C(40)   Padalinio aprašymo 2 eilutė
        /// </summary>
        public string? I09_EIL2 { get; set; }
        /// <summary>
        /// I09_EIL3    C(40)   Padalinio aprašymo 3 eilutė
        /// </summary>
        public string? I09_EIL3 { get; set; }
        /// <summary>
        /// I09_KODAS_IS2   C(12)   Padalinio gavėjo kodas Privalomas, kai oper=I
        /// </summary>
        public string? I09_KODAS_IS2 { get; set; }
        /// <summary>
        /// I09_NUTOL2  N(1)    Ar padalinys gavėjas nutolęs?:0-ne,1-taip
        /// </summary>
        public int? I09_NUTOL2 { get; set; }
        /// <summary>
        /// I09_A_EIL1  C(40)   Padalinio gavėjo aprašymo 1 eilutė
        /// </summary>
        public string? I09_A_EIL1 { get; set; }
        /// <summary>
        /// I09_A_EIL2  C(40)   Padalinio gavėjo aprašymo 2 eilutė
        /// </summary>
        public string? I09_A_EIL2 { get; set; }
        /// <summary>
        /// I09_A_EIL3  C(40)   Padalinio gavėjo aprašymo 3 eilutė
        /// </summary>
        public string? I09_A_EIL3 { get; set; }
        /// <summary>
        /// I09_PERKELTA1   N(1)    Pirmos dalies perkėlimas:1-neperkelta,2-perkelta,3-kore
        /// </summary>
        public int? I09_PERKELTA1 { get; set; }
        /// <summary>
        /// I09_PERKELTA2   N(1)    Antros dalies perkėlimas:1-neperkelta,2-perkelta,3-kore
        /// </summary>
        public int? I09_PERKELTA2 { get; set; }
        /// <summary>
        /// I09_IMP_EXP N(1)    Rezervas
        /// </summary>
        public int? I09_IMP_EXP { get; set; }
        /// <summary>
        /// I09_USERIS  C(12)   Kas koregavo
        /// </summary>
        public string? I09_USERIS { get; set; }
        /// <summary>
        /// I09_R_DATE T(8)    Koregavimo Laikas
        /// </summary>
        public DateTime? I09_R_DATE { get; set; }
        /// <summary>
        /// I09_ADDUSR C(12)   Kas sukūrė
        /// </summary>
        public string? I09_ADDUSR { get; set; }
        /// <summary>
        /// I09_EIL_SK N(12,2) Eilučių skaičius
        /// </summary>
        public decimal? I09_EIL_SK { get; set; }
        /// <summary>
        /// I09_KODAS_SM1 C(12)   Asmuo
        /// </summary>
        public string? I09_KODAS_SM1 { get; set; }
        /// <summary>
        /// I09_KODAS_SM2   C(12)   Asmuo 2	
        /// </summary>
        public string? I09_KODAS_SM2 { get; set; }
        /// <summary>
        /// I09_PAV C(60)   Aprašymas
        /// </summary>
        public string? I09_PAV { get; set; }
        /// <summary>
        /// I09_KODAS_MS    C(12)   Menedžerio kodas
        /// </summary>
        public string? I09_KODAS_MS { get; set; }
        /// <summary>
        /// I09_KODAS_LS_1 C(12)   Logistika 1	
        /// </summary>
        public string? I09_KODAS_LS_1 { get; set; }
        /// <summary>
        /// I09_KODAS_LS_2 C(12)   Logistika 2
        /// </summary>
        public string? I09_KODAS_LS_2 { get; set; }
        /// <summary>
        /// I09_KODAS_LS_3 C(12)   Logistika 3
        /// </summary>
        public string? I09_KODAS_LS_3 { get; set; }

        /// <summary>
        /// I09_KODAS_LS_4 C(12)   Logistika 4
        /// </summary>
        public string? I09_KODAS_LS_4 { get; set; }

        /// <summary>
        /// I09_ADD_DATE T(8)    Kada sukūrė
        /// </summary>
        public DateTime? I09_ADD_DATE { get; set; }
        /// <summary>
        /// I09_PER1_DATE T(8)    Kada koreguotas pirmas perkėlimas
        /// </summary>
        public DateTime? I09_PER1_DATE { get; set; }
        /// <summary>
        /// I09_PER1_USER C(12)   Kas koregavo pirmą perkėlimą
        /// </summary>
        public string? I09_PER1_USER { get; set; }
        /// <summary>
        /// I09_KODAS_AU C(12)   Automobilio kodas
        /// </summary>
        public string? I09_KODAS_AU { get; set; }
        /// <summary>
        /// I09_KODAS_ZN C(12)   Zona
        /// </summary>
        public string? I09_KODAS_ZN { get; set; }

        /// <summary>
        /// I09_KODAS_MS2   C(12)   Menedžeris 2	
        /// </summary>
        public string? I09_KODAS_MS2 { get; set; }
        /// <summary>
        /// I09_BUSENA N(3)    Būsena
        /// </summary>
        public int? I09_BUSENA { get; set; }
        /// <summary>
        /// I49_DIM01, ... , I49_DIM15  C(12)   Dimensijos
        /// </summary>
        public string? I49_DIM01 { get; set; }
        public string? I49_DIM02 { get; set; }
        public string? I49_DIM03 { get; set; }
        public string? I49_DIM04 { get; set; }
        public string? I49_DIM05 { get; set; }
        public string? I49_DIM06 { get; set; }
        public string? I49_DIM07 { get; set; }
        public string? I49_DIM08 { get; set; }
        public string? I49_DIM09 { get; set; }
        public string? I49_DIM10 { get; set; }
        public string? I49_DIM11 { get; set; }
        public string? I49_DIM12 { get; set; }
        public string? I49_DIM13 { get; set; }
        public string? I49_DIM14 { get; set; }
        public string? I49_DIM15 { get; set; }

        public RivileDataI10? I10 { get; set; }

    }
}
