using MediatR;
using Microsoft.Extensions.DependencyInjection;
using WindowsSellerWASM.DAL;
using WindowsSellerWASM.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WindowSellerWASM.Shared.Persistance;

namespace WindowSellerWASM.BLL
{
    public static class BLLServiceRegistartion
    {
        public static IServiceCollection ConfigureBLLServices(this IServiceCollection servicess, IConfiguration configuration)
        {
            servicess.AddDbContext<WindowSellerDdContext>(options => options.UseSqlServer(configuration.GetConnectionString("WindowSellerConnectionString")));

            servicess.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            servicess.AddScoped<IUnitOfWork, UnitOfWork>();

            servicess.AddScoped<IOrderRepository, OrderRepository>();
            servicess.AddScoped<IWindowRepository, WindowRepository>();
            servicess.AddScoped<ISubElementRepository, SubElementRepository>();
            servicess.AddAutoMapper(Assembly.GetExecutingAssembly());
            servicess.AddMediatR(Assembly.GetExecutingAssembly());

            return servicess;
        }
    }
}
