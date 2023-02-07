using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowSellerWASM.BLL.Features.Orders.Requests.Commands
{
    public class DeleteOrderCommand : IRequest
    {
        public long orderId { get; set; }
    }
}
