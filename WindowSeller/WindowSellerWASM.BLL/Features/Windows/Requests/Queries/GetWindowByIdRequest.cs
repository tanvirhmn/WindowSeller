using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSellerWASM.BLL.DTOs;

namespace WindowSellerWASM.BLL.Features.Windows.Requests.Queries
{
    public class GetWindowByIdRequest : IRequest<WindowDto>
    {
        public long windowId { get; set; }
    }
}
