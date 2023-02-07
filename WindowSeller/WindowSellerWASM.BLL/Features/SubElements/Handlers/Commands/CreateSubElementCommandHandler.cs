using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSeller.Domain;
using WindowSellerWASM.BLL.DTOs.SubElement.Validators;
using WindowSellerWASM.BLL.Features.SubElements.Requests.Commands;
using WindowSellerWASM.BLL.Responses;
using WindowSellerWASM.Shared.Persistance;

namespace WindowSellerWASM.BLL.Features.SubElements.Handlers.Commands
{
    public class CreateSubElementCommandHandler : IRequestHandler<CreateSubElementCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateSubElementCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateSubElementCommand request, CancellationToken cancellationToken)
        {

            var response = new BaseCommandResponse();
            var validator = new SubElementDtoValidator(_unitOfWork.SubElementRepository);
            var validationResult = await validator.ValidateAsync(request.SubElementDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(err => err.ErrorMessage).ToList();
            }
            else
            {

                var subElement = _mapper.Map<SubElement>(request.SubElementDto);
                subElement = await _unitOfWork.SubElementRepository.AddAsync(subElement);

                response.Message = "Creation Successful";
                response.Success = true;
                response.Id = subElement.SubElementId;
            }
            return response;
        }
    }
}
