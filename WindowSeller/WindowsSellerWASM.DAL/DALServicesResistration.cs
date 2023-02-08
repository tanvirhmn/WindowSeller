using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSellerWASM.Shared.Persistance;
using WindowsSellerWASM.DAL.Repositories;

namespace WindowsSellerWASM.DAL
{
    public static class DALServicesResistration
    {
        public static IServiceCollection ConfigureDALServices(this IServiceCollection servicess, IConfiguration configuration)
        {
            servicess.AddDbContext<WindowSellerDdContext>(options => options.UseSqlServer(configuration.GetConnectionString("WindowSellerConnectionString")));

            servicess.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            servicess.AddScoped<IUnitOfWork, UnitOfWork>();

            servicess.AddScoped<IOrderRepository, OrderRepository>();
            servicess.AddScoped<IWindowRepository, WindowRepository>();
            servicess.AddScoped<ISubElementRepository, SubElementRepository>();

            return servicess;
        }
    }
}
