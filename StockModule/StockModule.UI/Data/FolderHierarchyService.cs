﻿using Microsoft.AspNetCore.Mvc;
using StockModule.UI.Model;
using System.Net.Http.Json;

namespace StockModule.UI.Data
{
    public class FolderHierarchyService
    {
        private HttpClient _http;
        const string MATERIALSEARCHBASEURL = "FolderHierarchy";
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
                return await _http.PostAsJsonAsync(url,fldrhrcy.Icon);
            }
            else
            {
                return await _http.PostAsJsonAsync(url + "&parentId=" + fldrhrcy.ParentId, fldrhrcy.Icon);
            }
        }

        public async Task<HttpResponseMessage> EditAsync(FolderHierarchyVM fldrhrcy)
        {
            string url = MATERIALSEARCHBASEURL + "?id=" + fldrhrcy.Id;
            if (!fldrhrcy.ParentId.HasValue)
            {
                return await _http.PutAsJsonAsync(url + "&name=" + fldrhrcy.Name, fldrhrcy.Icon);
            }
            else
            {
                return await _http.PutAsJsonAsync(url + "&name=" + fldrhrcy.Name + "&parentId=" + fldrhrcy.ParentId, fldrhrcy.Icon);
            }
        }
        public async Task<List<FolderHierarchyVM>> GetAllParentFoldersByMaterialId(int materialId)
        {
            return await _http.GetFromJsonAsync<List<FolderHierarchyVM>>(MATERIALSEARCHBASEURL + "/getallparentfoldersbymaterialid/"+ materialId);
        }

        public async Task<List<FolderHierarchyVM>> GetAllParentFoldersByFolderName(string folderName)
        {
            return await _http.GetFromJsonAsync<List<FolderHierarchyVM>>(MATERIALSEARCHBASEURL + "/getallparentfoldersbyfoldername/" + folderName);
        }

        public async Task<List<FolderHierarchyVM>> GetAllParentFoldersByFolderId(int folderhierarchyid)
        {
            return await _http.GetFromJsonAsync<List<FolderHierarchyVM>>(MATERIALSEARCHBASEURL + "/getallparentfoldersbyfolderid/" + folderhierarchyid);
        }
    }
}
