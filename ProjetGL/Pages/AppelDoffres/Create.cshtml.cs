using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projet.Business;
using ProjetGL.Data;
using ProjetGL.Interfaces;
using ProjetGL.Models;

namespace ProjetGL.Pages.AppelDoffres
{
	public class CreateModel : PageModel
	{
		private readonly Gestion_AppelOffre _dataAppelOffre;
		private readonly Gestion_Ressource _dataRessource;

		[BindProperty]
		public AppelOffre AppelOffre { get; set; } = new AppelOffre();

		[BindProperty]
		public List<int> SelectedRessources { get; set; } = new List<int>();

		public List<Ressource> RessourcesDisponibles { get; set; } = new List<Ressource>();

		public CreateModel()
		{
			_dataAppelOffre = new Gestion_AppelOffre();
			_dataRessource = new Gestion_Ressource();
		}

		public void OnGet()
		{
			RessourcesDisponibles = _dataRessource.GetAllRessources();
		}

		public IActionResult OnPost()
		{
			if (!ModelState.IsValid)
			{
				RessourcesDisponibles = _dataRessource.GetAllRessources();
				return Page();
			}

			try
			{
				if (SelectedRessources == null)
				{
					SelectedRessources = new List<int>();
				}

				IAppelOffreManager manager = new AppelOffreManager();
				manager.AddAppelOffre(AppelOffre, SelectedRessources);

				TempData["SuccessMessage"] = "L'appel d'offre a été ajouté avec succès.";
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = $"Erreur : {ex.Message}";
			}

			RessourcesDisponibles = _dataRessource.GetAllRessources();
			return RedirectToPage("Lister");
		}


	}
}
