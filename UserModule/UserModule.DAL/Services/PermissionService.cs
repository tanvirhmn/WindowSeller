using UserModule.DAL.Models;
using UserModule.DAL.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.DAL;

namespace StockModule.DAL.Services
{
    public class PermissionService : EntityService<Permission>, IPermissionService
    {
        private readonly UserModuleDdContext _context;
        public PermissionService(UserModuleDdContext context) : base(context)
        {
            _context = context; 
            Set = _context.Set<Permission>();
        }

        public async Task<List<Permission>> GetByAzureGroupsAsync(string azureGroups)
        {
            var premissions = await _context.Permissions
                .Where(perm=> azureGroups.Contains(perm.AzureGroup))
                .ToListAsync();

            return premissions; 
        }
    }
}
