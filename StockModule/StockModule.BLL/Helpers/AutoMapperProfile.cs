using AutoMapper;
using StockModule.BLL.Logic;
using StockModule.BLL.StockSettings;
using StockModule.DAL.Models;
using StockModule.DAL;
using StockModule.BLL;
using StockModule.BLL.Dto;
using Microsoft.EntityFrameworkCore.Update.Internal;
using StockModule.BLL.SubClasses;
using StockModule.DAL.Migrations;
using StockModule.BLL.Dto.FolderHierarchy;

namespace StockModule.BLL.Helpers
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Stock?, StockListDto?>().ForMember(dest => dest!.Warehouse, opts => opts.MapFrom(src => src!.Warehouse.Name));
            CreateMap<Material?, MaterialStockInfoDto?>()
                .ForMember(dest => dest!.Stocks,
                opts => opts.MapFrom(src => src!.Stocks!.OrderBy(_ => _.WarehouseID).ThenByDescending(_ => _.Length)));

            CreateMap<StockAccountingMovement, StockAccountingMovementDto>()
                .ForMember(dest => dest.Date, opts => opts.MapFrom(src => src!.StockMovement!.DocumentDate))
                .ForMember(dest => dest.MaterialCode, opts => opts.MapFrom(src => src!.StockMovement!.ToStock!.Material.Code))
                .ForMember(dest => dest.MaterialDescription, opts => opts.MapFrom(src => src!.StockMovement!.ToStock!.Material.Description))
                .ForMember(dest => dest.Measure, opts => opts.MapFrom(src => src!.StockMovement!.ToStock!.Material.Type))
                .ForMember(dest => dest.Movement, opts => opts.MapFrom(src => src!.StockMovement!.StockMovementReason!.GetMovementDTO()))
                .ForMember(dest => dest.Type, opts => opts.MapFrom(src => src!.StockMovement!.StockMovementReason!.GetReasonDTO()))
                .ForMember(dest => dest.Quantity, opts => opts.MapFrom(src => src!.StockMovement!.GetQuantityDTO()))
                .ForMember(dest => dest.LastResponseMessage, opts => opts.MapFrom(src => src!.StockAccountingActions!.GetLasMessageDTO()));




            #region StockSettings
            CreateMap<Material, MaterialDto>();
            CreateMap<StockSetting, StockSettingDto>();

            CreateMap<MaterialDto, Material>();
            CreateMap<StockSettingDto, StockSetting>();

            CreateMap<UserPermission, UserPermissionDto>();
            CreateMap<Permission, PermissionDto>();
            CreateMap<Employee, EmployeeDto>();
            #endregion StockSettings

            CreateMap<View_StockSettingsMaterial_FolderHierarchy, StockSettingsMaterial_FolderHierarchyDto>().ReverseMap();

            CreateMap<FolderHierarchy, FolderHierarchyDto>()
                .ForMember(flh => flh.HierarchyType, opts => opts.MapFrom(src => (int)EnumHierarchyType.Folder));
            CreateMap<FolderHierarchyDto, FolderHierarchy>()
                .ForSourceMember(flh => flh.HierarchyType, opts => opts.DoNotValidate());

            CreateMap<View_StockSettingsMaterial_FolderHierarchy, FolderHierarchyDto>()
                .ForMember(flh => flh.HierarchyType, opts => opts.MapFrom(src => (int)EnumHierarchyType.Material))
                .ForMember(flh => flh.ParentId, opts => opts.MapFrom(src => src.FolderHierarchyId))
                .ForMember(flh => flh.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(flh => flh.Name, opts => opts.MapFrom(src => src.Code + " " + src.Description))
                .ForMember(flh => flh.Icon, opts => opts.MapFrom(src => ""));
        }
    }
}
