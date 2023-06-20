using AutoMapper;
using StockModule.BLL.Logic;
using StockModule.DAL.Models;
using StockModule.DAL.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;
using StockModule.BLL.StockSettings;
using StockModule.BLL.Dto;

namespace StockModule.BLL
{
    public class MaterialLogic : IMaterialLogic
    {
        private readonly ITaskService _taskService;
        private readonly IMaterialService MaterialService;
        private readonly IEmployeeService EmployeeService;
        private readonly IExternalMovementService ExternalMovementService;
        private readonly IStockService StockService;
        private readonly IStockDocumentService StockDocumentService;
        private readonly IStockMovementService StockMovementService;
        private readonly IStockMovementReasonService StockMovementReasonService;
        private readonly IWarehouseService WarehouseService;
        private readonly IStockAccountingLogic _stockAccountingLogic;
        private readonly ILogger Logger;
        private readonly IMapper _mapper;

        public MaterialLogic(ITaskService taskService,
                             IMaterialService materialService,
                             IEmployeeService employeeService,
                             IExternalMovementService externalMovementService,
                             IStockService stockService,
                             IStockDocumentService stockDocumentService,
                             IStockMovementService stockMovementService,
                             IStockMovementReasonService stockMovementReasonService,
                             IWarehouseService warehouseService,
                             IStockAccountingLogic stockAccountingLogic,
                             ILogger<MaterialLogic> logger,
                             IMapper mapper)
        {
            MaterialService = materialService;
            EmployeeService = employeeService;
            ExternalMovementService = externalMovementService;
            StockService = stockService;
            StockDocumentService = stockDocumentService;
            StockMovementService = stockMovementService;
            StockMovementReasonService = stockMovementReasonService;
            WarehouseService = warehouseService;
            _stockAccountingLogic = stockAccountingLogic;
            Logger = logger;
            _mapper = mapper;
            _taskService = taskService;
        }

        #region Reproduction
        public bool MoveReproduction(ReproductionMovement reproductionMovement)
        {
            try
            {
                var reason = StockMovementReasonService.GetByReason(reproductionMovement.Reason.ToString());
                if (reason == null)
                {
                    throw new Exception("Not Set Reason Stock Movement");
                }

                StockMovement movement = new StockMovement()
                {
                    Quantity = reproductionMovement.Quantity,
                    EmployeeId = EmployeeService.GetId(reproductionMovement.Employee),
                    ReasonId = StockMovementReasonService.GetId(reproductionMovement.Reason.ToString()),
                    Comment = reproductionMovement.Comment,
                    DocumentDate = DateTime.Now,
                    InsertDate = DateTime.Now,
                    DocumentNumber = reproductionMovement.DocumentId,
                    DocumentId = StockDocumentService.GetID(reproductionMovement.Document.ToString()),
                    FromStockId = StockService.GetId(MaterialService.GetId(reproductionMovement.MaterialCode), reproductionMovement.Length, 0.0, reason.FromWarehouseId),
                    ToStockId = StockService.GetId(MaterialService.GetId(reproductionMovement.MaterialCode), reproductionMovement.Length, 0.0, reason.ToWarehouseId)
                };

                StockMovementService.Create(movement);
                //Try create or update Accounting
                _stockAccountingLogic.TryCreateOrUpdateByStockMovement(movement.Id);
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "MoveReproduction error");
                return false;
            }
        }
        #endregion Reproduction

        #region CollectionApi

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public bool MoveStockFromCollectionApi(List<CollectionMovement> movements)
        {
            //return true;
            try
            {
                foreach (var movement in movements)
                {
                    string typeKey = movement.TypeKey;
                    int cycle = movement.Cycle;
                    string materialCode = movement.Reference;
                    double length = movement.Length;
                    double height = movement.Height;


                    StockMovement stockMovement = GetMovementWithTypeKeyRelatedIds(movement);

                    if (stockMovement.ReasonId == 0) continue;

                    stockMovement.Quantity = Math.Abs(movement.Quantity);

                    if (stockMovement.Quantity == 0) continue;

                    stockMovement.EmployeeId = EmployeeService.GetId(movement.EmployeeId);
                    stockMovement.Comment = "Stock Movement from collection terminal";
                    stockMovement.DocumentId = StockDocumentService.GetID(StockDocuments.Task.ToString());
                    stockMovement.DocumentNumber = movement.TaskId;
                    stockMovement.InsertDate = DateTime.Now;
                    stockMovement.DocumentDate = DateTime.Now;

                    StockMovementService.Create(stockMovement);
                   
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "MoveStockFromCollectionApi error");
                return false;
            }
        }

        public StockMovement GetMovementWithTypeKeyRelatedIds(CollectionMovement movement)
        {
            StockMovementReasons reason = StockMovementReasons.NoReason;
            bool stockReturn = movement.Quantity < 0 ? true : false;

            switch (movement.TypeKey)
            {
                case "CT_SMP":
                case "CT_BMP":
                    reason = stockReturn ? StockMovementReasons.ProductionToMaterial : StockMovementReasons.MaterialToProduction;
                    break;
                case "CT_SMS":
                    reason = stockReturn ? StockMovementReasons.ShippingToMaterial : StockMovementReasons.MaterialToShipping;
                    break;
                case "CT_ISP":
                    reason = stockReturn ? StockMovementReasons.ProductionToMaterial : StockMovementReasons.ScreenMaterialToProduction;
                    break;
                case "CT_ISS":
                    reason = stockReturn ? StockMovementReasons.ShippingToMaterial : StockMovementReasons.ScreenMaterialToShipping;
                    break;
                case "CT_BMS":
                    switch (movement.Cycle)
                    {
                        case 1:
                            reason = StockMovementReasons.NoReason;
                            break;
                        case 2:
                            reason = stockReturn ? StockMovementReasons.ShippingToMaterial : StockMovementReasons.MaterialToShipping;
                            break;
                    }
                    break;
                case "CT_LIG":
                    reason = stockReturn ? StockMovementReasons.ShippingToMaterial : StockMovementReasons.IguMaterialToShipping;
                    break;
                case "CT_RMI":
                case "CT_RME":

                    break;
                default:
                    reason = StockMovementReasons.NoReason;
                    break;
            }

            var reasonStockMovement = StockMovementReasonService.GetByReason(reason.ToString());
            if (reasonStockMovement == null)
            {
                return new();
            }

            int materialId = MaterialService.GetId(movement.Reference);

            StockMovement stockMovement = new()
            {
                ReasonId = reasonStockMovement.Id,
                FromStockId = StockService.GetId(materialId, movement.Length, movement.Height, reasonStockMovement.FromWarehouseId),
                ToStockId = StockService.GetId(materialId, movement.Length, movement.Height, reasonStockMovement.ToWarehouseId)
            };

            return stockMovement;
        }
        #endregion CollectionApi

        #region ExternalMovement
        public bool ExternalMovementBusy { get; set; }
        public bool MoveExternalStock()
        {
            ExternalMovementBusy = true;
            try
            {
                var externalMovements = ExternalMovementService.GetAll().ToList();

                foreach (var movement in externalMovements)
                {
                    var stockMovementReason = StockMovementReasonService.GetByReason(movement.ReasonName);
                    if (stockMovementReason is null)
                    {
                        throw new Exception("Not Set Reason Stock Movement " + JsonConvert.SerializeObject(movement).ToString());
                    }

                    var materialId = MaterialService.GetId(movement.MaterialCode);
                    if (materialId == 0)
                    {
                        throw new Exception("Not Set Material " + JsonConvert.SerializeObject(movement).ToString());
                    }

                    int EmployeeId = 0;
                    var stockMovement = new StockMovement()
                    {
                        Quantity = movement.Quantity,
                        EmployeeId = int.TryParse(movement.UserName, out EmployeeId) ? EmployeeId : EmployeeService.GetIdByUserName(movement.UserName),
                        ReasonId = stockMovementReason.Id,
                        Comment = movement.Comment,
                        DocumentNumber = movement.DocumentNumber,
                        InsertDate = DateTime.Now,
                        DocumentDate = movement.DocumentDate,
                        DocumentId = StockDocumentService.GetID(movement.DocumentName),
                        FromStockId = StockService.GetId(materialId, movement.Length, movement.Height, stockMovementReason.FromWarehouseId),
                        ToStockId = StockService.GetId(materialId, movement.Length, movement.Height, stockMovementReason.ToWarehouseId)

                    };
                    StockMovementService.Create(stockMovement);

                    //Try create or update Accounting
                    _stockAccountingLogic.TryCreateOrUpdateByStockMovement(stockMovement.Id);

                    ExternalMovementService.Delete(movement);
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "MoveExternalStock error");
                
                return false;
            }
            finally
            {
                ExternalMovementBusy = false;
            }
        }
        #endregion ExternalMovement

        public bool MoveStocks(RequestMoveStock requestMoveStocks, out string errMessage)
        {
            try
            {
                errMessage = string.Empty;
                var stockMovementList = new List<StockMovement>();
                foreach (var moveStock in requestMoveStocks.MoveStocks)
                {
                    var stockMovement = CreateMovement(requestMoveStocks.TaskId, moveStock);
                    if (stockMovement != null)
                    {
                        //StockMovementService.Create(stockMovement);
                        stockMovementList.Add(stockMovement);
                    }
                }

                //update task
                var isTask = 0;
                switch (requestMoveStocks.TaskEvent)
                {
                    case EnumTaskEvent.Delete:
                        {
                            isTask = _taskService.DeleteTaskRemnant(requestMoveStocks.TaskId, requestMoveStocks.SelectCycleOrders);
                        }
                        break;
                    case EnumTaskEvent.Update:
                        {
                            isTask = _taskService.UpdateTaskRemnant(requestMoveStocks.TaskId, requestMoveStocks.Barcode, requestMoveStocks.SelectCycleOrders, requestMoveStocks.ReturnWarehouse);
                        }
                        break;
                    default:
                        {
                            errMessage = "Bad Task event";
                            Logger.LogError(errMessage);
                            return false;
                        }
                }

                if (stockMovementList.Any() && isTask > 1)
                {
                    stockMovementList.ForEach(StockMovementService.Create);
                }
                else
                {
                    errMessage = "Problems with update/delete Task Remnant or nothing to move";
                    Logger.LogError(errMessage);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "MoveStocks error");
                errMessage = ex.Message;
                return false;
            }
        }

        public MaterialStockInfoDto? GetMaterialStockInfo(string code, Warehouses warehouseId = Warehouses.NoWarehouse)
        {
            try
            {
                var warehouses = new List<int> { (int)Warehouses.Material, (int)Warehouses.FreeRemnants, (int)Warehouses.ReservedRemnants };

                if (warehouseId != Warehouses.NoWarehouse)
                {
                    warehouses.Clear();
                    warehouses.Add((int)warehouseId);
                }


                var r = MaterialService.GetMaterialWithStockInfo(code, warehouses);
                return _mapper.Map<MaterialStockInfoDto>(r);

            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "MaterialStockInfoDTO error");
                return null;
            }

        }


        public StockMovement? CreateMovement(int taskId,  MoveStock moveStock)
        {
            moveStock.Length = (int)(moveStock.Length / 200) * 200;

            var stackDocument = StockDocuments.Task;

            var reason = StockMovementReasons.NoReason;
            switch (moveStock.TypeKey)
            {

                case "CT_RMI":
                    {
                        if (!string.IsNullOrEmpty(moveStock.Option))
                        {
                            if (!Enum.TryParse(moveStock.Option, true, out reason))
                            {
                                throw new Exception(string.Format("Bad Stock Movement Reason: {0}", moveStock.Option));
                            }

                            if(moveStock.DocumentNumber > 0)
                            {
                                stackDocument = StockDocuments.OrderNumber;
                            } else
                            {
                                stackDocument = StockDocuments.Task;
                                moveStock.DocumentNumber = taskId;
                            }                            
                        }

                    }
                    break;
                case "CT_RME":
                    if (!string.IsNullOrEmpty(moveStock.Option))
                    {
                        if (!Enum.TryParse(moveStock.Option, true, out reason))
                        {
                            throw new Exception(string.Format("Bad Stock Movement Reason: {0}", moveStock.Option));
                        }
                    }
                    break;
                default:
                    reason = StockMovementReasons.NoReason;
                    break;
            }

            var reasonStockMovement = StockMovementReasonService.GetByReason(reason.ToString());
            if (reasonStockMovement == null)
            {
                throw new Exception("Not Set Reason Stock Movement");
            }
            var materialId = MaterialService.GetId(moveStock.Reference ?? "");
            if (materialId == 0)
            {
                throw new Exception("Not Set Material");
            }
            var documentId = StockDocumentService.GetID(stackDocument.ToString());
            if (documentId == 0)
            {
                throw new Exception("Not Set Stock Document");
            }

            var employeeId = EmployeeService.GetId(moveStock.EmployeeId);
            if (employeeId == 0)
            {
                throw new Exception("Not Set Employee");
            }

            int? fromStockId = null;
            int? toStockId = null;

            var fromStock = StockService.GetByFilter(materialId, moveStock.Length, moveStock.Height, reasonStockMovement.FromWarehouseId);
            var toStock = StockService.GetByFilter(materialId, moveStock.Length, moveStock.Height, reasonStockMovement.ToWarehouseId);

            if (fromStock == null)
            {
                if (fromStock == null && reasonStockMovement.FromWarehouseId.HasValue)
                {
                    var stock = new Stock()
                    {
                        MaterialId = materialId,
                        WarehouseID = reasonStockMovement.FromWarehouseId.Value,
                        Length = moveStock.Length,
                        Height = moveStock.Height,
                        Quantity = 0.0
                    };
                    StockService.Create(stock);
                    fromStock = stock;
                    fromStockId = stock.Id;
                }

            }
            else
            {
                fromStockId = fromStock.Id;
            }


            if (fromStock != null && fromStock.Quantity < 1)
            {
                if (reason == StockMovementReasons.LotRemnantsFreeToReserved || reason == StockMovementReasons.FreeRemnantToReserved || reason == StockMovementReasons.FreeRemnantToProduction || reason == StockMovementReasons.RemoveFreeRemnant)
                {
                    throw new Exception("There is no such free remnant.");
                }
                else if (reason == StockMovementReasons.ReservedRemnantToProduction)
                {
                    throw new Exception("There is no such reserved remnant.");
                }
            }

            if (toStock == null)
            {
                if (reasonStockMovement.ToWarehouseId.HasValue)
                {
                    var stock = new Stock()
                    {
                        MaterialId = materialId,
                        WarehouseID = reasonStockMovement.ToWarehouseId.Value,
                        Length = moveStock.Length,
                        Height = moveStock.Height,
                        Quantity = 0.0
                    };

                    StockService.Create(stock);
                    toStockId = stock.Id;
                }

            }
            else
            {
                toStockId = toStock.Id;
            }

            return new StockMovement()
            {
                ReasonId = reasonStockMovement.Id,
                FromStockId = fromStockId,
                ToStockId = toStockId,
                Quantity = moveStock.Quantity,
                EmployeeId = employeeId,
                Comment = moveStock.Comment ?? "",
                DocumentId = documentId,
                DocumentNumber = moveStock.DocumentNumber,
                InsertDate = DateTime.Now,
                DocumentDate = DateTime.Now
            };

        }

        public async Task<MaterialDescriptionDTO> GetMaterialsDescriptionsAsync(MaterialDescriptionRequestDTO materialDescriptionDTO)
        {
            var result = new MaterialDescriptionDTO()
            {
                MaterialCodesDescriptions = new Dictionary<string, string>()
            };
            var requiredMaterials = await MaterialService.GetAllQueryable().Where(mt => materialDescriptionDTO.MaterialCodes.Contains(mt.Code)).Select(mt => new { mt.Code, mt.Description }).ToListAsync();
            
            foreach ( var requiredMaterial in requiredMaterials)
            {
                result.MaterialCodesDescriptions.Add(requiredMaterial.Code, requiredMaterial.Description);
            }
            return result;
        }

        public async Task<IEnumerable<MaterialDto>> GetMaterialsByCodesAsync(MaterialDescriptionRequestDTO materialDescriptionDTO)
        {
            var requiredMaterials = await MaterialService.GetAllQueryable().Where(mt => materialDescriptionDTO.MaterialCodes.Contains(mt.Code)).ToListAsync();
            return _mapper.Map<IEnumerable<MaterialDto>>(requiredMaterials);
        }
    }


}
