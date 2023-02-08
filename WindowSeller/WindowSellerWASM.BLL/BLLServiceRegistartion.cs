using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WindowSellerWASM.BLL
{
    public static class BLLServiceRegistartion
    {
        public static IServiceCollection ConfigureBLLServices(this IServiceCollection servicess)
        {
            servicess.AddAutoMapper(Assembly.GetExecutingAssembly());
            servicess.AddMediatR(Assembly.GetExecutingAssembly());

            return servicess;
        }
    }
}
