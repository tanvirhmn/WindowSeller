using StockModule.UI.Model;

namespace StockModule.UI.Data
{
    public class StockSettingsService
    {
        private HttpClient _http;
        const string STOCKSETTINGSBASEURL = "StockSettings";

        public StockSettingsService(HttpClient http)
        {
            _http = http;
        }
        public async Task<HttpResponseMessage> UpdateStockSetting(List<int> ids, int folderHierarchyId)
        {
            return await _http.PutAsJsonAsync(STOCKSETTINGSBASEURL + "/updatestocksettinghierarchy?folderHierarchyId=" + folderHierarchyId,ids);
        }
    }
}
