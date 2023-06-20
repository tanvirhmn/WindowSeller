using StockModule.BLL.Dto.FolderHierarchy;
using StockModule.DAL.Models;
using StockModule.DAL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSellerWASM.BLL.Responses;

namespace StockModule.BLL.Interfaces
{
    public interface IFolderHierarchyLogic
    {

        List<FolderHierarchyDto> GetAll();
        List<FolderHierarchyDto> GetAllParentFoldersByMaterialId(int materialId);
        List<FolderHierarchyDto> GetAllParentFoldersByFolderId(int folderHierarchyId);
        List<FolderHierarchyDto> GetAllParentFoldersByFolderName(string folderName);
        List<FolderHierarchyDto> GetAllRoots();
        List<FolderHierarchyDto> GetAllChildren(int parentId);
        List<FolderHierarchyDto> GetAllNodes(string names);
        List<FolderHierarchyDto> GetAllNodes();
        FolderHierarchyDto GetById(int id);
        bool HasChildren(int parentId);
        public BaseCommandResponse Create(FolderHierarchyDto folderHierarchyDto);
        public BaseCommandResponse Edit(FolderHierarchyDto folderHierarchyDto);
        public BaseCommandResponse Delete(FolderHierarchyDto folderHierarchyDto);
    }
}
