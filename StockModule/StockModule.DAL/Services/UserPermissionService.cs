using Microsoft.EntityFrameworkCore;
using StockModule.DAL.DBContexts;
using StockModule.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.DAL.Services
{
    public class UserPermissionService : EntityService<UserPermission>, IUserPermissionService
    {
        public UserPermissionService(IntusPrefContext context) : base(context)
        {
            Set = context.Set<UserPermission>();
        }

        public List<UserPermission> GetPermissions()
        {
            return Set.Include(i => i.Permission).Include(i => i.Employee).ToList();
        }
    }
}
