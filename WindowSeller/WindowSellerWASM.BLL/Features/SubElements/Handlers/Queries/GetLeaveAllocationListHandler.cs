//using AutoMapper;
//using HR.LeaveManagement.Application.DTOs.LeaveRequest;
//using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Queries;
//using HR.LeaveManagement.Application.Contracts.Persistance;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using HR.LeaveManagement.Application.Contracts.Identity;
//using Microsoft.AspNetCore.Http;
//using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
//using HR.LeaveManagement.Application.Constants;
//using HR.LeaveManagement.Domain;

//namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Queries
//{
//    public class GetLeaveAllocationListRequestHandler : IRequestHandler<GetOrderListRequest, List<LeaveAllocationDto>>
//    {
//        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
//        private readonly IMapper _mapper;
//        private readonly IHttpContextAccessor _httpContextAccessor;
//        private readonly IUserService _userService;

//        public GetLeaveAllocationListRequestHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper,
//            IHttpContextAccessor httpContextAccessor,
//            IUserService userService)
//        {
//            this._leaveAllocationRepository = leaveAllocationRepository;
//            this._mapper = mapper;
//            this._httpContextAccessor = httpContextAccessor;
//            this._userService = userService;
//        }

//        public async Task<List<LeaveAllocationDto>> Handle(GetOrderListRequest request, CancellationToken cancellationToken)
//        {
//            var leaveAllocationDtos = new List<LeaveAllocationDto>();
//            var leaveAllocations = new List<LeaveAllocation>();

//            if (request.IsLoggedInUser)
//            {
//                var userId = _httpContextAccessor.HttpContext.User.FindFirst(
//                    user => user.Type == CustomClaimTypes.UID)?.Value;
//                leaveAllocations = await _leaveAllocationRepository.GetLeaveAllocationsWithDetailsAsync();

//                var employe = await _userService.GetEmployee(userId);
//                leaveAllocationDtos = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);
//                foreach (var leaveAllocation in leaveAllocationDtos)
//                {
//                    leaveAllocation.Employee = employe;
//                }
//            }
//            else
//            {

//                leaveAllocations = await _leaveAllocationRepository.GetLeaveAllocationsWithDetailsAsync();

//                leaveAllocationDtos = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);

//                foreach (var leaveAllocation in leaveAllocationDtos)
//                {
//                    leaveAllocation.Employee = await _userService.GetEmployee(leaveAllocation.EmployeeId);
//                }
//            }

//            return leaveAllocationDtos;

//        }
//    }
//}
