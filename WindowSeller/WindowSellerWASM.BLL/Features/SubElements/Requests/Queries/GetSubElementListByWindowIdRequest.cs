
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSellerWASM.BLL.DTOs.SubElement;

namespace WindowSellerWASM.BLL.Features.SubElements.Requests.Queries
{
    public class GetSubElementListByWindowIdRequest : IRequest<List<SubElementDto>>
    {
        public long windowId { get; set; }
    }
}
