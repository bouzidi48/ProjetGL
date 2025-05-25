using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetNet.Bussiness;
using ProjetNet.Models;
using ProjetNet.Services;

namespace ProjetNet.Controllers
{
    public class NotificationController : Controller
    {
		InterfaceProjetManager projetManager;
		InterfaceUtilisateurManager utilisateurManager;
		InterfaceNotificationManager notificationManager;
		InterfaceServiceManager serviceManager;
		InterfaceDeveloppeurManager developpeurManager;
		InterfaceTacheManager tacheManager;

		public NotificationController(InterfaceProjetManager projetManager, InterfaceUtilisateurManager interfaceUtilisateurManager, InterfaceNotificationManager notificationManager, InterfaceServiceManager serviceManager, InterfaceDeveloppeurManager developpeurManager, InterfaceTacheManager tacheManager)
		{
			this.projetManager = projetManager;
			this.utilisateurManager = interfaceUtilisateurManager;
			this.notificationManager = notificationManager;
			this.serviceManager = serviceManager;
			this.developpeurManager = developpeurManager;
			this.tacheManager = tacheManager;
		}
		// GET: NotificationController
		public ActionResult Index()
        {
            return View();
        }

        // GET: NotificationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NotificationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NotificationController/Create
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

        // GET: NotificationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NotificationController/Edit/5
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

        // GET: NotificationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NotificationController/Delete/5
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
		public ActionResult ListerNotification()
		{
			int? id = HttpContext.Session.GetInt32("utilisateurId");
			List<Notification> notifications = new List<Notification>();
            notifications = notificationManager.RecevoirNotification((int)id);


			return View(notifications);
		}

		[HttpPost]
		public IActionResult MarquerCommeLue(int id)
		{
            Console.WriteLine(id);
			Notification notification = notificationManager.GetNotification(id);
            Console.WriteLine(notification.Message);
			if (notification != null)
			{
				notification.EstLue = true;
                Console.WriteLine(notification.Destinataire.Id);
				notificationManager.ModifierNotification(notification);
			}

			return RedirectToAction("ListerNotification");
		}

	}


}
