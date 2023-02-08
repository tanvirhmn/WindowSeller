using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSeller.Domain;
using WindowSellerWASM.BLL.DTOs.Order;
using WindowSellerWASM.BLL.DTOs.SubElement;
using WindowSellerWASM.BLL.DTOs.Window;
using WindowSellerWASM.BLL.Features.Orders.Requests.Queries;
using WindowSellerWASM.BLL.Features.SubElements.Requests.Queries;
using WindowSellerWASM.BLL.Features.Windows.Requests.Queries;
using WindowSellerWASM.Shared.Persistance;

namespace WindowSellerWASM.BLL.Features.SubElements.Handlers.Queries
{
    public class GetSubElementByIdRequestHandler : IRequestHandler<GetSubElementByIdRequest, SubElementDto>
    {
        private readonly IOrderRepository _ordcerRepository;
        private readonly IMapper _mapper;

        public GetSubElementByIdRequestHandler(IOrderRepository ordcerRepository, IMapper mapper)
        {
            _ordcerRepository = ordcerRepository;
            _mapper = mapper;
        }

        public Task<SubElementDto> Handle(GetSubElementByIdRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
