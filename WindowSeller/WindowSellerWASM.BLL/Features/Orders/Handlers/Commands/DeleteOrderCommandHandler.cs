//using AutoMapper;
//using HR.LeaveManagement.Application.Exceptions;
//using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
//using HR.LeaveManagement.Application.Contracts.Persistance;
//using HR.LeaveManagement.Domain;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using HR.LeaveManagement.Persistence.Repositories;

//namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
//{
//    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Unit>
//    {
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly IMapper _mapper;
//        public DeleteOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
//        {
//            this._unitOfWork = unitOfWork;
//            this._mapper = mapper;
//        }
//        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
//        {
//            var leaveAllocation = await _unitOfWork.LeaveAllocationRepository.GetAsync(request.Id);
//            if (leaveAllocation == null) 
//            {
//                throw new NotFoundException(nameof(LeaveAllocation), request.Id);
//            }

//            await _unitOfWork.LeaveAllocationRepository.DeleteAsync(leaveAllocation);
//            await _unitOfWork.Save();

//            return Unit.Value;
//        }
//    }
//}
