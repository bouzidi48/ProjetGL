using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;
using ProjetNet.Services;
using ProjetNet.Models;

namespace ProjetNet.Controllers
{
    public class ProjetController : Controller
    {
        InterfaceProjetManager projetManager;
        InterfaceUtilisateurManager utilisateurManager;

		public ProjetController(InterfaceProjetManager projetManager, InterfaceUtilisateurManager interfaceUtilisateurManager)
		{
			this.projetManager = projetManager;
			this.utilisateurManager = interfaceUtilisateurManager;
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

		[HttpGet]
		public IActionResult GetAllProject()
		{
            int id = int.Parse(TempData["utilisateurId"].ToString());
			List<Projet> projets = projetManager.GetProjetParDir(id);
			return View(projets);
		}

		[HttpGet]
		public IActionResult SelectionnerChefProjet(int id)
		{
            //int id = int.Parse(TempData["utilisateurId"].ToString());
            TempData["projetId"] = id;
			List<Utilisateur> chefprojets = utilisateurManager.GetByRole("Chef Projet");
			return View(chefprojets);
		}

		[HttpPost]
		public IActionResult AffecterChef(int id)
		{
            //int id = int.Parse(TempData["utilisateurId"].ToString());
            int projetId = int.Parse(TempData["projetId"].ToString());
            Projet projet = projetManager.ConsulterProjet(projetId);
            projet.ChefProjet = (ChefProjet) utilisateurManager.GetById(id);
            projetManager.AffecterChefprojet(projet);
			TempData.Remove("projetId");
			return RedirectToAction(nameof(GetAllProject), "Project");
		}
	}
}
