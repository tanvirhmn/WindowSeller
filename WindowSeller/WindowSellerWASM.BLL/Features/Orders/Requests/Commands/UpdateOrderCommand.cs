
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSellerWASM.BLL.DTOs.Order;
using WindowSellerWASM.BLL.Responses;

namespace WindowSellerWASM.BLL.Features.Orders.Requests.Commands
{
    public class UpdateOrderCommand : IRequest<BaseCommandResponse>
    {
        //public UpdateLeaveAllocationDto UpdateLeaveAllocationDto { get; set; }
        public OrderDto OrderDto { get; set; }

    }
}
