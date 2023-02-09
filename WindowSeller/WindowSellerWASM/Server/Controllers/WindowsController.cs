using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WindowSeller.Domain;
using WindowSellerWASM.BLL.DTOs;
using WindowSellerWASM.BLL.Features.SubElements.Requests.Commands;
using WindowSellerWASM.BLL.Features.SubElements.Requests.Queries;
using WindowSellerWASM.BLL.Features.Windows.Requests.Commands;
using WindowSellerWASM.BLL.Features.Windows.Requests.Queries;
using WindowSellerWASM.BLL.Responses;

namespace WindowSellerWASM.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WindowsController : Controller
    {
        private readonly IMediator _mediator;

        public WindowsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: WindowsController/5
        [HttpGet]
        public async Task<ActionResult> Index(long id)
        {
            var window = await _mediator.Send(new GetWindowListByOrderIdRequest { orderId = id });
            return Ok(window);
        }

        // GET: WindowsController/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Details(long id)
        {
            var window = await _mediator.Send(new GetWindowByIdRequest { windowId = id });
            return Ok(window);
        }

        // POST: WindowsController/Create
        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Create(WindowDto windowDto)
        {

            var command = new CreateWindowCommand { WindowDto = windowDto };
            var respone = await _mediator.Send(command);
            return Ok(respone);
        }


        // PUT: WindowsController/Edit/5
        [HttpPut]
        public async Task<ActionResult<BaseCommandResponse>> Edit(WindowDto windowDto)
        {
            var command = new UpdateWindowCommand { WindowDto = windowDto };
            var respone = await _mediator.Send(command);

            return Ok(respone);
        }

        // DELETE: WindowsController/Delete/5
        [HttpDelete]
        public async Task<ActionResult<BaseCommandResponse>> Delete(long id)
        {
            var command = new DeleteWindowCommand { windowId = id };
            var respone = await _mediator.Send(command);
            return Ok(respone);
        }
    }
}
