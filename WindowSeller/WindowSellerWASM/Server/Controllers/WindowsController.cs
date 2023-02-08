using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WindowSellerWASM.Server.Controllers
{
    public class WindowsController : Controller
    {
        // GET: WindowsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: WindowsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WindowsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WindowsController/Create
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

        // GET: WindowsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WindowsController/Edit/5
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

        // GET: WindowsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WindowsController/Delete/5
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
