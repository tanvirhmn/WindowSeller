
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSellerWASM.BLL.DTOs;
using WindowSellerWASM.BLL.Responses;

namespace WindowSellerWASM.BLL.Features.Windows.Requests.Commands
{
    public class CreateWindowCommand : IRequest<BaseCommandResponse>
    {
        public WindowDto WindowDto { get; set; }
    }
}
