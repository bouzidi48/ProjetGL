using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetGL.Data;
using ProjetGL.Models;

namespace ProjetGL.Pages.AppelDoffres
{
	public class DetailsModel : PageModel
	{
		private readonly Gestion_AppelOffre _dataAppelOffre;

		public AppelOffre? AppelOffre { get; set; }

		public DetailsModel()
		{
			_dataAppelOffre = new Gestion_AppelOffre();
		}

		public IActionResult OnGet(int id)
		{
			Console.WriteLine($" Chargement des détails pour l'Appel d'Offre ID : {id}");

			AppelOffre = _dataAppelOffre.FindAppelOffre(id);

			if (AppelOffre == null)
			{
				Console.WriteLine(" Aucune donnée trouvée !");
				return NotFound();
			}

			Console.WriteLine($" Appel d'Offre {AppelOffre.AppelId} chargé avec {AppelOffre.Ressources.Count} ressources.");
			return Page();
		}
	}
}
