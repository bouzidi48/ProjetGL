using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using ProjetNet.Bussiness;
using ProjetNet.Models;
using ProjetNet.Services;
using System.Runtime.ConstrainedExecution;

namespace ProjetNet.Controllers
{
    public class ServiceController : Controller
    {
		InterfaceProjetManager projetManager;
		InterfaceUtilisateurManager utilisateurManager;
		InterfaceNotificationManager notificationManager;
		InterfaceServiceManager serviceManager;
		InterfaceDeveloppeurManager developpeurManager;
		InterfaceTacheManager tacheManager;

		public ServiceController(InterfaceProjetManager projetManager, InterfaceUtilisateurManager interfaceUtilisateurManager, InterfaceNotificationManager notificationManager, InterfaceServiceManager serviceManager, InterfaceDeveloppeurManager developpeurManager, InterfaceTacheManager tacheManager)
		{
			this.projetManager = projetManager;
			this.utilisateurManager = interfaceUtilisateurManager;
			this.notificationManager = notificationManager;
			this.serviceManager = serviceManager;
			this.developpeurManager = developpeurManager;
			this.tacheManager = tacheManager;
		}
		// GET: ServiceController
		public ActionResult Index()
        {
            return View();
        }

        // GET: ServiceController/Details/5
        public ActionResult Details(int id)
        {
			int? utilisateurId = HttpContext.Session.GetInt32("utilisateurId");
			
			int? projetId = HttpContext.Session.GetInt32("projetId");
			ViewBag.ProjetId = projetId;
			Console.WriteLine(id);
            ServiceProjet service= serviceManager.GetServiceProjet(id);
            if(service.DeveloppeurAssigne != null)
            {
                service.DeveloppeurAssigne = developpeurManager.GetDeveloppeur(service.DeveloppeurAssigne.Id);
                Console.WriteLine(service.Id);
				List<Tache> taches = tacheManager.GetTachesByService(service);
                service.Taches = taches;
                Console.WriteLine(service.Taches.Count);
			}
            
			return View(service);
        }

		public ActionResult DetailsDev(int id)
		{
			int? utilisateurId = HttpContext.Session.GetInt32("utilisateurId");

			int? projetId = HttpContext.Session.GetInt32("projetId");
			ViewBag.ProjetId = projetId;
			Console.WriteLine(id);
			ServiceProjet service = serviceManager.GetServiceProjet(id);
			if (service.DeveloppeurAssigne != null)
			{
				service.DeveloppeurAssigne = developpeurManager.GetDeveloppeur(service.DeveloppeurAssigne.Id);
				Console.WriteLine(service.Id);
				List<Tache> taches = tacheManager.GetTachesByService(service);
				service.Taches = taches;
				Console.WriteLine(service.Taches.Count);
			}

			return View(service);
		}

		// GET: ServiceController/Create
		[HttpGet]
		public ActionResult Create()
		{
			int? projetId = HttpContext.Session.GetInt32("projetId");
			Projet projet = projetManager.ConsulterProjet((int)projetId);
            List<Developpeur> developpeurs = developpeurManager.GetDeveParProjet(projet);
			ViewBag.Developpeurs = developpeurs;
			ViewBag.ProjetId = projetId;
            Console.WriteLine(projetId);
			return View();
        }

        // POST: ServiceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ServiceProjet serviceProjet)
        {
            try
            {
				int? projetId = HttpContext.Session.GetInt32("projetId");
				Projet projet = projetManager.ConsulterProjet((int)projetId);
                serviceProjet.Projet = projet;
				var developpeurIdStr = Request.Form["DeveloppeurId"];
				if (!string.IsNullOrEmpty(developpeurIdStr) && int.TryParse(developpeurIdStr, out int developpeurId))
				{
					serviceProjet.DeveloppeurAssigne = developpeurManager.GetDeveloppeur(developpeurId);
				}
				serviceManager.AjouterService(serviceProjet);
				return RedirectToAction("AfficherServicesChefProjet", "Service",new { id = projet.Id });
            }
            catch
            {
                return View();
            }
        }

        // GET: ServiceController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ServiceController/Edit/5
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

        // GET: ServiceController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ServiceController/Delete/5
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
		public IActionResult AfficherServices(int id)
		{

			TempData["projetId"] = id;
			HttpContext.Session.SetInt32("projetId", id);
			Projet projet = projetManager.ConsulterProjet(id);
			List<ServiceProjet> services = serviceManager.getServiceByProjet(projet);
			foreach (var item in services)
			{
                if (item.DeveloppeurAssigne != null)
                {
                    item.DeveloppeurAssigne = developpeurManager.GetDeveloppeur(item.DeveloppeurAssigne.Id);
                }

			}
			return View(services);
		}

		[HttpGet]
		public IActionResult AfficherServicesChefProjet(int id)
		{

			TempData["projetId"] = id;
			HttpContext.Session.SetInt32("projetId", id);
			Projet projet = projetManager.ConsulterProjet(id);
			List<ServiceProjet> services = serviceManager.getServiceByProjet(projet);
			foreach (var item in services)
			{
                if (item.DeveloppeurAssigne != null)
                {
                    item.DeveloppeurAssigne = developpeurManager.GetDeveloppeur(item.DeveloppeurAssigne.Id);
                }

			}
			return View(services);
		}
	}
}
