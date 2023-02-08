using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSellerWASM.BLL.DTOs.Order;
using WindowSellerWASM.BLL.DTOs.Window;
using WindowSellerWASM.BLL.Features.Orders.Requests.Queries;
using WindowSellerWASM.BLL.Features.Windows.Requests.Queries;
using WindowSellerWASM.Shared.Persistance;

namespace WindowSellerWASM.BLL.Features.Windows.Handlers.Queries
{
    public class GetWindowByIdRequestHandler : IRequestHandler<GetWindowByIdRequest, WindowDto>
    {
        private readonly IOrderRepository _ordcerRepository;
        private readonly IMapper _mapper;


        public GetWindowByIdRequestHandler(IOrderRepository ordcerRepository, IMapper mapper)
        {
            _ordcerRepository = ordcerRepository;
            _mapper = mapper;
        }
        public async Task<WindowDto> Handle(GetWindowByIdRequest request, CancellationToken cancellationToken)
        {
            var orders = await _ordcerRepository.GetAsync(request.windowId);

            return _mapper.Map<WindowDto>(orders);

        }
    }
}
