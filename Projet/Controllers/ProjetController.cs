using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;
using ProjetNet.Services;
using ProjetNet.Models;
using ProjetNet.Data;
using System.Runtime.Intrinsics.X86;
using ProjetNet.Bussiness;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjetNet.Controllers
{
    public class ProjetController : Controller
    {
        InterfaceProjetManager projetManager;
        InterfaceUtilisateurManager utilisateurManager;
		InterfaceNotificationManager notificationManager;
		InterfaceServiceManager serviceManager;
		InterfaceDeveloppeurManager developpeurManager;
		InterfaceTacheManager tacheManager;

		public ProjetController(InterfaceProjetManager projetManager, InterfaceUtilisateurManager interfaceUtilisateurManager, InterfaceNotificationManager notificationManager,InterfaceServiceManager serviceManager, InterfaceDeveloppeurManager developpeurManager, InterfaceTacheManager tacheManager)
		{
			this.projetManager = projetManager;
			this.utilisateurManager = interfaceUtilisateurManager;
			this.notificationManager = notificationManager;
			this.serviceManager = serviceManager;
			this.developpeurManager = developpeurManager;
			this.tacheManager = tacheManager;
		}


		// GET: ProjetController1
		public ActionResult Index()
        {
            return View();
        }

        // GET: ProjetController1/Details/5
        public ActionResult Details(int id)
        {

			Projet projet = projetManager.ConsulterProjet(id);
			projet.Services = serviceManager.getServiceByProjet(projet);
			projet.Developpeurs = developpeurManager.GetDeveParProjet(projet);
			foreach (var item in projet.Services)
			{
				if (item.DeveloppeurAssigne != null)
				{
					item.DeveloppeurAssigne = developpeurManager.GetDeveloppeur(item.DeveloppeurAssigne.Id);
				}

			}
			Console.WriteLine(projet.Technologies.Count);
			return View(projet);
        }

		

		public ActionResult DetailsChefProjet(int id)
		{

			Projet projet = projetManager.ConsulterProjet(id);
			projet.Services = serviceManager.getServiceByProjet(projet);
			projet.Developpeurs = developpeurManager.GetDeveParProjet(projet);
			foreach (var item in projet.Services)
			{
				if (item.DeveloppeurAssigne != null)
				{
					item.DeveloppeurAssigne = developpeurManager.GetDeveloppeur(item.DeveloppeurAssigne.Id);
				}

			}
			Console.WriteLine(projet.Technologies.Count);
			return View(projet);
		}
		[HttpGet]
		public ActionResult Create()
        {
            return View();
        }

		// POST: ProjetController1/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Projet projet)
		{
			
				
				// Vérifie si un projet avec ce nom existe déjà
				if (projetManager.GetByNom(projet.Nom)!= null)
				{
					Console.WriteLine(" Sauvegarde du projet");
					ModelState.AddModelError("Nom", "Un projet avec ce nom existe déjà.");
					return View(projet);
				}
				else
				{
					if(projet != null )
					{
						// Sauvegarde du projet
						Console.WriteLine(" Sauvegarde du projet");
						int? idUser = HttpContext.Session.GetInt32("utilisateurId");
						projet.Directeur = new DirecteurInformatique();
						projet.Directeur.Id = (int)idUser;
						bool test = projetManager.CreerProjet(projet);
						if(!test)
						{
							ModelState.AddModelError("Nom", "Un projet avec ce nom existe déjà.");
							return View(projet);
						}
						Console.WriteLine(" Sauvegarde du projet");
						//Console.WriteLine(test);
					}




					return RedirectToAction("GetAllProject", "Projet");
				}
					
		}


		// GET: ProjetController1/Edit/5
		public ActionResult Edit(int id)
        {
			Projet projet = projetManager.ConsulterProjet(id);
			projet.Services = serviceManager.getServiceByProjet(projet);
			projet.Developpeurs = developpeurManager.GetDeveParProjet(projet);
			return View(projet);
        }

        // POST: ProjetController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Projet projet)
        {
			Projet p = projetManager.ConsulterProjet(id);
			p.Nom = projet.Nom;
			p.Description = projet.Description;
			p.Client = projet.Client;
			p.DateDemarrage = projet.DateDemarrage;
			p.DateLivraison = projet.DateLivraison;
			p.NombreJoursDev = projet.NombreJoursDev;
			Console.WriteLine(p.Technologies.Count);
			projetManager.ModifierProjet(p);
				return RedirectToAction("GetAllProject", "Projet");
			
        }

		// GET: ProjetController1/EditMethoTech/4
		public ActionResult EditMethoTech(int id)
		{
			Projet projet = projetManager.ConsulterProjet(id);
			projet.Services = serviceManager.getServiceByProjet(projet);
			projet.Developpeurs = developpeurManager.GetDeveParProjet(projet);
			ViewBag.MethodologieList = new SelectList(Enum.GetValues(typeof(Methodologie)));
			return View(projet);
		}

		// POST: ProjetController1/EditMethoTech/4
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditMethoTech(int id, Projet projet)
		{
			HttpContext.Session.SetInt32("projetId", id);
			Projet p = projetManager.ConsulterProjet(id);
			p.Methodologie = projet.Methodologie;
			p.Technologies = projet.Technologies;
			projetManager.ModifierProjet(p);
			
			return RedirectToAction("SelectionnerdeveloppeursChefProjet", "Developpeur", new {id = p.Id});

		}
		[HttpGet]
		// GET: ProjetController1/EditMethoTech/4
		public ActionResult AffecterDateReunion(int id)
		{
			Projet projet = projetManager.ConsulterProjet(id);
			projet.Services = serviceManager.getServiceByProjet(projet);
			projet.Developpeurs = developpeurManager.GetDeveParProjet(projet);
			
			return View(projet);
		}

		// POST: ProjetController1/EditMethoTech/4
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AffecterDateReunion(int id, Projet projet)
		{
			//HttpContext.Session.SetInt32("projetId", id);
			Projet p = projetManager.ConsulterProjet(id);
			p.DateReunion = projet.DateReunion;
			List<Developpeur> dev = developpeurManager.GetDeveParProjet(p);
			foreach (var item in dev)
			{
				Notification notification = new Notification();
				notification.Message = "le projet " + projet.Nom + " est affctée pour vous! La date de Reunion est :" +p.DateReunion.ToString();
				notification.Destinataire = new Utilisateur();
				notification.Destinataire.Id = item.Id;
				notification.EstLue = false;

				notificationManager.EnvoyerNotification(notification);
			}
			projetManager.ModifierProjet(p);

			return RedirectToAction("ListerProjetChefProjet", "Projet", new { id = p.Id });

		}

		// GET: ProjetController1/Delete/5
		public ActionResult Delete(int id)
        {
			Projet projet = projetManager.ConsulterProjet(id);
			projet.Services = serviceManager.getServiceByProjet(projet);
			projet.Developpeurs = developpeurManager.GetDeveParProjet(projet);
			return View(projet);
        }

        // POST: ProjetController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Projet projet)
        {
            try
            {

				projetManager.SupprimerProjet(id);
                return RedirectToAction("GetallProject", "Projet");
            }
            catch
            {
                return View();
            }
        }

		[HttpGet]
		public IActionResult GetAllProject()
		{
            int? id = HttpContext.Session.GetInt32("utilisateurId");
			List<Projet> projets = new List<Projet>();
			//TempData["utilisateurId"] = id;
			if (id != null)
			{
				projets = projetManager.GetProjetParDir((int)id);
				foreach (var item in projets)
				{
					Projet pro = projetManager.ConsulterProjet(item.Id);
					item.Technologies = pro.Technologies;
				}
				Console.WriteLine(projets[0].Technologies.Count);
			}
			
			return View(projets);
		}

		[HttpGet]
		public IActionResult ListerProjetChefProjet()
		{
			int? id = HttpContext.Session.GetInt32("utilisateurId");
			List<Projet> projets = new List<Projet>();
			//TempData["utilisateurId"] = id;
			if (id != null)
			{
				projets = projetManager.GetProjetParChef((int)id);
				foreach (var item in projets)
				{
					Projet pro = projetManager.ConsulterProjet(item.Id);
					item.Technologies = pro.Technologies;
				}
			}

			return View(projets);
		}

		[HttpGet]
		public IActionResult ConsulterProjet()
		{
			int? id = HttpContext.Session.GetInt32("utilisateurId");
			
			Developpeur dev = developpeurManager.GetDeveloppeur((int)id);
			Console.WriteLine("dev = " + id);

			
			Projet projet = null;
			//TempData["utilisateurId"] = id;
			if (id != null && dev.Projet != null)
			{
				projet = projetManager.ConsulterProjet(dev.Projet.Id);
				HttpContext.Session.SetInt32("projetId", dev.Projet.Id);
				Console.WriteLine("projet = " + dev.Projet);
				projet.Services = serviceManager.GetServiceByDeveleppeur(dev);
			}

			return View(projet);
		}

		[HttpGet]
		public IActionResult AffecterChefProjet(int id)
		{
            
			TempData["projetId"] = id;
			HttpContext.Session.SetInt32("projetId", id);
			List<Utilisateur> chefprojets = utilisateurManager.GetByRole("Chef de Projet");
			return View(chefprojets);
		}


		

		[HttpPost]
		public IActionResult AffecterChef(int id)
		{
           
			int? projetId = HttpContext.Session.GetInt32("projetId");
			Projet projet = projetManager.ConsulterProjet((int)projetId);
            Console.WriteLine(projet.Nom);
            Utilisateur user = utilisateurManager.GetById(id);
            Console.WriteLine(id);
            Console.WriteLine(user.Id);
            projet.ChefProjet = new ChefProjet();
			projet.ChefProjet.Id = user.Id;
			projet.ChefProjet.Nom = user.Nom;
			projet.ChefProjet.Prenom = user.Prenom;
			projet.ChefProjet.Email = user.Email;
			projet.ChefProjet.Role = user.Role;
			projet.ChefProjet.MotDePasse = user.MotDePasse;
			Notification notification = new Notification();
			notification.Message = "le projet " + projet.Nom + " est affctée pour vous!";
			notification.Destinataire = new Utilisateur();
			notification.Destinataire.Id = projet.ChefProjet.Id;
			notification.EstLue = false;

			notificationManager.EnvoyerNotification(notification);

			projetManager.AffecterChefprojet(projet);
			TempData.Remove("projetId");
			HttpContext.Session.Remove("projetId");
			return RedirectToAction("GetAllProject", "Projet");
		}
	}
}
