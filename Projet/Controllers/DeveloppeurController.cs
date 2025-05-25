using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetNet.Models;
using ProjetNet.Services;

namespace ProjetNet.Controllers
{
	public class DeveloppeurController : Controller
	{
		InterfaceProjetManager projetManager;
		InterfaceUtilisateurManager utilisateurManager;
		InterfaceNotificationManager notificationManager;
		InterfaceServiceManager serviceManager;
		InterfaceDeveloppeurManager developpeurManager;
		InterfaceTacheManager tacheManager;

		public DeveloppeurController(InterfaceProjetManager projetManager, InterfaceUtilisateurManager interfaceUtilisateurManager, InterfaceNotificationManager notificationManager, InterfaceServiceManager serviceManager, InterfaceDeveloppeurManager developpeurManager, InterfaceTacheManager tacheManager)
		{
			this.projetManager = projetManager;
			this.utilisateurManager = interfaceUtilisateurManager;
			this.notificationManager = notificationManager;
			this.serviceManager = serviceManager;
			this.developpeurManager = developpeurManager;
			this.tacheManager = tacheManager;
		}
		// GET: DeveloppeurController
		public ActionResult Index()
		{
			return View();
		}

		// GET: DeveloppeurController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: DeveloppeurController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: DeveloppeurController/Create
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

		// GET: DeveloppeurController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: DeveloppeurController/Edit/5
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

		// GET: DeveloppeurController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: DeveloppeurController/Delete/5
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
		public IActionResult Afficherdeveloppeurs(int id)
		{

			TempData["projetId"] = id;
			HttpContext.Session.SetInt32("projetId", id);
			Projet projet = projetManager.ConsulterProjet(id);
			List<Developpeur> developpeurs = developpeurManager.GetDeveParProjet(projet);
			return View(developpeurs);
		}

		[HttpGet]
		public IActionResult AfficherdeveloppeursChefProjet(int id)
		{

			TempData["projetId"] = id;
			HttpContext.Session.SetInt32("projetId", id);
			Projet projet = projetManager.ConsulterProjet(id);
			List<Developpeur> developpeurs = developpeurManager.GetDeveParProjet(projet);
			return View(developpeurs);
		}

		[HttpGet]
		public IActionResult SelectionnerdeveloppeursChefProjet(int id)
		{
			int? projetId = HttpContext.Session.GetInt32("projetId");
			HttpContext.Session.SetInt32("projetId", id);
			Projet p = projetManager.ConsulterProjet(id);
			List<Developpeur> developpeurs = null;
			developpeurs = developpeurManager.ListerDeveParTech(p.Technologies);
			ViewBag.ProjetId = projetId;
			return View(developpeurs);
		}

		[HttpPost]
		public IActionResult SelectionnerdeveloppeursChefProjet()
		{

			// Récupérer le projet depuis la session
			int? projetId = HttpContext.Session.GetInt32("projetId");
			if (projetId == null)
			{
				
				// Si le projet n'est pas en session, redirection avec erreur
				TempData["Erreur"] = "Aucun projet sélectionné.";
				return RedirectToAction("ListerProjetChefProjet", "Projet");
			}

			Projet projet = projetManager.ConsulterProjet((int)projetId);

			// Récupérer tous les IDs cochés dans le formulaire
			var ids = Request.Form["DeveloppeurId"];
			List<Developpeur> developpeurs = new List<Developpeur>();

			foreach (var idStr in ids)
			{
				if (int.TryParse(idStr, out int id))
				{
					var dev = developpeurManager.GetDeveloppeur(id);
					if (dev != null)
					{
						developpeurs.Add(dev);
					}
				}
			}
			
			if (projet != null)
			{
				foreach (var item in developpeurs)
				{
					item.Projet = projet;
					developpeurManager.affecterProjet(item);
					Console.WriteLine("vsfdgf");
				}

			}
			TempData.Remove("projetId");
			HttpContext.Session.Remove("projetId");
			return RedirectToAction("ListerProjetChefProjet", "Projet");
		}


		[HttpGet]
		public IActionResult ChangerProfil(int id)
		{
			int? utilisateurId = HttpContext.Session.GetInt32("utilisateurId");

			Developpeur dev = developpeurManager.GetDeveloppeur((int)utilisateurId);
			var allTechnologies = Enum.GetValues(typeof(Technologie)).Cast<Technologie>().ToList();
			ViewBag.AllTechnologies = allTechnologies;
			return View(dev);
		}

		[HttpPost]
		public IActionResult EnregistrerProfil(Developpeur developpeur)
		{

			// Récupérer le projet depuis la session
			int? utilisateurId = HttpContext.Session.GetInt32("utilisateurId");
			if (utilisateurId == null)
			{

				
				return RedirectToAction("ChangerProfil", "Developpeur");
			}

			Developpeur dev = developpeurManager.GetDeveloppeur((int)utilisateurId);
			dev.Nom = developpeur.Nom;
			dev.Prenom = developpeur.Prenom;
			dev.Email = developpeur.Email;
			dev.Technologies = developpeur.Technologies;
			developpeurManager.ChagerProfile(dev);
		

			
			return RedirectToAction("Index", "Developpeur");
		}
	}
}
