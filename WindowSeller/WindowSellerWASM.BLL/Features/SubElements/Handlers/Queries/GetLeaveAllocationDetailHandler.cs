//using AutoMapper;
//using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
//using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Queries;
//using HR.LeaveManagement.Application.Contracts.Persistance;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Queries
//{
//    public class GetOrderDetailHandler : IRequestHandler<GetOrderDetailRequest, LeaveAllocationDto>
//    {
//        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
//        private readonly IMapper _mapper;

//        public GetOrderDetailHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
//        {
//            _leaveAllocationRepository = leaveAllocationRepository;
//            _mapper = mapper;
//        }

//        public async Task<LeaveAllocationDto> Handle(GetOrderDetailRequest request, CancellationToken cancellationToken)
//        {
//            var leaveType = await _leaveAllocationRepository.GetLeaveAllocationWithDetailAsync(request.Id);

//            return _mapper.Map<LeaveAllocationDto>(leaveType);
//        }
//    }
//}
