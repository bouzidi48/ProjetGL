using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetGL.Data;
using ProjetGL.Models;

namespace ProjetGL.Pages.Utilisateur
{
    public class GestionUtilisateursModel : PageModel
    {
		private readonly Gestion_Account _dataAccount;

		public List<Account> Users { get; set; } = new List<Account>();

		public string Message { get; set; } = string.Empty;

		public GestionUtilisateursModel()
		{
			_dataAccount = new Gestion_Account();
		}

		public void OnGet()
		{
			Users = _dataAccount.GetAllAccounts();
		}

		public IActionResult OnPostDelete(string username)
		{
			_dataAccount.DeleteAccount(username);
			Message = $"Utilisateur {username} supprimé avec succès.";
			Users = _dataAccount.GetAllAccounts();
			return Page();
		}
	}
}
