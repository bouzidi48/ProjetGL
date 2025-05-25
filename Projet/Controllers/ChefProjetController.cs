using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjetNet.Controllers
{
    public class ChefProjetController : Controller
    {
        // GET: ChefProjetController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ChefProjetController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ChefProjetController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChefProjetController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: ChefProjetController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ChefProjetController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: ChefProjetController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ChefProjetController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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
