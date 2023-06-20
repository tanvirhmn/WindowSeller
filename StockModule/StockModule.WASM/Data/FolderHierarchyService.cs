
using StockModule.UI.Model;
using System.Net.Http.Json;

namespace StockModule.UI.Data
{
    public class FolderHierarchyService
    {
        private HttpClient _http;
        const string MATERIALSEARCHBASEURL = "MaterialSearch";
        public FolderHierarchyService(HttpClient http)
        {
            _http = http;
        }

        public async Task<FolderHierarchyVM> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<FolderHierarchyVM>(MATERIALSEARCHBASEURL + "/getfolder/"+id);
        }

        public async Task<List<FolderHierarchyVM>> GetAllRootsAsync()
        {
            return await _http.GetFromJsonAsync<List<FolderHierarchyVM>>(MATERIALSEARCHBASEURL + "/getallroots");
        }
        public async Task<List<FolderHierarchyVM>> GetAllChiledrenAsync(int parentID)
        {
            //https://localhost:5096/MaterialSearch/getallchildren/6
            return await _http.GetFromJsonAsync<List<FolderHierarchyVM>>(MATERIALSEARCHBASEURL + "/getallchildren/" + parentID);
        }
        public async Task<List<FolderHierarchyVM>> GetAllNodes(string names)
        {
            //https://localhost:5096/MaterialSearch/getallnodes?names=o
            return await _http.GetFromJsonAsync<List<FolderHierarchyVM>>(MATERIALSEARCHBASEURL + "/getallfilterednodes?names=" + names);
        }
        public async Task<List<FolderHierarchyVM>> GetAllNodes()
        {
            return await _http.GetFromJsonAsync<List<FolderHierarchyVM>>(MATERIALSEARCHBASEURL + "/getallnodes");
        }
        public async Task<bool> HasChiledrenAsync(int parentID)
        {
            return await _http.GetFromJsonAsync<bool>(MATERIALSEARCHBASEURL + "/haschildren/" + parentID);
        }

        public async Task<HttpResponseMessage> CreateAsync(FolderHierarchyVM fldrhrcy)
        {
            string url = MATERIALSEARCHBASEURL + "?name=" + fldrhrcy.Name;
            if (!fldrhrcy.ParentId.HasValue)
            {
                return await _http.PostAsync(url + "&icon=" + fldrhrcy.Icon, null);
            }
            else
            {
                return await _http.PostAsync(url + "&parentId=" + fldrhrcy.ParentId + "&icon=" + fldrhrcy.Icon, null);
            }
        }

        public async Task<HttpResponseMessage> EditAsync(FolderHierarchyVM fldrhrcy)
        {
            string url = MATERIALSEARCHBASEURL + "?id=" + fldrhrcy.Id;
            if (!fldrhrcy.ParentId.HasValue)
            {
                return await _http.PutAsync(url + "&name=" + fldrhrcy.Name + "&icon=" + fldrhrcy.Icon, null);
            }
            else
            {
                return await _http.PutAsync(url + "&name=" + fldrhrcy.Name + "&parentId=" + fldrhrcy.ParentId + "&icon=" + fldrhrcy.Icon, null);
            }
        }
        public async Task<List<FolderHierarchyVM>> GetAllParentFoldersByMaterialId(int materialId)
        {
            return await _http.GetFromJsonAsync<List<FolderHierarchyVM>>(MATERIALSEARCHBASEURL + "/getallparentfoldersbymaterialid/"+ materialId);
        }
    }
}
