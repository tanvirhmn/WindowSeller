using Microsoft.AspNetCore.Mvc;
using StockModule.BLL.Dto;
using StockModule.BLL.Dto.FolderHierarchy;
using StockModule.BLL.Dto.FolderHierarchy.Validators;
using StockModule.BLL.Interfaces;
using StockModule.BLL.Logic;
using System.Drawing;
using System.Net;
using WindowSellerWASM.BLL.Responses;

namespace StockModule.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FolderHierarchyController : Controller
    {
        private readonly ILogger<FolderHierarchyController> _logger;
        private readonly IFolderHierarchyLogic _folderHierarchyLogic;

        public FolderHierarchyController(ILogger<FolderHierarchyController> logger, IFolderHierarchyLogic folderHierarchyLogic)
        {
            _logger = logger;
            _folderHierarchyLogic = folderHierarchyLogic;
        }

        #region FolderHierarchyDto

        /// <summary>
        /// Get All Folders
        /// </summary>
        /// <returns></returns>
        [HttpGet("getallfolders")]
        public IEnumerable<FolderHierarchyDto> GetAllFolders()
        {
            return _folderHierarchyLogic.GetAll();
        }

        /// <summary>
        /// Get All Root Folders
        /// </summary>
        /// <returns></returns>
        [HttpGet("getallroots")]
        public IEnumerable<FolderHierarchyDto> GetAllRoots()
        {
            return _folderHierarchyLogic.GetAllRoots();
        }

        /// <summary>
        /// Get All cHILDREN
        /// </summary>
        /// <returns></returns>
        [HttpGet("getallchildren/{parentId}")]
        public IEnumerable<FolderHierarchyDto> GetAllChildren(int parentId)
        {
            return _folderHierarchyLogic.GetAllChildren(parentId);
        }

        /// <summary>
        /// Get All PARENTS
        /// </summary>
        /// <returns></returns>
        [HttpGet("getallparentfoldersbymaterialid/{materialId}")]
        public IEnumerable<FolderHierarchyDto> GetAllParentFoldersByMaterialId(int materialId)
        {
            return _folderHierarchyLogic.GetAllParentFoldersByMaterialId(materialId);
        }

        /// <summary>
        /// Get All PARENTS
        /// </summary>
        /// <returns></returns>
        [HttpGet("getallparentfoldersbyfolderid/{folderhierarchyid}")]
        public IEnumerable<FolderHierarchyDto> GetAllParentFoldersByFolderId(int folderHierarchyId)
        {
            return _folderHierarchyLogic.GetAllParentFoldersByFolderId(folderHierarchyId);
        }

        /// <summary>
        /// cHECK FOR CHILDREN
        /// </summary>
        /// <returns></returns>
        [HttpGet("haschildren/{parentId}")]
        public bool HasChildren(int parentId)
        {
            return _folderHierarchyLogic.HasChildren(parentId);
        }

        /// <summary>
        /// Get Folder By Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("getfolder/{id}")]
        public FolderHierarchyDto GetFolderById(int id)
        {
            return _folderHierarchyLogic.GetById(id);
        }

        /// <summary>
        /// Get Folder By Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("uploadfolderimage")]
        public FolderHierarchyDto Upload(int id)
        {
            return _folderHierarchyLogic.GetById(id);
        }

        // POST: MaterialSearchController/Create
        [HttpPost]
        public ActionResult<BaseCommandResponse> Create(string name, int? parentId, [FromBody] string icon)
        {
            try
            {
                FolderHierarchyDto fdlhrchy = new FolderHierarchyDto() { Name = name, ParentId = parentId, Icon = icon };


                var respone = _folderHierarchyLogic.Create(fdlhrchy);

                return Ok(respone);
            }
            catch (Exception ex) { _logger.LogError(ex.Message); return StatusCode((int)HttpStatusCode.InternalServerError); }
        }

        // PUT: MaterialSearchController/Edit/
        [HttpPut]
        public ActionResult<BaseCommandResponse> Edit(int id, string name, int? parentId, [FromBody] string icon)
        {
            try
            {
                FolderHierarchyDto fdlhrchy = new FolderHierarchyDto() { Id = id, Name = name, ParentId = parentId, Icon = icon };

                var respone = _folderHierarchyLogic.Edit(fdlhrchy);
                return Ok(respone);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }


        // DELETE: MaterialSearchController/Delete/5
        [HttpDelete]
        public ActionResult<BaseCommandResponse> Delete(int id, string name, int? parentId, [FromBody] string icon)
        {

            try
            {
                FolderHierarchyDto fdlhrchy = new FolderHierarchyDto() { Id = id, Name = name, ParentId = parentId, Icon = icon };
                var respone = _folderHierarchyLogic.Delete(fdlhrchy);
                return Ok(respone);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Node serach
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        [HttpGet("getallfilterednodes")]
        public IEnumerable<FolderHierarchyDto> GetAllNodes(string names)
        {
            return _folderHierarchyLogic.GetAllNodes(names);
        }

        /// <summary>
        /// Node serach
        /// </summary>
        /// <returns></returns>
        [HttpGet("getallnodes")]
        public IEnumerable<FolderHierarchyDto> GetAllNodes()
        {
            return _folderHierarchyLogic.GetAllNodes();
        }

        /// <summary>
        /// Get All PARENTS
        /// </summary>
        /// <returns></returns>
        [HttpGet("getallparentfoldersbyfoldername/{folderName}")]
        public IEnumerable<FolderHierarchyDto> GetAllParentFoldersByFolderName(string folderName)
        {
            return _folderHierarchyLogic.GetAllParentFoldersByFolderName(folderName);
        }
        #endregion
    }
}
