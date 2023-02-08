using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WindowSellerWASM.BLL.Features.Orders.Handlers.Queries;
using WindowSellerWASM.BLL.Features.Orders.Requests.Queries;

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

        // GET: OrdersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrdersController/Create
        [HttpPost]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrdersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrdersController/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrdersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrdersController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
