
using StockModule.UI.Model;
using System.Net.Http.Json;

namespace StockModule.UI.Data
{
    public class StockSettingsMaterial_FolderHierarchyService
    {
        private HttpClient _http;
        const string MATERIALSEARCHBASEURL = "MaterialSearch";

        public StockSettingsMaterial_FolderHierarchyService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<StockSettingsMaterial_FolderHierarchyVM>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<StockSettingsMaterial_FolderHierarchyVM>>(MATERIALSEARCHBASEURL);
        }

        public async Task<List<StockSettingsMaterial_FolderHierarchyVM>> GetTopFiveAsync()
        {
            return await _http.GetFromJsonAsync<List<StockSettingsMaterial_FolderHierarchyVM>>(MATERIALSEARCHBASEURL+"/gettopfive");
        }

        public async Task<List<StockSettingsMaterial_FolderHierarchyVM>> GetByFilterAsync(string? filter)
        {
            return await _http.GetFromJsonAsync<List<StockSettingsMaterial_FolderHierarchyVM>>(MATERIALSEARCHBASEURL+"?filter=" + filter);
        }
        public async Task<List<StockSettingsMaterial_FolderHierarchyVM>> GetCustomVirtualization(string? filter, string? order, int? skip, int? take)
        {
            return await _http.GetFromJsonAsync<List<StockSettingsMaterial_FolderHierarchyVM>>(MATERIALSEARCHBASEURL + "/getcustomvirtualization?filter=" + filter + "&order=" + order + "&skip=" + skip.Value + "&take=" + take.Value);
        }

        public async Task<int> GetCustomVirtualizationCount(string? filter)
        {
            return await _http.GetFromJsonAsync<int>(MATERIALSEARCHBASEURL + "/getcustomvirtualizationcount?filter=" + filter);
        }
    }
}
