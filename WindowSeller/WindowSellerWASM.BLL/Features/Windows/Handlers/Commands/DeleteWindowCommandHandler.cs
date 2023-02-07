using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSeller.Domain;
using WindowSellerWASM.BLL.Exceptions;
using WindowSellerWASM.BLL.Features.SubElements.Requests.Commands;
using WindowSellerWASM.BLL.Features.Windows.Requests.Commands;
using WindowSellerWASM.Shared.Persistance;

namespace WindowSellerWASM.BLL.Features.Windows.Handlers.Commands
{
    public class DeleteWindowCommandHandler : IRequestHandler<DeleteWindowCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteWindowCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<Unit> Handle(DeleteWindowCommand request, CancellationToken cancellationToken)
        {
            var window = await _unitOfWork.WindowRepository.GetAsync(request.windowId);
            if (window == null)
            {
                throw new NotFoundException(nameof(Window), request.windowId);
            }

            await _unitOfWork.WindowRepository.DeleteAsync(window);

            return Unit.Value;
        }
    }
}
