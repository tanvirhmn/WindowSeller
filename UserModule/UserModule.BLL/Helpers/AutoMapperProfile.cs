using AutoMapper;
using UserModule.BLL.DTOs;
using UserModule.DAL.Models;

namespace UserModule.BLL.Helpers
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        { 
            CreateMap<Permission, PermissionDto>();
        }
    }
}
