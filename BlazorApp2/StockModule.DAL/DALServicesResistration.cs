using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockModule.DAL.DBContexts;
using StockModule.DAL.Services;
using StockModule.DAL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.DAL
{
    public static class DALServicesResistration
    {
        public static IServiceCollection ConfigureDALServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region DBContext
            SqlConnectionStringBuilder sqlbuilder = new SqlConnectionStringBuilder((configuration.GetConnectionString("intusPref")));
            SqlConnection dbCon = new SqlConnection(sqlbuilder.ConnectionString);
            services.AddDbContext<IntusPrefContext>(options => options.UseSqlServer(dbCon,
                            assembly => assembly.MigrationsAssembly(typeof(IntusPrefContext).Assembly.FullName)));
            #endregion

            #region EntityServices
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IExternalMovementService, ExternalMovementService>();
            services.AddScoped<IMaterialService, MaterialService>();
            services.AddScoped<IStockSettingService, StockSettingService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IStockDocumentService, StockDocumentService>();
            services.AddScoped<IStockMovementReasonService, StockMovementReasonService>();
            services.AddScoped<IStockMovementService, StockMovementService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IUserPermissionService, UserPermissionService>();
            services.AddScoped<IWarehouseService, WarehouseService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IStockAccountingMovementService, StockAccountingMovementService>();
            services.AddScoped<IStockAccountingActionService, StockAccountingActionService>();
            services.AddScoped<IFolderHierarchyService, FolderHierarchyService>();
            services.AddScoped<IView_StockSettingsMaterial_FolderHierarchyService, View_StockSettingsMaterial_FolderHierarchyService>();
            services.AddScoped<IFilterViewMasterService, FilterViewMasterService>();
            services.AddScoped<IFilterViewDetailService, FilterViewDetailService>();
            services.AddScoped<IDynamicMaterialColumnPivotedTableService, DynamicMaterialColumnPivotedTableService>();
            services.AddScoped<IMaterialColumnService, MaterialColumnService>();
            services.AddScoped<IMaterialColumnValueService, MaterialColumnValueService>();
            services.AddScoped<IDynamicMaterialCoulmnGridHidingService, DynamicMaterialCoulmnGridHidingService>();
            #endregion EntityServices

            return services;
        }
    }
}
