using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WindowSellerWASM.Server.Controllers
{
    public class SubElementsController : Controller
    {
        // GET: SubElementsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: SubElementsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SubElementsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SubElementsController/Create
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

        // GET: SubElementsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SubElementsController/Edit/5
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

        // GET: SubElementsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SubElementsController/Delete/5
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
