using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetGL.Data;
using ProjetGL.Models;

namespace ProjetGL.Pages.AppelDoffres
{
	public class DeleteModel : PageModel
	{
		private readonly Gestion_AppelOffre _dataAppelOffre;

		public DeleteModel()
		{
			_dataAppelOffre = new Gestion_AppelOffre();
		}

		[BindProperty]
		public AppelOffre AppelOffre { get; set; }

		public IActionResult OnGet(int id)
		{
			AppelOffre = _dataAppelOffre.FindAppelOffre(id);

			if (AppelOffre == null)
			{
				return RedirectToPage("Lister");
			}

			return Page();
		}

		public IActionResult OnPost()
		{
			if (AppelOffre == null)
			{
				return RedirectToPage("Lister");
			}

			_dataAppelOffre.DeleteAppelOffre(AppelOffre.AppelId);

			return RedirectToPage("Lister");
		}
	}
}
