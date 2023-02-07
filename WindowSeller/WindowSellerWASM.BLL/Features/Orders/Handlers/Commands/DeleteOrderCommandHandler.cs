using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSeller.Domain;
using WindowSellerWASM.BLL.Exceptions;
using WindowSellerWASM.BLL.Features.Orders.Requests.Commands;
using WindowSellerWASM.Shared.Persistance;

namespace WindowSellerWASM.BLL.Features.Orders.Handlers.Commands
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var leaveAllocation = await _unitOfWork.OrderRepository.GetAsync(request.orderId);
            if (leaveAllocation == null)
            {
                throw new NotFoundException(nameof(Order), request.orderId);
            }

            await _unitOfWork.OrderRepository.DeleteAsync(leaveAllocation);

            return Unit.Value;
        }
    }
}
