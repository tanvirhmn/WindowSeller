using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WindowSellerWASM.BLL.DTOs;
using WindowSellerWASM.BLL.Features.Orders.Requests.Commands;
using WindowSellerWASM.BLL.Features.Orders.Requests.Queries;
using WindowSellerWASM.BLL.Features.SubElements.Requests.Commands;
using WindowSellerWASM.BLL.Features.SubElements.Requests.Queries;
using WindowSellerWASM.BLL.Responses;

namespace WindowSellerWASM.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubElementsController : Controller
    {
        private readonly IMediator _mediator;

        public SubElementsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //GET: SubElementsController/5
        [HttpGet]
        public async Task<ActionResult> Index(long id)
        {
            var subElements = await _mediator.Send(new GetSubElementListByWindowIdRequest { windowId = id });
            return Ok(subElements);
        }

        // GET: SubElementsController/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Details(long id)
        {
            var subElement = await _mediator.Send(new GetSubElementByIdRequest { subElementId = id });
            return Ok(subElement);
        }


        // POST: SubElementsController/Create
        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Create(SubElementDto subElementDto)
        {
            var command = new CreateSubElementCommand { SubElementDto = subElementDto };
            var respone = await _mediator.Send(command);

            return Ok(respone);
        }


        // PUT: SubElementsController/Edit/
        [HttpPut]
        public async Task<ActionResult<BaseCommandResponse>> Edit(SubElementDto subElementDto)
        {
            var command = new UpdateSubElementCommand { SubElementDto = subElementDto };
            var respone = await _mediator.Send(command);
            return Ok(respone);
        }

        // DELETE: SubElementsController/Delete/5
        [HttpDelete]
        public async Task<ActionResult<BaseCommandResponse>> Delete(long id)
        {
            var command = new DeleteSubElementCommand { subElementId = id };
            var respone = await _mediator.Send(command);
            return Ok(respone);
        }
    }

}