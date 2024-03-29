﻿using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSeller.Domain;
using WindowSellerWASM.BLL.Exceptions;
using WindowSellerWASM.BLL.Features.Orders.Requests.Commands;
using WindowSellerWASM.BLL.Responses;
using WindowSellerWASM.Shared.Persistance;

namespace WindowSellerWASM.BLL.Features.Orders.Handlers.Commands
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var order = await _unitOfWork.OrderRepository.GetAsync(request.orderId);
            if (order == null)
            {
                throw new NotFoundException(nameof(Order), request.orderId);
            }
            await _unitOfWork.OrderRepository.DeleteAsync(order);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Sucessful";
            response.Id = request.orderId;

            return response;
        }
    }
}
