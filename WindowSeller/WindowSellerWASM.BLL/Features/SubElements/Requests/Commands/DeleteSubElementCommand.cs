using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowSellerWASM.BLL.Features.SubElements.Requests.Commands
{
    public class DeleteSubElementCommand : IRequest
    {
        public long subElementId { get; set; }
    }
}
