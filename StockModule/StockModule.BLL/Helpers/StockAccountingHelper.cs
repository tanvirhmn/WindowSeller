using Newtonsoft.Json;
using StockModule.BLL.Dto;
using StockModule.BLL.SubClasses.RivileModels;
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
    public static class StockAccountingHelper
    {

        public static RivileRequest GetRivileRequest(this StockAccountingMovement stockAccountingMovement, EnumRivileMethod enumRivileMethod, string docNo, string responseCode = "", RivileDataN17? dataN17 = null )
        {
            var r = new RivileRequest() { Method = enumRivileMethod.ToString() };
            switch (enumRivileMethod)
            {
                case EnumRivileMethod.EDIT_I09_FULL:
                    {
                        var code = stockAccountingMovement.StockMovement!.ToStock!.Material.Code;                                        
                        var date = stockAccountingMovement.StockMovement!.InsertDate.ToString("yyyy-MM-dd HH:mm:ss");
                        var fromWarehouse = MovementToRivile(stockAccountingMovement.StockMovement!.StockMovementReason!.FromWarehouseId);
                        var ToWarehouse = MovementToRivile(stockAccountingMovement.StockMovement!.StockMovementReason!.ToWarehouseId);                     
                        var rivileQuantity = stockAccountingMovement.StockMovement!.GetRivileQuantityWithUnit();
                        if (dataN17 != null && !string.IsNullOrEmpty(dataN17.N17_KODAS_US))
                        {
                            rivileQuantity.Unit = dataN17.N17_KODAS_US;
                        }


                            var rivileDataI09 = new RivileDataI09()
                        {
                            I09_TIPAS = 1,
                            I09_DOK_NR = docNo,
                            I09_IS_DATA = date,
                            I09_GAV_DATA = date,
                            I09_KODAS_IS1 = fromWarehouse,
                            I09_KODAS_IS2 = ToWarehouse,
                            I10 = new RivileDataI10()
                            {
                                I10_TIPAS = 1,//p 1-preke
                                I10_KODAS_PS = code,
                                I10_FRAKCIJA1 = rivileQuantity.Fraction,
                                I10_KIEKIS1 = rivileQuantity.QuantityF,                        
                                I10_KODAS_US1 = rivileQuantity.Unit,
                                I10_FRAKCIJA= rivileQuantity.Fraction,
                                I10_KIEKIS = rivileQuantity.QuantityF,
                                I10_KODAS_US = rivileQuantity.Unit,
                                I10_FRAKCIJA2 = rivileQuantity.Fraction,
                                I10_KIEKIS2 = rivileQuantity.QuantityF,
                                I10_KODAS_US2 = rivileQuantity.Unit                               
                            }
                        };
                        r.Data = new RivileData(rivileDataI09);
                    }
                    break;

                case EnumRivileMethod.EDIT_I09:
                    {         
                        r.Params = new RivileParams() { Oper="P"};

                        var rivileDataI09 = new RivileDataI09()
                        {
                            I09_KODAS_VD = responseCode,                                 
                        };
                        r.Data = new RivileData(rivileDataI09);
                    }
                    break;


                case EnumRivileMethod.GET_N17_LIST:
                    {
                        r.Params = new RivileParams() { 
                            List="H",
                            Filter= $"n17_kodas_ps='{stockAccountingMovement.StockMovement!.ToStock!.Material.Code}'"
                        };
                      
                    }
                    break;




                default:
                    break;
            }

            return r;
        }
        private static string MovementToRivile(int? warehouseId)
        {
            return warehouseId switch
            {
                1 => "G",
                6 => "CL",
                _ => ""
            };
        }

        public static string ToJsonString(this RivileRequest rivileRequest)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            return  JsonConvert.SerializeObject(rivileRequest, jsonSettings);          
        }

        public static RivileReTDok? ResponseToObject(this StockAccountingAction stockAccountingAction)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            return JsonConvert.DeserializeObject<RivileReTDok?>(stockAccountingAction.Response, jsonSettings);

        }
         

        public static bool IsValidUnit(this StockMovement stockMovement, string? unit)
        {
            var defaultUnit = stockMovement.GetUnitDTO();
            if(defaultUnit.Equals(unit))
            {
                return true;
            } else if(defaultUnit == "PCS" && unit =="VNT")
            {
                return true;
            }
            else if (defaultUnit == "M2" && unit == "M*2")
            {
                return true;
            }

            return false;
        }

        public static string GetUnitDTO(this StockMovement stockMovement)
        { 
             return (stockMovement.ToStock!.Height != 0.0) ? "M2" : (stockMovement.ToStock!.Length != 0.0) ? "M" : "PCS";                      
        }


        public static RivileQuantityWithUnit GetRivileQuantityWithUnit(this StockMovement stockMovement)
        {
            return new RivileQuantityWithUnit() { Quantity= stockMovement.GetQuantityDTO(), Unit= stockMovement.GetUnitDTO() };          
            
        }       

   
        public static string GetLasMessageDTO(this IEnumerable<StockAccountingAction> StockAccountingActions)
        {
            if(StockAccountingActions.Count() == 0) return string.Empty;
            var lastAction = StockAccountingActions.Last();
            if (lastAction == null) return string.Empty;
            if (!lastAction.IsSuccess)
            {
                try
                {

                    var obj = JsonConvert.DeserializeObject<RivileReTDok>(lastAction.Response);
                   
                    if(obj !=null && obj.Errors.Any())
                    {                      
                      var msg =  obj.Errors.Where(_ => _.DataErrors.Any()).FirstOrDefault()?.DataErrors?.FirstOrDefault()?.Message;
                        if(msg != null)
                        {
                            return string.Format("{0}: {1}", obj!.ErrorMessage , msg);
                        }                        
                    }
                    return obj!.ErrorMessage;

                }
                catch (Exception ex)
                {
                    var e = ex.Message;

                    return lastAction.Response;
                }
              
            }

            return string.Empty;
            

        }      

    }
}

