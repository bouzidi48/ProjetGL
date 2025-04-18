using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetGL.Data;
using ProjetGL.Models;

namespace ProjetGL.Pages.Utilisateur
{
    public class AjouterUtilisateurModel : PageModel
    {
		private readonly Gestion_Account _dataAccount;

		[BindProperty]
		public Account Utilisateur { get; set; } = new Account();

		public string Message { get; set; } = string.Empty;

		public AjouterUtilisateurModel()
		{
			_dataAccount = new Gestion_Account();
		}

		public void OnGet()
		{
		}

		public IActionResult OnPost()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			// Vérifier si l'utilisateur existe déjà
			if (_dataAccount.Exists(Utilisateur.Username))

			{
				Message = "Cet utilisateur existe déjà.";
				return Page();
			}

			_dataAccount.AddAccount(Utilisateur);

			Message = "Utilisateur ajouté avec succès.";

			return RedirectToPage("GestionUtilisateurs");
		}
	}
}
