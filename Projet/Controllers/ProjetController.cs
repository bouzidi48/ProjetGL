using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetNet.Services;

namespace ProjetNet.Controllers
{
    public class ProjetController : Controller
    {
        InterfaceProjetManager projetManager;

		public ProjetController(InterfaceProjetManager projetManager)
		{
			this.projetManager = projetManager;
		}


		// GET: ProjetController1
		public ActionResult Index()
        {
            return View();
        }

        // GET: ProjetController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProjetController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProjetController1/Create
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

        // GET: ProjetController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProjetController1/Edit/5
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

        // GET: ProjetController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProjetController1/Delete/5
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
