using AutoMapper;

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSellerWASM.BLL.DTOs;
using WindowSellerWASM.BLL.Features.SubElements.Requests.Queries;
using WindowSellerWASM.BLL.Features.Windows.Requests.Queries;
using WindowSellerWASM.Shared.Persistance;

namespace WindowSellerWASM.BLL.Features.Windows.Handlers.Queries
{
    public class GetWindowListByOrderIdRequestHandler : IRequestHandler<GetWindowListByOrderIdRequest, List<WindowDto>>
    {
        private readonly IWindowRepository _windowRepository;
        private readonly IMapper _mapper;

        public GetWindowListByOrderIdRequestHandler(IWindowRepository subElementRepository, IMapper mapper)
        {
            this._windowRepository = subElementRepository;
            this._mapper = mapper;
        }

        public async Task<List<WindowDto>> Handle(GetWindowListByOrderIdRequest request, CancellationToken cancellationToken)
        {
            var subElements = await _windowRepository.GetByOrderIdAsync(request.orderId);

            return _mapper.Map<List<WindowDto>>(subElements);

        }
    }
}
