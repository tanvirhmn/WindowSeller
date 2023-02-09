using AutoMapper;
using MediatR;
using WindowSeller.Domain;
using WindowSellerWASM.BLL.DTOs.Window.Validators;
using WindowSellerWASM.BLL.Features.Windows.Requests.Commands;
using WindowSellerWASM.BLL.Responses;
using WindowSellerWASM.Shared.Persistance;

namespace WindowSellerWASM.BLL.Features.Windows.Handlers.Commands
{
    public class CreateWindowCommandHandler : IRequestHandler<CreateWindowCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateWindowCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateWindowCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new WindowDtoValidator(_unitOfWork.WindowRepository);
            var validationResult = await validator.ValidateAsync(request.WindowDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(err => err.ErrorMessage).ToList();
            }
            else
            {

                var window = _mapper.Map<Window>(request.WindowDto);
                window = await _unitOfWork.WindowRepository.AddAsync(window);
                await _unitOfWork.Save();

                response.Message = "Creation Successful";
                response.Success = true;
                response.Id = window.WindowId;
            }
            return response;
        }
    }
}
