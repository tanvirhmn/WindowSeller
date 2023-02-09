using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSeller.Domain;
using WindowSellerWASM.BLL.Exceptions;
using WindowSellerWASM.BLL.Features.SubElements.Requests.Commands;
using WindowSellerWASM.BLL.Responses;
using WindowSellerWASM.Shared.Persistance;

namespace WindowSellerWASM.BLL.Features.SubElements.Handlers.Commands
{
    public class DeleteSubElementCommandHandler : IRequestHandler<DeleteSubElementCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteSubElementCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(DeleteSubElementCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var subElement = await _unitOfWork.SubElementRepository.GetAsync(request.subElementId);
            if (subElement == null)
            {
                throw new NotFoundException(nameof(SubElement), request.subElementId);
            }

            await _unitOfWork.SubElementRepository.DeleteAsync(subElement);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Sucessful";
            response.Id = request.subElementId;

            return response;
        }
    }
}
