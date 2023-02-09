using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSeller.Domain;
using WindowSellerWASM.BLL.DTOs;
using WindowSellerWASM.BLL.Features.Orders.Requests.Queries;
using WindowSellerWASM.BLL.Features.SubElements.Requests.Queries;
using WindowSellerWASM.BLL.Features.Windows.Requests.Queries;
using WindowSellerWASM.Shared.Persistance;

namespace WindowSellerWASM.BLL.Features.SubElements.Handlers.Queries
{
    public class GetSubElementByIdRequestHandler : IRequestHandler<GetSubElementByIdRequest, SubElementDto>
    {
        private readonly ISubElementRepository _subElementRepository;
        private readonly IMapper _mapper;

        public GetSubElementByIdRequestHandler(ISubElementRepository subElementRepository, IMapper mapper)
        {
            _subElementRepository = subElementRepository;
            _mapper = mapper;
        }

        public async Task<SubElementDto> Handle(GetSubElementByIdRequest request, CancellationToken cancellationToken)
        {
            var subElement = await _subElementRepository.GetAsync(request.subElementId);

            return _mapper.Map<SubElementDto>(subElement);
        }
    }
}
