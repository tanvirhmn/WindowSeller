using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSellerWASM.BLL.DTOs;

namespace WindowSellerWASM.BLL.Features.SubElements.Requests.Queries
{
    public class GetSubElementByIdRequest : IRequest<SubElementDto>
    {
        public long subElementId { get; set; }
    }
}
