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
using WindowSellerWASM.Shared;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using System.Data;

namespace WindowSellerWASM.BLL
{
    public static class BLLServiceRegistartion
    {
        public static IServiceCollection ConfigureBLLServices(this IServiceCollection servicess, IConfiguration configuration)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(configuration.GetConnectionString("WindowSellerConnectionString"));
            builder.Password = TrippleDES.Decrypt(TrippleDES.GetBytes(builder.Password.ToString()));

            SqlConnection dbCon = new SqlConnection(builder.ConnectionString);

            //ssqlCred.


            servicess.AddDbContext<WindowSellerDdContext>(options => options.UseSqlServer(dbCon));

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
