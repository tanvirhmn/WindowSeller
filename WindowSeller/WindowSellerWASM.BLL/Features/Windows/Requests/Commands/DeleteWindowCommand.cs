using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSellerWASM.BLL.Responses;

namespace WindowSellerWASM.BLL.Features.Windows.Requests.Commands
{
    public class DeleteWindowCommand : IRequest<BaseCommandResponse>
    {
        public long windowId { get; set; }
    }
}
