using AutoMapper;
using StockModule.DAL.Models;
using StockModule.DAL.Services;
using Microsoft.Extensions.Logging;
using System.Text;
using StockModule.BLL.Dto;
using StockModule.BLL.Helpers;
using System.Linq.Dynamic.Core;
using Microsoft.Extensions.Configuration;
using AutoMapper.QueryableExtensions;
using StockModule.BLL.SubClasses.RivileModels;
using StockModule.BLL.SubClasses.StockAccountingModels;

namespace StockModule.BLL
{
    public class StockAccountingLogic : IStockAccountingLogic
    {
        private readonly IStockAccountingMovementService _stockAccountingMovementService;
        private readonly IStockMovementService _stockMovementService;
        private readonly IStockAccountingActionService _stockAccountingActionService;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly string? _accountingApiUrl;
        private readonly string? _accountingApiKey;
        private readonly string? _accountingDocNo;
        private readonly int[] _filterStatusList;

        public StockAccountingLogic(IStockAccountingMovementService stockAccountingMovementService,
                             IStockMovementService stockMovementService,
                             IStockAccountingActionService stockAccountingActionService,
                             ILogger<MaterialLogic> logger,
                             IMapper mapper,
                             IConfiguration configuration)
        {

            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
            _stockAccountingMovementService = stockAccountingMovementService;
            _stockAccountingActionService = stockAccountingActionService;
            _stockMovementService = stockMovementService;
            _accountingApiUrl = _configuration["StockAccounting:ApiUrl"];
            _accountingApiKey = _configuration["StockAccounting:ApiKey"];
            _accountingDocNo = _configuration["StockAccounting:DocNoTemplate"];
            _filterStatusList = new[]{ (int)StockAccountingEnums.NOT_STARTED, (int)StockAccountingEnums.ERROR, (int)StockAccountingEnums.MISMATCH };

        }
        /// <summary>
        ///  Create or update stock accounting movement by stock movement id 
        /// </summary>
        /// <param name="stockMovementId"></param>
        /// <returns></returns>
        public bool TryCreateOrUpdateByStockMovement(int stockMovementId)
        {

            var stockMovement = _stockMovementService.GetById(stockMovementId);
            if (stockMovement != null)
            {
                if (stockMovement.StockMovementReason.IsGenerateAccountingEvent)
                {
                   return CreateOrUpdate(stockMovement);
                }
              
            }
            return false;
        }


        /// <summary>
        /// Create or Update canceled stock accounting movement or create new and send request to Rivile by stock movement
        /// </summary>
        /// <param name="stockMovement"></param>
        /// <returns></returns>
        private bool CreateOrUpdate(StockMovement stockMovement)
        {
            if(!stockMovement.StockMovementReason.AccountingEventCanceledReasonId.HasValue)
            {
                return (Create(stockMovement.Id) > 0);
            }
            
            var reasonId= stockMovement.StockMovementReason.AccountingEventCanceledReasonId.Value;

            var stockAcocunting = _stockAccountingMovementService.GetFirstByReasonAndStatus(reasonId, new[] { (int)StockAccountingEnums.NOT_STARTED, (int)StockAccountingEnums.COMPLETED }, stockMovement.ToStockId, stockMovement.FromStockId);
            if (stockAcocunting == null)
            {
                return false;
            }
            else if (stockAcocunting.Status == (int)StockAccountingEnums.NOT_STARTED)
            {
                stockAcocunting.ChangedByStockMovementId = stockMovement.Id;
                stockAcocunting.Status = (int)StockAccountingEnums.CANCELED; //Canceled                
                stockAcocunting.UpdatedDate = DateTime.Now;
                _stockAccountingMovementService.Update(stockAcocunting);
            }
            else
            {

                var stockAcocuntingId = Create(stockMovement.Id, (int)StockAccountingEnums.AUTO);
                if (stockAcocuntingId == 0)
                {
                    return false;
                }
                //Auto send Rivile Request
                var accontingTransferRequest = new StockAccontingTransferRequest();
                accontingTransferRequest.IsConfirmTransfer = true;
                accontingTransferRequest.StockAccountingIds.Add(stockAcocuntingId);
                var response = TransferToAccounting(accontingTransferRequest).Result;
                if (response.IsSuccess)
                {
                    _logger.LogInformation(string.Format("AUTO TransferToAccounting success by stockMovementId: {0}", stockMovement.Id));
                }
                else
                {
                    _logger.LogError(string.Format("AUTO TransferToAccounting error by stockMovementId: {0}", stockMovement.Id));
                }
             
            }

            return true;

        }


        /// <summary>
        /// Create stock accounting movement by stock movement id
        /// </summary>
        /// <param name="stokMovementId"></param>
        /// <returns>stock accounting movement id</returns>
        private int Create(int stokMovementId, int status = 0)
        {
            try
            {
                if (!_stockAccountingMovementService.IsExistByStockMovement(stokMovementId))
                {
                    return _stockAccountingMovementService.CreateByStockMovement(stokMovementId, status);

                }
                else
                {
                    _logger.LogError(string.Format("Already exists by stockMovementId: {0}", stokMovementId));
                    return 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Create error");
                return 0;
            }
        }

        public List<int> GetAllStockMovementByFromDate(DateTime datetime)
        {
            return _stockMovementService.GetStockMovementIdByFromDateForAccounting(datetime);
        }



        /// <summary>
        /// Get all stock accounting movements
        /// </summary>
        /// <param name="json"></param>
        /// <returns>List stock accounting movements DTO</returns>
        public List<StockAccountingMovementDto> GetAll()
        {
            var r = new List<StockAccountingMovementDto>();
            try
            {
                var stockAccountingMovements = _stockAccountingMovementService.GetFullAll();
                r = _mapper.Map<List<StockAccountingMovementDto>>(stockAccountingMovements);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAll error");

            }
            return r;
        }

        /// <summary>
        /// Get stock accounting movements by filter (íf status 'not started' and 'error' return always all, filter not work)
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>List stock accounting movements DTO</returns>
        public List<StockAccountingMovementDto> GetByFilter(string? filter)
        {
            var r = new List<StockAccountingMovementDto>();
            try
            {
               
                r = _stockAccountingMovementService.GetQueryableAllWithIncludes().Where(_ => _filterStatusList.Any(__=>__==_.Status)).OrderBy(_ => _.StockMovement.DocumentDate).ProjectTo<StockAccountingMovementDto>(_mapper.ConfigurationProvider).ToList();
                var stockAccountingMovements = _stockAccountingMovementService.GetQueryableAllWithIncludes().Where(_ => !_filterStatusList.Any(__ => __ == _.Status)).OrderBy(_ => _.StockMovement.DocumentDate).ProjectTo<StockAccountingMovementDto>(_mapper.ConfigurationProvider);
                if (!string.IsNullOrEmpty(filter) && filter != "_")
                {                   
                    stockAccountingMovements = stockAccountingMovements.Where(filter);                    
                } else
                {
                    stockAccountingMovements = stockAccountingMovements.Where(_ => _.Date >= DateTime.Now.AddMonths(-1));
                }
                r.AddRange(stockAccountingMovements.ToList());


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetByFilter error");

            }
            return r;
        }


        /// <summary>
        ///  Transfert to Rivile web api requests
        /// </summary>
        /// <param name="accontingTransferRequest"></param>
        /// <returns>Return status</returns>
        public async Task<StockAccontingTransferResponse> TransferToAccounting(StockAccontingTransferRequest accontingTransferRequest)
        {
            var r = new StockAccontingTransferResponse();

            try
            {
                foreach (var stockAccountingMovementId in accontingTransferRequest.StockAccountingIds)
                {
                    var rItem = new StockAccontingItemResponse();
                  
                    var stockAccountingMovement = _stockAccountingMovementService.GetFullInfoById(stockAccountingMovementId);
                    if (stockAccountingMovement != null)
                    {
                        rItem.IsSuccessTransfer = false;
                        RivileReTDok? dok = null;
                        RivileDataN17? dataN17 = null;
                        int actionConfirmCount = 0;
                        if (stockAccountingMovement.Status == (int)StockAccountingEnums.ERROR)
                        {
                          var action =  stockAccountingMovement.StockAccountingActions!.FirstOrDefault(_ => _.IsSuccess && _.Method == EnumRivileMethod.EDIT_I09_FULL.ToString());
                            if (action != null) {
                                rItem.IsSuccessTransfer = action.IsSuccess;
                                dok = action.ResponseToObject();
                            }
                            actionConfirmCount = stockAccountingMovement.StockAccountingActions!.Count(_ => _.IsSuccess && _.Method == EnumRivileMethod.EDIT_I09.ToString());                           

                        }
                        if (stockAccountingMovement.Status == (int)StockAccountingEnums.MISMATCH)
                        {
                            var action = stockAccountingMovement.StockAccountingActions!.FirstOrDefault(_ => _.IsSuccess && _.Method == EnumRivileMethod.GET_N17_LIST.ToString());
                            if (action != null && action.IsSuccess)                            {
                              
                                var dok1 = action.ResponseToObject();
                                if (dok1 != null)
                                {
                                    dataN17 = dok1.N17;
                                }
                            }                         

                        }

                        if (!rItem.IsSuccessTransfer)
                        {
                            var isSend = true;
                            if (dataN17 == null)
                            {
                                var response1 = await TransferItem(stockAccountingMovement, EnumRivileMethod.GET_N17_LIST);

                                if (response1.IsSuccess && response1.retDoK != null && response1.retDoK.N17 != null)
                                {
                                    dataN17 = response1.retDoK!.N17;
                                    if(dataN17!=null)
                                    {
                                       if(!stockAccountingMovement.StockMovement.IsValidUnit(dataN17.N17_KODAS_US))
                                        {
                                            rItem.IsSuccessTransfer = false;
                                            rItem.IsSuccessTransfer1 = false;
                                            rItem.IsSuccessTransfer2 = false;
                                            isSend = false;
                                            stockAccountingMovement.Status = (int)StockAccountingEnums.MISMATCH;
                                            rItem.ErrorMessage = "Units of measurement do not match";
                                        }
                                    }
                                  
                                }
                            }
                            if (isSend)
                            {
                                var response = await TransferItem(stockAccountingMovement, EnumRivileMethod.EDIT_I09_FULL, "", dataN17);
                                rItem.IsSuccessTransfer = response.IsSuccess;
                                rItem.ErrorMessage = response.retDoK == null ? response.ErrorMessage : response.retDoK.ErrorMessage + "\n";
                                dok = response.retDoK;
                            }
                        }

                        if (rItem.IsSuccessTransfer && dok != null && accontingTransferRequest.IsConfirmTransfer)
                        {                      
                            var code = dok.I09!.I09_KODAS_VD;
                            if (!string.IsNullOrEmpty(code))
                            {                              

                                if (actionConfirmCount == 0) { 
                                    var response1 = await TransferItem(stockAccountingMovement, EnumRivileMethod.EDIT_I09, code);
                                    rItem.IsSuccessTransfer1 = response1.IsSuccess;
                                    rItem.ErrorMessage += response1.retDoK == null ? response1.ErrorMessage : response1.retDoK.ErrorMessage + "\n";
                                    if (rItem.IsSuccessTransfer1)
                                    {
                                        actionConfirmCount++;
                                    }
                                }
                                else if (actionConfirmCount == 1)
                                {
                                    rItem.IsSuccessTransfer1 = true;
                                }                                                        
                             
                                if (actionConfirmCount == 1)
                                {
                                    var response2 = await TransferItem(stockAccountingMovement, EnumRivileMethod.EDIT_I09, code);
                                    rItem.IsSuccessTransfer2 = response2.IsSuccess;
                                    rItem.ErrorMessage += response2.retDoK == null ? response2.ErrorMessage : response2.retDoK.ErrorMessage + "\n";
                                }                                
                               
                            }

                        } else if(!accontingTransferRequest.IsConfirmTransfer)
                        {
                            rItem.IsSuccessTransfer1 = true;
                            rItem.IsSuccessTransfer2 = true;
                        } 
                        r.StockAccountingItemResponses.Add(rItem);
                        stockAccountingMovement.Status = rItem.IsSuccess ? ((stockAccountingMovement.Status == (int)StockAccountingEnums.AUTO) ? (int)StockAccountingEnums.AUTO : (int)StockAccountingEnums.COMPLETED) : ((stockAccountingMovement.Status == (int)StockAccountingEnums.MISMATCH) ? (int)StockAccountingEnums.MISMATCH:(int)StockAccountingEnums.ERROR);
                        stockAccountingMovement.UpdatedDate = DateTime.Now;
                        _stockAccountingMovementService.Update(stockAccountingMovement);

                    }
                }

            }
            catch (Exception ex)
            {          
                _logger.LogError(ex, "TransferToAccounting error");

            }
            return r;
        }
        /// <summary>
        /// Transfert one to Rivile web api request (save request and response)
        /// </summary>
        /// <param name="stockAccountingMovement"></param>
        /// <param name="enumRivile"></param>
        /// <param name="rivileCode"></param>
        /// <returns>return response Rivile api</returns>
        private async Task<RivileResponse> TransferItem(StockAccountingMovement stockAccountingMovement, EnumRivileMethod enumRivile, string rivileCode="", RivileDataN17? dataN17=null)
        {
            var r= new RivileResponse();
            var docNo = string.Format("SM{0:D10}", stockAccountingMovement.StockMovementId);   
                                  
            var jsonRequest = stockAccountingMovement.GetRivileRequest(enumRivile, docNo, rivileCode, dataN17).ToJsonString();
            var action = new StockAccountingAction()
            {
                Method = enumRivile.ToString(),
                CreatedDate = DateTime.Now,
                Request = jsonRequest,
                StockAccountingMovementId = stockAccountingMovement.Id,
                IsSuccess = false
            };

            try
            {             
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("ApiKey", _accountingApiKey);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(string.Format("{0}", _accountingApiUrl), content);
                action.IsSuccess = response.IsSuccessStatusCode;
                action.Response = await response.Content.ReadAsStringAsync();
                r.IsSuccess = response.IsSuccessStatusCode;
                r.retDoK = action.ResponseToObject();            
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SendToRivile error");
                action.IsSuccess = false;
                action.Response = ex.Message;
                r.ErrorMessage = ex.Message;              
            }
            _stockAccountingActionService.Create(action);

            return r;
        }




    }  
}