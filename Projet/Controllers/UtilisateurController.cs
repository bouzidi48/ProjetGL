using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetNet.Models;
using ProjetNet.Services;
using System.Security.Claims;

namespace ProjetNet.Controllers
{
    public class UtilisateurController : Controller
    {
        InterfaceUtilisateurManager utilisateurManager;

		public UtilisateurController(InterfaceUtilisateurManager utilisateurManager)
		{
			this.utilisateurManager = utilisateurManager;
		}




		// GET: UtilisateurController
		public ActionResult Index()
        {
            return View();
        }

		[HttpGet]
        public IActionResult Signout()
		{
			HttpContext.SignOutAsync("Cookies");
			return RedirectToAction("Signin");
		}
		[HttpGet]
		public IActionResult Signin()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Signin(Utilisateur user)
		{

			if (utilisateurManager.connecter(user.Email, user.MotDePasse))
			{
                Utilisateur user1 = utilisateurManager.GetById(user.Email);
				TempData["utilisateurId"] = user1.Id;
				if (user.Role == "directeur")
				{
					
					return RedirectToAction(nameof(Index), "Directeur");
				}
				else if(user.Role == "chef projet")
				{

					return RedirectToAction(nameof(Index), "Chef Projet");
				}
                else
                {
                    return RedirectToAction(nameof(Index), "Developpeur");
                }
			}
			else
			{
				ViewBag.msg = "Authentication failed!";
				return View();
			}
			//return Content("<script>alert('account :" + account.Username + " authentication failed')</script>", "text/html");

		}

		// GET: UtilisateurController/Details/5
		public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UtilisateurController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UtilisateurController/Create
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

        // GET: UtilisateurController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UtilisateurController/Edit/5
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

        // GET: UtilisateurController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UtilisateurController/Delete/5
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
