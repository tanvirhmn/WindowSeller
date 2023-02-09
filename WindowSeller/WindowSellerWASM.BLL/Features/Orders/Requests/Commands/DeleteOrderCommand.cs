using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSellerWASM.BLL.Responses;

namespace WindowSellerWASM.BLL.Features.Orders.Requests.Commands
{
    public class DeleteOrderCommand : IRequest<BaseCommandResponse>
    {
        public long orderId { get; set; }
    }
}
