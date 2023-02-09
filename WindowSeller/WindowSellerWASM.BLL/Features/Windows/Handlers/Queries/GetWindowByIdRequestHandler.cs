using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSellerWASM.BLL.DTOs;
using WindowSellerWASM.BLL.Features.Orders.Requests.Queries;
using WindowSellerWASM.BLL.Features.Windows.Requests.Queries;
using WindowSellerWASM.Shared.Persistance;

namespace WindowSellerWASM.BLL.Features.Windows.Handlers.Queries
{
    public class GetWindowByIdRequestHandler : IRequestHandler<GetWindowByIdRequest, WindowDto>
    {
        private readonly IWindowRepository _windowRepository;
        private readonly IMapper _mapper;


        public GetWindowByIdRequestHandler(IWindowRepository windowRepository, IMapper mapper)
        {
            _windowRepository = windowRepository;
            _mapper = mapper;
        }
        public async Task<WindowDto> Handle(GetWindowByIdRequest request, CancellationToken cancellationToken)
        {
            var orders = await _windowRepository.GetAsync(request.windowId);

            return _mapper.Map<WindowDto>(orders);

        }
    }
}
