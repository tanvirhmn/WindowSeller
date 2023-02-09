using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WindowSeller.Domain;
using WindowSellerWASM.BLL.DTOs.SubElement;
using WindowSellerWASM.BLL.DTOs.Window;
using WindowSellerWASM.BLL.Features.SubElements.Requests.Commands;
using WindowSellerWASM.BLL.Features.SubElements.Requests.Queries;
using WindowSellerWASM.BLL.Features.Windows.Requests.Commands;
using WindowSellerWASM.BLL.Features.Windows.Requests.Queries;

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
            return View(window);
        }

        // GET: WindowsController/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Details(long id)
        {
            var window = await _mediator.Send(new GetWindowByIdRequest { windowId = id });
            return View(window);
        }

        // POST: WindowsController/Create
        [HttpPost]
        public async Task<ActionResult> Create(WindowDto windowDto)
        {
            try
            {
                var command = new CreateWindowCommand { WindowDto = windowDto };
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
            return View(windowDto);
        }


        // PUT: WindowsController/Edit/5
        [HttpPut]
        public async Task<ActionResult> Edit(WindowDto windowDto)
        {
            try
            {
                var command = new UpdateWindowCommand { WindowDto = windowDto };
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
            return View(windowDto);
        }

        // DELETE: WindowsController/Delete/5
        [HttpDelete]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                var command = new DeleteWindowCommand { windowId = id };
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
