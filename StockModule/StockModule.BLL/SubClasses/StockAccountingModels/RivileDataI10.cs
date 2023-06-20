using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StockModule.BLL.SubClasses.RivileModels
{
    public class RivileDataI10
    {
        /// <summary>
        /// I10_KODAS_VD C(12)   Operacijos numeris  Privalomas, kai oper = U, D
        /// </summary>
        public string? I10_KODAS_V { get; set; }
        /// <summary>
        /// I10_EIL_NR N(6)    Detalios eilutės numeris Privalomas, kai oper=U,D
        /// </summary>
        public int? I10_EIL_NR { get; set; }
        /// <summary>
        /// I10_KODAS_TR    C(12)   Transporto operacijos numeris
        /// </summary>
        public string? I10_KODAS_TR { get; set; }
        /// <summary>
        /// I10_TIPAS   N(1)    Eilutės tipas:1-prekė, 2 kodas Privalomas, kai oper=I
        /// </summary>
        public int? I10_TIPAS { get; set; }
        /// <summary>
        /// I10_PERKELTA    N(1)    Perkėlimas:1-neperkelta,2-perkelta,3-perkelta pirma dal
        /// </summary>
        public int? I10_PERKELTA { get; set; }
        /// <summary>
        /// I10_KODAS_PS    C(12)   Prekės kodas    Privalomas***
        /// </summary>
        public string? I10_KODAS_PS { get; set; }
        /// <summary>
        /// I10_KODAS_OS1   C(12)   Padalinio siuntėjo prekės objekto kodas Arba rinkinio kodas, jei dirbama su rinkiniais
        /// </summary>
        public string? I10_KODAS_OS1 { get; set; }
        /// <summary>
        /// I10_SERIJA1 C(12)   Padalinio siuntėjo prekės serija
        /// </summary>
        public string? I10_SERIJA1 { get; set; }
        /// <summary>
        /// I10_KODAS_OS2 C(12)   Padalinio gavėjo prekės objekto kodas Arba rinkinio kodas, jei dirbama su rinkiniais
        /// </summary>
        public string? I10_KODAS_OS2 { get; set; }
        /// <summary>
        /// I10_SERIJA2 C(12)   Padalinio gavėjo prekės serijos
        /// </summary>
        public string? I10_SERIJA2 { get; set; }
        /// <summary>
        /// I10_PAV C(40)   Prekės pavadinimas
        /// </summary>
        public string? I10_PAV { get; set; }
        /// <summary>
        /// I10_KODAS_US1 C(12)   Perduodamas matavimo kodas
        /// </summary>
        public string? I10_KODAS_US1 { get; set; }
        /// <summary>
        /// I10_KIEKIS1 N(14)   Perduodamas kiekis
        /// </summary>
        public double? I10_KIEKIS1 { get; set; }
        /// <summary>
        /// I10_FRAKCIJA1 N(4)    Perduodamo matavimo vieneto frakcija
        /// </summary>
        public int? I10_FRAKCIJA1 { get; set; }
        /// <summary>
        /// I10_KODAS_US C(12)   Pagrindinio matavimo vieneto kodas
        /// </summary>
        public string? I10_KODAS_US { get; set; }
        /// <summary>
        /// I10_KIEKIS N(14)   Kiekis pagrindiniu matu
        /// </summary>
        public double? I10_KIEKIS { get; set; }
        /// <summary>
        /// I10_FRAKCIJA    N(4)    Pagrindinio matavimo frakcija
        /// </summary>
        public int? I10_FRAKCIJA { get; set; }
        /// <summary>
        /// I10_KODAS_US2   C(12)   Gavimo matavimo kodas
        /// </summary>
        public string? I10_KODAS_US2 { get; set; }
        /// <summary>
        /// I10_KIEKIS2 N(14)   Gaunamas kiekis
        /// </summary>
        public double? I10_KIEKIS2 { get; set; }
        /// <summary>
        /// I10_FRAKCIJA2 N(4)    Gavimo matavimo vieneto frakcija
        /// </summary>
        public int? I10_FRAKCIJA2 { get; set; }
        /// <summary>
        /// I10_PIR_KAINA N(12,4) Pirkimo kaina
        /// </summary>
        public double? I10_PIR_KAINA { get; set; }
        /// <summary>
        /// I10_PARD_KAINA1 N(12,4) Siuntėjo pardavimo kaina
        /// </summary>
        public double? I10_PARD_KAINA1 { get; set; }
        /// <summary>
        /// I10_PARD_KAINA2 N(12,4) Gavėjo pardavimo kaina
        /// </summary>
        public double? I10_PARD_KAINA2 { get; set; }
        /// <summary>
        /// I10_KITOS   N(12,2) Kitos išlaidos
        /// </summary>
        public double? I10_KITOS { get; set; }
        /// <summary>
        /// I10_MUITAS N(12,2) Muitas
        /// </summary>
        public double? I10_MUITAS { get; set; }
        /// <summary>
        /// I10_AKCIZAS N(12,2) Akcizas
        /// </summary>
        public double? I10_AKCIZAS { get; set; }
        /// <summary>
        /// I10_SAV_VISO    N(12,2) Prekės savikaina
        /// </summary>
        public double? I10_SAV_VISO { get; set; }
        /// <summary>
        /// I10_GAL_DATA T(8)    Galiojimo data
        /// </summary>
        public DateTime? I10_GAL_DATA { get; set; }
        /// <summary>
        /// I10_USERIS C(12)   Kas koregavo
        /// </summary>
        public string? I10_USERIS { get; set; }
        /// <summary>
        /// I10_R_DATE T(8)    Koregavimo laikas
        /// </summary>
        public DateTime? I10_R_DATE { get; set; }
        //I10_ADDUSR C(12)   Kas sukūrė
        public string? I10_ADDUSR { get; set; }
        /// <summary>
        /// I10_ADD_DATE T(8)    Kada sukūrė
        /// </summary>
        public DateTime? I10_ADD_DATE { get; set; }
        /// <summary>
        /// I10_APRASYMAS1 C(150)  Aprašymas 1	
        /// </summary>
        public string? I10_APRASYMAS1 { get; set; }
        /// <summary>
        /// I10_APRASYMAS2 C(150)  Aprašymas 2	
        /// </summary>
        public string? I10_APRASYMAS2 { get; set; }
        /// <summary>
        /// I10_APRASYMAS3 C(150)  Aprašymas 3	
        /// </summary>
        public string? I10_APRASYMAS3 { get; set; }
        /// <summary>
        /// kiekis_u N(12,4) Alternatyvus kiekis dešimtainėje išraiškoje	***
        /// </summary>
        public double? kiekis_u { get; set; }
        /// <summary>
        /// bar_kodas C(12)   Barkodas***
        /// </summary>
        public string? bar_kodas { get; set; }
        /// <summary>
        /// obj_ser_order       Likučių nurašymo tvarka.Sąlygą galima formuoti nurodant tik dvi kolonėles i17_kodas_os ir / arba i17_serija, pavyzdžiui, "i17_kodas_os, i17_serija", "i17_kodas_os desc, i17_serija asc", "i17_serija desc, i17_kodas_os asc"	***
        /// </summary>
        public string? obj_ser_order { get; set; }
        /// <summary>
        /// I49_DIM01, ... , I49_DIM15 C(12)   Dimensijos
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


        /// <summary>
        /// K45_RIN01, ... , K45_RIN15  C(12)   Padalinio siuntėjo rinkiniai
        /// </summary>
        public string? K45_RIN01 { get; set; }
        public string? K45_RIN02 { get; set; }
        public string? K45_RIN03 { get; set; }
        public string? K45_RIN04 { get; set; }
        public string? K45_RIN05 { get; set; }
        public string? K45_RIN06 { get; set; }
        public string? K45_RIN07 { get; set; }
        public string? K45_RIN08 { get; set; }
        public string? K45_RIN09 { get; set; }
        public string? K45_RIN10 { get; set; }
        public string? K45_RIN11 { get; set; }
        public string? K45_RIN12 { get; set; }
        public string? K45_RIN13 { get; set; }
        public string? K45_RIN14 { get; set; }
        public string? K45_RIN15 { get; set; }
        /// <summary>
        /// K45_2_RIN01, ... , K45_2_RIN15  C(12   	Padalinio gavėjo rinkiniai
        /// </summary>
        public string? K45_2_RIN01 { get; set; }
        public string? K45_2_RIN02 { get; set; }
        public string? K45_2_RIN03 { get; set; }
        public string? K45_2_RIN04 { get; set; }
        public string? K45_2_RIN05 { get; set; }
        public string? K45_2_RIN06 { get; set; }
        public string? K45_2_RIN07 { get; set; }
        public string? K45_2_RIN08 { get; set; }
        public string? K45_2_RIN09 { get; set; }
        public string? K45_2_RIN10 { get; set; }
        public string? K45_2_RIN11 { get; set; }
        public string? K45_2_RIN12 { get; set; }
        public string? K45_2_RIN13 { get; set; }
        public string? K45_2_RIN14 { get; set; }
        public string? K45_2_RIN15 { get; set; }
    }
}
