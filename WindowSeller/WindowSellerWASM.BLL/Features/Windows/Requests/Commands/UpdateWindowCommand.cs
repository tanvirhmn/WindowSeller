
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSellerWASM.BLL.DTOs.Window;
using WindowSellerWASM.BLL.Responses;

namespace WindowSellerWASM.BLL.Features.Window.Requests.Commands
{
    public class UpdateWindowCommand : IRequest<BaseCommandResponse>
    {
        public WindowDto WindowDto { get; set; }

    }
}
