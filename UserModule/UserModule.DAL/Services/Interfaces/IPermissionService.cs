using UserModule.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModule.DAL.Services
{
    public interface IPermissionService : IEntityService<Permission>
    {
        Task<List<Permission>> GetByAzureGroupsAsync(string azureGroups);
    }
}
