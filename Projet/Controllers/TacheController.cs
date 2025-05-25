using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using ProjetNet.Bussiness;
using ProjetNet.Models;
using ProjetNet.Services;

namespace ProjetNet.Controllers
{
    public class TacheController : Controller
    {
		InterfaceProjetManager projetManager;
		InterfaceUtilisateurManager utilisateurManager;
		InterfaceNotificationManager notificationManager;
		InterfaceServiceManager serviceManager;
		InterfaceDeveloppeurManager developpeurManager;
		InterfaceTacheManager tacheManager;

		public TacheController(InterfaceProjetManager projetManager, InterfaceUtilisateurManager interfaceUtilisateurManager, InterfaceNotificationManager notificationManager, InterfaceServiceManager serviceManager, InterfaceDeveloppeurManager developpeurManager, InterfaceTacheManager tacheManager)
		{
			this.projetManager = projetManager;
			this.utilisateurManager = interfaceUtilisateurManager;
			this.notificationManager = notificationManager;
			this.serviceManager = serviceManager;
			this.developpeurManager = developpeurManager;
			this.tacheManager = tacheManager;
		}
		// GET: TacheController
		public ActionResult Index()
        {
			
			return View();
        }

        // GET: TacheController/Details/5
        public ActionResult Details(int id)
        {
			Tache tache = tacheManager.GetTache(id);
			int? serviceId = HttpContext.Session.GetInt32("serviceId");
			ViewBag.ServiceId = serviceId;
			ServiceProjet service = serviceManager.GetServiceProjet((int)serviceId);
            if (service != null) 
            {
                service.DeveloppeurAssigne = developpeurManager.GetDeveloppeur(service.DeveloppeurAssigne.Id);
                tache.Service = service;
                tache.Developpeur = service.DeveloppeurAssigne;
                
            }
			return View(tache);
        }

        // GET: TacheController/Create
        public ActionResult Create()
        {
			int? serviceId = HttpContext.Session.GetInt32("serviceId");
			ViewBag.ServiceId = serviceId;

			return View();
        }
        [HttpGet]
		public IActionResult AfficherTaches(int id)
		{

			TempData["serviceId"] = id;
			HttpContext.Session.SetInt32("serviceId", id);
			int? projetId = HttpContext.Session.GetInt32("projetId");
			ViewBag.ProjetId = projetId;

			ServiceProjet service = serviceManager.GetServiceProjet(id);
            Console.WriteLine(service.Nom);
			List<Tache> taches = tacheManager.GetTachesByService(service);
			foreach (var item in taches)
			{
				if (item.Service != null)
				{
					Console.WriteLine(service.Nom);
					item.Service = service;
				}

			}
			return View(taches);
		}

		// POST: TacheController/Create
		[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tache tacheProjet)
        {
			int? serviceId = HttpContext.Session.GetInt32("serviceId");
            ServiceProjet service = serviceManager.GetServiceProjet((int)serviceId);
           
            if (service == null || service.DeveloppeurAssigne == null) 
            {
                
				return RedirectToAction("Create", "Tache");
			}
            else
            {
                service.DeveloppeurAssigne = developpeurManager.GetDeveloppeur(service.DeveloppeurAssigne.Id);
                tacheProjet.Service = service;
                tacheProjet.Developpeur = service.DeveloppeurAssigne;
                tacheManager.AjouterTache(tacheProjet);
				return RedirectToAction("AfficherTaches", "Tache", new { id = service.Id});

			}
		}

		// GET: TacheController/Edit/5
		[HttpGet]
		public ActionResult Edit(int id)
        {
            Tache tache = tacheManager.GetTache(id);
			int? serviceId = HttpContext.Session.GetInt32("serviceId");
			ViewBag.ServiceId = serviceId;
			return View(tache);
        }

        // POST: TacheController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int id, Tache tache)
        {
            Tache tache2 = tacheManager.GetTache(id);
            tache2.DescriptionTache = tache.DescriptionTache;
            tache2.PourcentageAvancement = tache.PourcentageAvancement;
            tache2.Nom = tache.Nom;
            tacheManager.UpdateTache(tache2);
			return RedirectToAction("AfficherTaches", "Tache", new { id = tache2.Service.Id });
		}

        // GET: TacheController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TacheController/Delete/5
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
