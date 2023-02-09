
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSellerWASM.BLL.DTOs;

namespace WindowSellerWASM.BLL.Features.Orders.Requests.Queries
{
    public class GetOrderListRequest : IRequest<List<OrderDto>>
    {
    }
}
