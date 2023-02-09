using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSeller.Domain;
using WindowSellerWASM.BLL.DTOs.SubElement.Validators;
using WindowSellerWASM.BLL.Exceptions;
using WindowSellerWASM.BLL.Features.SubElements.Requests.Commands;
using WindowSellerWASM.BLL.Responses;
using WindowSellerWASM.Shared.Persistance;

namespace WindowSellerWASM.BLL.Features.SubElements.Handlers.Commands
{
    public class UpdateSubElementCommandHandler : IRequestHandler<UpdateSubElementCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateSubElementCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
        }

        public async Task<BaseCommandResponse> Handle(UpdateSubElementCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var validator = new SubElementDtoValidator(_unitOfWork.SubElementRepository);
            var validationResult = await validator.ValidateAsync(request.SubElementDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Update Failed";
                response.Errors = validationResult.Errors.Select(err => err.ErrorMessage).ToList();
            }
            else
            {
                var subElement = await _unitOfWork.SubElementRepository.GetAsync(request.SubElementDto.SubElementId);

                if (subElement is null)
                {
                    throw new NotFoundException(nameof(Order), request.SubElementDto.SubElementId);
                }

                _mapper.Map(request.SubElementDto, subElement);

                await _unitOfWork.SubElementRepository.UpdateAsync(subElement);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Sucessful";
                response.Id = subElement.SubElementId;
            }
            return response;
        }
    }
}
