using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSeller.Domain;
using WindowSellerWASM.BLL.DTOs.Order.Validators;
using WindowSellerWASM.BLL.Exceptions;
using WindowSellerWASM.BLL.Features.Orders.Requests.Commands;
using WindowSellerWASM.BLL.Responses;
using WindowSellerWASM.Shared.Persistance;

namespace WindowSellerWASM.BLL.Features.Orders.Handlers.Commands
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
        }

        public async Task<BaseCommandResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var validator = new OrderDtoValidator(_unitOfWork.OrderRepository);
            var validationResult = await validator.ValidateAsync(request.OrderDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Update Failed";
                response.Errors = validationResult.Errors.Select(err => err.ErrorMessage).ToList();
            }
            else
            {
                var order = await _unitOfWork.OrderRepository.GetAsync(request.OrderDto.OrderId);

                if (order is null)
                {
                    throw new NotFoundException(nameof(Order), request.OrderDto.OrderId);
                }

                _mapper.Map(request.OrderDto, order);

                await _unitOfWork.OrderRepository.UpdateAsync(order);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Sucessful";
                response.Id = order.OrderId;
            }
            return response;
        }
    }
}
