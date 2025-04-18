using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetGL.Data;
using ProjetGL.Models;

namespace ProjetGL.Pages.AppelDoffres
{
	public class ListerModel : PageModel
	{
		private readonly Gestion_AppelOffre _dataAppelOffre;

		public List<AppelOffre> AppelsDoffres { get; set; } = new List<AppelOffre>();

		public ListerModel()
		{
			_dataAppelOffre = new Gestion_AppelOffre();
		}

		public void OnGet()
		{
			AppelsDoffres = _dataAppelOffre.GetAllAppelOffre();

			// Charger les ressources associées
			foreach (var appel in AppelsDoffres)
			{
				appel.Ressources = _dataAppelOffre.GetRessourcesByAppelId(appel.AppelId);
				Console.WriteLine($"Appel {appel.AppelId} contient {appel.Ressources.Count} ressources.");

			}
		}
	}
}
