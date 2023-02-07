using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSeller.Domain;
using WindowSellerWASM.BLL.DTOs.Order;
using WindowSellerWASM.BLL.DTOs.SubElement;
using WindowSellerWASM.BLL.DTOs.Window;

namespace WindowSellerWASM.BLL.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Order
            CreateMap<Order, OrderDto>().ReverseMap();
            #endregion

            #region Window
            CreateMap<Window, WindowDto>().ReverseMap();
            #endregion

            #region Subelement
            CreateMap<SubElement, SubElementDto>().ReverseMap();
            #endregion
        }
    }
}
