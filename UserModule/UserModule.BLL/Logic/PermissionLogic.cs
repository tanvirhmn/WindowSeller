using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StockModule.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.BLL.DTOs;
using UserModule.BLL.Interfaces;
using UserModule.DAL.Models;
using UserModule.DAL.Services;

namespace UserModule.BLL.Logic
{
    public  class PermissionLogic : IPermissionLogic
    {
        private readonly IPermissionService _permissionService;

        //private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public PermissionLogic(IPermissionService permissionService, IMapper mapper)
        {
            _permissionService = permissionService;
            //_logger = logger;
            _mapper = mapper;
        }

        public async Task<List<PermissionDto>> GetByAzureGroupsAsync(string azureGroups)
        {
            var premissions = await _permissionService.GetByAzureGroupsAsync(azureGroups);

            return _mapper.Map<List<PermissionDto>>(premissions);

        }
    }
}
