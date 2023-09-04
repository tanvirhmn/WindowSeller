using StockModule.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.DAL.Services.Interfaces
{
    public interface IFolderHierarchyService : IEntityService<FolderHierarchy>
    {
        List<FolderHierarchy> GetAllParentFoldersByMaterialId(int rowNo, string columnName);
        List<FolderHierarchy> GetAllParentFoldersByFolderId(int folderHierarchyId);

        List<FolderHierarchy> GetAllParentFoldersByFolderName(string folderName);
        IEnumerable<FolderHierarchy> GetAllRoots();
        IEnumerable<FolderHierarchy> GetAllChildren(int parentId);
        IEnumerable<FolderHierarchy> GetAllNodes(string names);
        IEnumerable<FolderHierarchy> GetAllNodes();
        bool HasChildren(int parentId);
    }
}
