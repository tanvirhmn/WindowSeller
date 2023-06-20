using AutoMapper;
using Azure.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StockModule.BLL.Dto.FolderHierarchy;
using StockModule.BLL.Dto.FolderHierarchy.Validators;
using StockModule.BLL.Interfaces;
using StockModule.BLL.SubClasses.StockAccountingModels;
using StockModule.DAL.Models;
using StockModule.DAL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSellerWASM.BLL.Responses;
using static Azure.Core.HttpHeader;

namespace StockModule.BLL.Logic
{
    public class FolderHierarchyLogic : IFolderHierarchyLogic
    {
        private readonly IFolderHierarchyService _folderHierarchyService;
        private readonly IView_StockSettingsMaterial_FolderHierarchyService _view_StockSettingsMaterial_FolderHierarchyService;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="folderHierarchyService"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        public FolderHierarchyLogic(
            IFolderHierarchyService folderHierarchyService,
            IConfiguration configuration,
            ILogger<FolderHierarchyLogic> logger,
            IMapper mapper,
            IView_StockSettingsMaterial_FolderHierarchyService view_StockSettingsMaterial_FolderHierarchyService)
        {
            _folderHierarchyService = folderHierarchyService;
            _configuration = configuration;
            _logger = logger;
            _mapper = mapper;
            _view_StockSettingsMaterial_FolderHierarchyService = view_StockSettingsMaterial_FolderHierarchyService;
        }

        public List<FolderHierarchyDto> GetAll()
        {
            var result = new List<FolderHierarchyDto>();
            try
            {
                var folderHierarchies = _folderHierarchyService.GetAll();
                result = _mapper.Map<List<FolderHierarchyDto>>(folderHierarchies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAll error");

            }
            return result;
        }
        public FolderHierarchyDto GetById(int id)
        {
            var result = new FolderHierarchyDto();
            try
            {
                var folderHierarchy = _folderHierarchyService.GetById(id);
                result = _mapper.Map<FolderHierarchyDto>(folderHierarchy);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAll error");

            }
            return result;
        }
        public BaseCommandResponse Create(FolderHierarchyDto folderHierarchyDto)
        {
            var response = new BaseCommandResponse();
            var validator = new FolderHierarchyDtoValidator(_folderHierarchyService);
            var validationResult =  validator.Validate(folderHierarchyDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(err => err.ErrorMessage).ToList();
            }
            else
            {

                var result = _mapper.Map<FolderHierarchy>(folderHierarchyDto);
                try
                {
                    _folderHierarchyService.Create(result);

                    response.Message = "Creation Successful";
                    response.Success = true;
                    response.Id = result.Id;
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = "Creation Failed";
                    response.Errors = new List<string>() { ex.Message };

                    _logger.LogError(ex, "Create error");

                }
            }
            return response;
            
        }

        public BaseCommandResponse Edit(FolderHierarchyDto folderHierarchyDto)
        {
            var response = new BaseCommandResponse();
            var validator = new FolderHierarchyDtoValidator(_folderHierarchyService);
            var validationResult = validator.Validate(folderHierarchyDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Update Failed";
                response.Errors = validationResult.Errors.Select(err => err.ErrorMessage).ToList();
            }
            else
            {
                var result = _mapper.Map<FolderHierarchy>(folderHierarchyDto);
                try
                {
                    _folderHierarchyService.Update(result);

                    response.Message = "Update Successful";
                    response.Success = true;
                    response.Id = result.Id;
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = "Update Failed";
                    response.Errors = new List<string>() { ex.Message};

                    _logger.LogError(ex, "Update error");

                }
            }
            return response;
        }

        public BaseCommandResponse Delete(FolderHierarchyDto folderHierarchyDto)
        {
            var response = new BaseCommandResponse();
            var result = _mapper.Map<FolderHierarchy>(folderHierarchyDto);
            try
            {
                _folderHierarchyService.Delete(result);

                response.Success = true;
                response.Message = "Delete Sucessful";
                response.Id = result.Id;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Delete Failed";
                response.Errors = new List<string>() { ex.Message };

                _logger.LogError(ex, "Delete error");

            }

            return response;
        }

        public List<FolderHierarchyDto> GetAllParentFoldersByMaterialId(int materialId)
        {
            var result = new List<FolderHierarchyDto>();
            try
            {
                var folderHierarchies = _folderHierarchyService.GetAllParentFoldersByMaterialId(materialId);
                result = _mapper.Map<List<FolderHierarchyDto>>(folderHierarchies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAllParentFoldersByMaterialId error");

            }
            return result;
        }
        public List<FolderHierarchyDto> GetAllRoots()
        {
            var result = new List<FolderHierarchyDto>();
            try
            {
                var folderHierarchies = _folderHierarchyService.GetAllRoots();
                result = _mapper.Map<List<FolderHierarchyDto>>(folderHierarchies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAllRoots error");

            }
            return result;
        }

        public List<FolderHierarchyDto> GetAllChildren(int parentId)
        {
            var result = new List<FolderHierarchyDto>();
            try
            {
                var folderHierarchies = _folderHierarchyService.GetAllChildren(parentId).ToList();
                if (folderHierarchies != null)
                {
                    result = _mapper.Map<List<FolderHierarchyDto>>(folderHierarchies);
                }

                var folderHierarchiesFromMaterial = _view_StockSettingsMaterial_FolderHierarchyService.GetByFolderHierarchyId(parentId).ToList();
                if (folderHierarchiesFromMaterial != null)
                {
                    result.AddRange(_mapper.Map<List<FolderHierarchyDto>>(folderHierarchiesFromMaterial));
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAllChildren error");

            }
            return result;
        }

        public bool HasChildren(int parentId)
        {
            bool result = false;
            try
            {
                result = _folderHierarchyService.HasChildren(parentId);

                if (!result)
                {
                    result = _view_StockSettingsMaterial_FolderHierarchyService.GetByFolderHierarchyId(parentId).ToList().Any();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "HasChildren error");

            }
            return result;
        }

        public List<FolderHierarchyDto> GetAllNodes(string names)
        {
            var result = new List<FolderHierarchyDto>();
            try
            {
                var folderHierarchies = _folderHierarchyService.GetAllNodes(names).ToList();
                if (folderHierarchies != null)
                {
                    result = _mapper.Map<List<FolderHierarchyDto>>(folderHierarchies);
                }

                var folderHierarchiesFromMaterial = _view_StockSettingsMaterial_FolderHierarchyService.GetAllNodes(names).ToList();
                if (folderHierarchiesFromMaterial != null)
                {
                    result.AddRange(_mapper.Map<List<FolderHierarchyDto>>(folderHierarchiesFromMaterial));
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAllChildren error");

            }
            return result;
        }

        public List<FolderHierarchyDto> GetAllNodes()
        {
            var result = new List<FolderHierarchyDto>();
            List<FolderHierarchyDto> hierarchicalItems = new List<FolderHierarchyDto>();
            try
            {
                var folderHierarchies = _folderHierarchyService.GetAll().ToList();
                if (folderHierarchies != null)
                {
                    result = _mapper.Map<List<FolderHierarchyDto>>(folderHierarchies);
                }

                var folderHierarchiesFromMaterial = _view_StockSettingsMaterial_FolderHierarchyService.GetAllNodes().ToList();
                if (folderHierarchiesFromMaterial != null)
                {
                    result.AddRange(_mapper.Map<List<FolderHierarchyDto>>(folderHierarchiesFromMaterial));
                }

                hierarchicalItems = RecrusiveHeirarchyCreator(result);

                //foreach (var item in hierarchicalItems)
                //{
                //    if (item.Icon.Trim().Length == 0)
                //    {
                //        item.HierarchyType = 2;
                //    }
                //}
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAllNodes error");

            }
            return hierarchicalItems;
        }

        private static List<FolderHierarchyDto> RecrusiveHeirarchyCreator(List<FolderHierarchyDto> result)
        {
            List<FolderHierarchyDto> hierarchicalItems;
            Action<FolderHierarchyDto> SetChildren = null;
            SetChildren = parent =>
            {
                if (parent.HierarchyType == 1)
                {
                    parent.Children = result
                        .Where(childItem => childItem.ParentId == parent.Id)
                        .OrderBy(childItem => childItem.HierarchyType)
                        .ThenBy(childItem => childItem.Name)
                        .ToList();

                    //Recursively call the SetChildren method for each child.
                    parent.Children
                        .ForEach(SetChildren);
                }
            };

            // Initialize the hierarchical list to root level items
            hierarchicalItems = result
                .Where(rootItem => rootItem.ParentId is null)
                .OrderBy(rootItem => rootItem.Name)
                .ToList();

            //Call the SetChildren method to set the children on each root level item.
            hierarchicalItems.ForEach(SetChildren);
            return hierarchicalItems;
        }

        public List<FolderHierarchyDto> GetAllParentFoldersByFolderName(string folderName)
        {
            var result = new List<FolderHierarchyDto>();
            //List<FolderHierarchyDto> hierarchicalItems = new List<FolderHierarchyDto>();
            //try
            //{
            //    var folderHierarchies = _folderHierarchyService.GetAll().ToList();
            //    if (folderHierarchies != null)
            //    {
            //        result = _mapper.Map<List<FolderHierarchyDto>>(folderHierarchies);
            //    }

            //    var folderHierarchiesFromMaterial = _folderHierarchyService.GetAllParentFoldersByFolderName(folderName).ToList();
            //    if (folderHierarchiesFromMaterial != null)
            //    {
            //        result.AddRange(_mapper.Map<List<FolderHierarchyDto>>(folderHierarchiesFromMaterial));
            //    }

            //    Action<FolderHierarchyDto> SetChildren = null;
            //    SetChildren = parent =>
            //    {
            //        if (parent.HierarchyType == 1)
            //        {
            //            parent.Children = result
            //                .Where(childItem => childItem.ParentId == parent.Id)
            //                .ToList();

            //            //Recursively call the SetChildren method for each child.
            //            parent.Children
            //                .ForEach(SetChildren);
            //        }
            //    };

            //    // Initialize the hierarchical list to root level items
            //    hierarchicalItems = result
            //        .Where(rootItem => rootItem.ParentId is null)
            //        .ToList();

            //    //Call the SetChildren method to set the children on each root level item.
            //    hierarchicalItems.ForEach(SetChildren);

            //    //foreach (var item in hierarchicalItems)
            //    //{
            //    //    if (item.Icon.Trim().Length == 0)
            //    //    {
            //    //        item.HierarchyType = 2;
            //    //    }
            //    //}
            //    var result = new List<FolderHierarchyDto>();
            try
            {
                var folderHierarchies = _folderHierarchyService.GetAllParentFoldersByFolderName(folderName);
                result = _mapper.Map<List<FolderHierarchyDto>>(folderHierarchies);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAllParentFoldersByFolderName error");

            }
            return result;
        }

        public List<FolderHierarchyDto> GetAllParentFoldersByFolderId(int folderHierarchyId)
        {
            var result = new List<FolderHierarchyDto>();
            try
            {
                var folderHierarchies = _folderHierarchyService.GetAllParentFoldersByFolderId(folderHierarchyId);
                result = _mapper.Map<List<FolderHierarchyDto>>(folderHierarchies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAllParentFoldersByFolderId error");

            }
            return result;
        }
    }
}
