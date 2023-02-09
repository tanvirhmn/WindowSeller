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
using WindowSellerWASM.BLL.Features.SubElements.Requests.Queries;

namespace WindowSellerWASM.BLL.Features.Orders.Handlers.Queries
{
    public class GetSubElementRequestHandler : IRequestHandler<GetSubElementListByWindowIdRequest, List<SubElementDto>>
    {
        private readonly ISubElementRepository _subElementRepository;
        private readonly IMapper _mapper;

        public GetSubElementRequestHandler(ISubElementRepository subElementRepository, IMapper mapper)
        {
            this._subElementRepository = subElementRepository;
            this._mapper = mapper;
        }

        public async Task<List<SubElementDto>> Handle(GetSubElementListByWindowIdRequest request, CancellationToken cancellationToken)
        {
            var subElements = await _subElementRepository.GetByWindowIdAsync(request.windowId);

            return _mapper.Map<List<SubElementDto>>(subElements);

        }

    }
}
