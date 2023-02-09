using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSellerWASM.Shared.Persistance;
using WindowSellerWASM.BLL.Responses;
using WindowSellerWASM.BLL.Features.Orders.Requests.Commands;
using WindowSellerWASM.BLL.DTOs.Order.Validators;
using WindowSeller.Domain;

namespace WindowSellerWASM.BLL.Features.Orders.Handlers.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new OrderDtoValidator(_unitOfWork.OrderRepository);
            var validationResult = await validator.ValidateAsync(request.OrderDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(err => err.ErrorMessage).ToList();
            }
            else
            {

                var order = _mapper.Map<Order>(request.OrderDto);
                order = await _unitOfWork.OrderRepository.AddAsync(order);
                await _unitOfWork.Save();

                response.Message = "Creation Successful";
                response.Success = true;
                response.Id = order.OrderId;
            }
            return response;
        }
    }
}
