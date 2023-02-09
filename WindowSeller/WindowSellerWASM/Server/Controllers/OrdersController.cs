using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using WindowSellerWASM.BLL.DTOs.Order;
using WindowSellerWASM.BLL.Features.Orders.Handlers.Queries;
using WindowSellerWASM.BLL.Features.Orders.Requests.Commands;
using WindowSellerWASM.BLL.Features.Orders.Requests.Queries;
using WindowSellerWASM.BLL.Responses;

namespace WindowSellerWASM.Server.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: OrdersController
        public async Task<ActionResult> GetAllOrders()
        {
            var orders = await _mediator.Send(new GetOrderListRequest());
            return View(orders);
        }

        // GET: OrdersController/Details/5
        public async Task<ActionResult> GetOrderByID(long id)
        {
            var order = await _mediator.Send(new GetOderByIDRequest { orderId = id });
            return View(order);
        }

        // POST: OrdersController/Create
        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] OrderDto orderDto)
        {
            try
            {
                var command = new CreateOrderCommand { OrderDto = orderDto };
                var respone = await _mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // PUT: OrdersController/Edit/5
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] OrderDto orderDto)
        {
            try
            {
                var command = new UpdateOrderCommand { OrderDto = orderDto };
                var respone = await _mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // DELETE: OrdersController/Delete/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                var command = new DeleteOrderCommand { orderId = id };
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
