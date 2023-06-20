
using StockModule.UI.Model;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace StockModule.UI.Data
{
    public class StockAccountingService
    {
        private HttpClient _http;
        const string STOCKSETTINGSBASEURL = "StockAccounting";

        public StockAccountingService(HttpClient http)
        {
            _http = http;
        }

        /// <summary>
        /// Send stock accounting ids to stock api transfer to rivile accounting
        /// </summary>
        /// <param name="transferRequest"></param>
        /// <returns>status</returns>
        public async Task<StockAccountingTransferResponse> TransferAccountingAsync(StockAccountingTransferRequest transferRequest)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(transferRequest), Encoding.UTF8, "application/json");
                var response = await _http.PostAsync(STOCKSETTINGSBASEURL+"/transfer", content);
                if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    var rContent = await response.Content.ReadFromJsonAsync<StockAccountingTransferResponse>();
                    if (rContent != null)
                    {
                        return rContent;
                    }
                    else
                    {
                        return new StockAccountingTransferResponse()
                        {
                            IsSuccess = response.IsSuccessStatusCode,
                            Message = "Response bad content"
                        };
                    }

                }

                return new StockAccountingTransferResponse()
                {
                    IsSuccess = response.IsSuccessStatusCode,
                    Message = await response.Content.ReadAsStringAsync()
                };

            }
            catch (Exception ex)
            {
                return new StockAccountingTransferResponse()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
        /// <summary>
        /// From stock api get stock accounting list by date filter  
        /// </summary>
        /// <param name="filterDate"></param>
        /// <returns>List stock accounting</returns>
        public async Task<IEnumerable<StockAccountingVM>> GetStockAccountingAsync(string? filterDate)
        {

            string apiPath = string.Format(STOCKSETTINGSBASEURL+"/filter/{0}", !string.IsNullOrEmpty(filterDate) ? filterDate : "_");
            var r = await _http.GetFromJsonAsync<List<StockAccountingVM>>(apiPath);
            return r ?? new List<StockAccountingVM>();

        }
    }
}