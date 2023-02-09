using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WindowSellerWASM.BLL.DTOs.Order;
using WindowSellerWASM.BLL.DTOs.SubElement;
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
            return View(subElements);
        }

        // GET: SubElementsController/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Details(long id)
        {
            var subElement = await _mediator.Send(new GetSubElementByIdRequest { subElementId = id });
            return View(subElement);
        }


        // POST: SubElementsController/Create
        [HttpPost]
        public async Task<ActionResult> Create(SubElementDto subElementDto)
        {
            try
            {
                var command = new CreateSubElementCommand { SubElementDto = subElementDto };
                var respone = await _mediator.Send(command);
                if (respone.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                if (respone.Errors != null)
                {
                    respone.Errors.ForEach(error => ModelState.AddModelError("", error));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(subElementDto);
        }


        // PUT: SubElementsController/Edit/
        [HttpPut]
        public async Task<ActionResult> Edit(SubElementDto subElementDto)
        {
            try
            {
                var command = new UpdateSubElementCommand { SubElementDto = subElementDto };
                var respone = await _mediator.Send(command);
                if (respone.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                if (respone.Errors != null)
                {
                    respone.Errors.ForEach(error => ModelState.AddModelError("", error));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(subElementDto);
        }

        // DELETE: SubElementsController/Delete/5
        [HttpDelete]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                var command = new DeleteSubElementCommand { subElementId = id };
                var respone = await _mediator.Send(command);
                if (respone.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                if (respone.Errors != null)
                {
                    respone.Errors.ForEach(error => ModelState.AddModelError("", error));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return BadRequest();
        }
    }

}