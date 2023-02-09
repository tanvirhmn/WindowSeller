using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSellerWASM.BLL.DTOs;
using WindowSellerWASM.BLL.Features.Orders.Requests.Queries;
using WindowSellerWASM.Shared.Persistance;

namespace WindowSellerWASM.BLL.Features.Orders.Handlers.Queries
{
    public class GetOrderByIDHandler : IRequestHandler<GetOderByIDRequest, OrderDto>
    {
        private readonly IOrderRepository _ordcerRepository;
        private readonly IMapper _mapper;

        public GetOrderByIDHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            this._ordcerRepository = orderRepository;
            this._mapper = mapper;
        }

        public async Task<OrderDto> Handle(GetOderByIDRequest request, CancellationToken cancellationToken)
        {
            var orders = await _ordcerRepository.GetAsync(request.orderId);

            return _mapper.Map<OrderDto>(orders);

        }
    }
}
