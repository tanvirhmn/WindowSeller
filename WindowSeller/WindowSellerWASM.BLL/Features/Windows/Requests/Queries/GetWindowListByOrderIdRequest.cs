

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSellerWASM.BLL.DTOs.Window;

namespace WindowSellerWASM.BLL.Features.Windows.Requests.Queries
{
    public class GetWindowListByOrderIdRequest : IRequest<List<WindowDto>>
    {
        public long orderId { get; set; }
    }
}
