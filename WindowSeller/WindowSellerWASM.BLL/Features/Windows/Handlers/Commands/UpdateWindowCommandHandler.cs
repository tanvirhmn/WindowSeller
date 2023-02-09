using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSeller.Domain;
using WindowSellerWASM.BLL.DTOs.SubElement.Validators;
using WindowSellerWASM.BLL.DTOs.Window.Validators;
using WindowSellerWASM.BLL.Exceptions;
using WindowSellerWASM.BLL.Features.Windows.Requests.Commands;
using WindowSellerWASM.BLL.Responses;
using WindowSellerWASM.Shared.Persistance;

namespace WindowSellerWASM.BLL.Features.Windows.Handlers.Commands
{
    public class UpdateWindowCommandHandler : IRequestHandler<UpdateWindowCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateWindowCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
        }

        public async Task<BaseCommandResponse> Handle(UpdateWindowCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var validator = new WindowDtoValidator(_unitOfWork.WindowRepository);
            var validationResult = await validator.ValidateAsync(request.WindowDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Update Failed";
                response.Errors = validationResult.Errors.Select(err => err.ErrorMessage).ToList();
            }
            else
            {
                var window = await _unitOfWork.WindowRepository.GetAsync(request.WindowDto.WindowId);

                if (window is null)
                {
                    throw new NotFoundException(nameof(Window), request.WindowDto.WindowId);
                }

                _mapper.Map(request.WindowDto, window);

                await _unitOfWork.WindowRepository.UpdateAsync(window);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Sucessful";
                response.Id = window.WindowId;
            }
            return response;
        }
    }
}
