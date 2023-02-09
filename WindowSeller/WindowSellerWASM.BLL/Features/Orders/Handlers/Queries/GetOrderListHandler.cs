using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WindowSellerWASM.BLL.DTOs;
using WindowSellerWASM.Shared.Persistance;
using WindowSellerWASM.BLL.Features.Orders.Requests.Queries;
using WindowSeller.Domain;

namespace WindowSellerWASM.BLL.Features.Orders.Handlers.Queries
{
    public class GetOrderListRequestHandler : IRequestHandler<GetOrderListRequest, List<OrderDto>>
    {
        private readonly IOrderRepository _ordcerRepository;
        private readonly IMapper _mapper;

        public GetOrderListRequestHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            this._ordcerRepository = orderRepository;
            this._mapper = mapper;
        }

        public async Task<List<OrderDto>> Handle(GetOrderListRequest request, CancellationToken cancellationToken)
        {
            var orders = await _ordcerRepository.GetAllAsync();

            return _mapper.Map<List<OrderDto>>(orders);

        }

    }
}
