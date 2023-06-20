using Newtonsoft.Json;
using StockModule.BLL.Dto;
using StockModule.BLL.SubClasses;
using StockModule.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StockModule.BLL.Helpers
{
    public static class StockMovementReasonHelper
    {

        public static StockMovementReasons ReasonEnum(this StockMovementReason stockMovementReason)
        {
            if(!Enum.TryParse(stockMovementReason.Name, true, out StockMovementReasons r)) {
                r = StockMovementReasons.NoReason;
            }
            return r;          
        }

        public static bool IsReasonEnum(this StockMovementReason stockMovementReason, StockMovementReasons stockMovementReasons)
        {
            if (stockMovementReason == null) return false;
            return (stockMovementReason.ReasonEnum() == stockMovementReasons);
        }

        public static string GetReasonDTO(this StockMovementReason stockMovementReason)
        {
           switch (stockMovementReason.ReasonEnum())
            {
                case StockMovementReasons.SpoilageMaterialToQuality:
                    return "Warehouse";
                case StockMovementReasons.ProdSpoilageMaterialToQuality:
                    return "Production";
                case StockMovementReasons.IguSpoilageSupplier:
                    return "IGU";
                case StockMovementReasons.IguSpoilageCancelSupplier:
                    return "IGU";
                default:
                    return "N/A";
            }
        }         
          

        public static string GetMovementDTO(this StockMovementReason stockMovementReason)
        {
            return string.Format("{0}->{1}", stockMovementReason!.FromWarehouse!.Name.Substring(0, 1), stockMovementReason!.ToWarehouse!.Name.Substring(0, 1));
        }

        public static double GetQuantityDTO(this StockMovement stockMovement)
        {
            var quantity = stockMovement.Quantity;
            var length = (stockMovement.ToStock!.Length != 0.0) ? stockMovement.ToStock!.Length / 1000.0 : 1.0;
            var height = (stockMovement.ToStock!.Height != 0.0) ? stockMovement.ToStock!.Height / 1000.0 : 1.0;
            return (quantity * length * height);
        }

    }
}

