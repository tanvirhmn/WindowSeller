//using AutoMapper;
//using HR.LeaveManagement.Application.DTOs.LeaveAllocation.Validators;
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
//    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Unit>
//    {
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly IMapper _mapper;

//        public UpdateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
//        {
//            this._mapper = mapper;
//            this._unitOfWork = unitOfWork;
//        }

//        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
//        {
//            var validator = new UpdateLeaveAllocationDtoValidator(_unitOfWork.LeaveTypeRepository);
//            var validationResult = await validator.ValidateAsync(request.LeaveAllocationDto);

//            if (validationResult.IsValid == false)
//            {
//                throw new ValidationException(validationResult);
//            }

//            var leaveAllocation = await _unitOfWork.LeaveAllocationRepository.GetAsync(request.LeaveAllocationDto.Id);

//            if (leaveAllocation is null)
//            {
//                throw new NotFoundException(nameof(LeaveAllocation), request.LeaveAllocationDto.Id);
//            }

//            _mapper.Map(request.LeaveAllocationDto, leaveAllocation);

//            await _unitOfWork.LeaveAllocationRepository.UpdateAsync(leaveAllocation);
//            await _unitOfWork.Save();

//            return Unit.Value;
//        }
//    }
//}
