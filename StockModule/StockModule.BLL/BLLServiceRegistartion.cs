using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockModule.BLL.Interfaces;
using StockModule.BLL.Logic;
using StockModule.BLL.StockSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.BLL
{
    public static class BLLServiceRegistartion
    {
        public static IServiceCollection ConfigureBLLServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMaterialLogic, MaterialLogic>();
            services.AddScoped<IStockSettingsLogic, StockSettingsLogic>();
            services.AddScoped<IStockAccountingLogic, StockAccountingLogic>();
            services.AddScoped<IView_StockSettingsMaterial_FolderHierarchyLogic, View_StockSettingsMaterial_FolderHierarchyLogic>();
            services.AddScoped<IFolderHierarchyLogic, FolderHierarchyLogic>();
            return services;
        }
    }
}
