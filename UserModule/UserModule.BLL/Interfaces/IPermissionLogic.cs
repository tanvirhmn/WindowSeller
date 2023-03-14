using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.BLL.DTOs;

namespace UserModule.BLL.Interfaces
{
    public interface IPermissionLogic
    {
        public Task<List<PermissionDto>> GetByAzureGroupsAsync(string azureGroups);
    }
}
