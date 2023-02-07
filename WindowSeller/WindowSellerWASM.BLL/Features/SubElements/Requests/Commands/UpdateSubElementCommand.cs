
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSellerWASM.BLL.DTOs.SubElement;
using WindowSellerWASM.BLL.Responses;

namespace WindowSellerWASM.BLL.Features.SubElements.Requests.Commands
{
    public class UpdateSubElementCommand : IRequest<BaseCommandResponse>
    {
        public SubElementDto SubElementDto { get; set; }

    }
}
