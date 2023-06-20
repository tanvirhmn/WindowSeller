using Radzen;
using Radzen.Blazor;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Xml.Linq;

namespace StockModule.UI.Model
{ 

    public class StockAccountingVM
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string MaterialCode { get; set; } = String.Empty;
        public string MaterialDescription { get; set; } = String.Empty;
        public string Movement { get; set; } = String.Empty;
        public string Type { get; set; } = String.Empty;
        public string Measure { get; set; } = String.Empty;
        public double Quantity { get; set; }
        public int Status { get; set; } = 0;
        public string? LastResponseMessage { get; set; }
        public EnumStatus StatusEnum
        {
            get
            {
                return (EnumStatus)Status;
            }
        }                  
        public ButtonStyle StatusButtonStyle
        {
            get
            {
                return StatusEnum switch
                {
                    EnumStatus.Mismatch => ButtonStyle.Danger,
                    EnumStatus.Error=> ButtonStyle.Danger,                   
                    EnumStatus.NotStarted => ButtonStyle.Light,
                    EnumStatus.Completed => ButtonStyle.Success,
                    EnumStatus.Canceled=> ButtonStyle.Warning,
                    EnumStatus.Auto => ButtonStyle.Success,
                    _ => ButtonStyle.Success
                };
            }
        }


    }

    public enum EnumStatus
    {
        [Display(Description = "MISMATCH")]
        Mismatch = -2,
        [Display(Description = "ERROR")]
        Error = -1,
        [Display(Description = "NOT STARTED")]
        NotStarted = 0,
        [Display(Description = "COMPLETED")]
        Completed = 1,
        [Display(Description = "CANCELED")]
        Canceled = 2,
        [Display(Description = "AUTO")]
        Auto = 3
    }

}