using StockModule.DAL.Models;
using StockModule.DAL.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockModule.DAL.DBContexts;

namespace StockModule.DAL.Services
{
    public class PermissionService : EntityService<Permission>, IPermissionService
    {
        public PermissionService(IntusPrefContext context) : base(context)
        {
            Set = context.Set<Permission>();
        }
    }
}
