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
//using HR.LeaveManagement.Application.Responses;
//using HR.LeaveManagement.Application.Contracts.Identity;
//using HR.LeaveManagement.Persistence.Repositories;

//namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
//{
//    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, BaseCommandResponse>
//    {
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly IUserService _userService;
//        private readonly IMapper _mapper;
//        public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IUserService userService,IMapper mapper)
//        {
//            this._unitOfWork = unitOfWork;
//            this._userService = userService;    
//            this._mapper = mapper;  
//        }
//        public async Task<BaseCommandResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
//        {
//            var response = new BaseCommandResponse();   
//            var validator = new CreateLeaveAllocationDtoValidator(_unitOfWork.LeaveTypeRepository);
//            var validationResult = await validator.ValidateAsync(request.LeaveAllocationDto);

//            if (validationResult.IsValid == false)
//            {
//                response.Success = false;
//                response.Message = "Creation Failed";
//                response.Errors = validationResult.Errors.Select(err => err.ErrorMessage).ToList();
//            }
//            else
//            {
//                var leaveType = await _unitOfWork.LeaveTypeRepository.GetAsync(request.LeaveAllocationDto.LeaveTypeId);
//                var employees = await _userService.GetEmployees();
//                var period = DateTime.Now.Year;
//                var allocations = new List<LeaveAllocation>();

//                foreach (var employee in employees)
//                {
//                    if(await _unitOfWork.LeaveAllocationRepository.AllocationExits(employee.Id, leaveType.Id, period))
//                    {
//                        continue;
//                    }
//                    allocations.Add(new LeaveAllocation
//                    {
//                        EmployeeId = employee.Id,
//                        LeaveTypeId = leaveType.Id,
//                        NumberOfDays = leaveType.Deafultdays,
//                        Period = period
//                    });
//                }
                
//                await _unitOfWork.LeaveAllocationRepository.AddAllocations(allocations);
//                await _unitOfWork.Save();
//                //var leaveAllocation = _mapper.Map<LeaveAllocation>(request.LeaveAllocationDto);
//                //leaveAllocation = await _leaveAllocationRpository.AddAsync(leaveAllocation);

//                response.Message = "Creation Successful";
//                response.Success = true;
//            }
//            return response;
//        }
//    }
//}
