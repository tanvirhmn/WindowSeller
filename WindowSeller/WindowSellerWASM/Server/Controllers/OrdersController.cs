using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using WindowSellerWASM.BLL.DTOs;
using WindowSellerWASM.BLL.Features.Orders.Handlers.Queries;
using WindowSellerWASM.BLL.Features.Orders.Requests.Commands;
using WindowSellerWASM.BLL.Features.Orders.Requests.Queries;
using WindowSellerWASM.BLL.Responses;

namespace WindowSellerWASM.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : Controller
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: OrdersController
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var orders = await _mediator.Send(new GetOrderListRequest());
            return Ok(orders);
        }

        // GET: OrdersController/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Details(long id)
        {
            var order = await _mediator.Send(new GetOderByIDRequest { orderId = id });
            return Ok(order);
        }

        // POST: OrdersController/Create
        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Create(OrderDto orderDto)
        {
            var command = new CreateOrderCommand { OrderDto = orderDto };
            var respone = await _mediator.Send(command);


            return Ok(respone);
        }

        // PUT: OrdersController/Edit/
        [HttpPut]
        public async Task<ActionResult<BaseCommandResponse>> Edit(OrderDto orderDto)
        {

            var command = new UpdateOrderCommand { OrderDto = orderDto };
            var respone = await _mediator.Send(command);
            return Ok(respone);
        }


        // DELETE: OrdersController/Delete/5
        [HttpDelete]
        public async Task<ActionResult<BaseCommandResponse>> Delete(long id)
        {
            var command = new DeleteOrderCommand { orderId = id };
            var respone = await _mediator.Send(command);
            return Ok(respone);
        }

    }
}
