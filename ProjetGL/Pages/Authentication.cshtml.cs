using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetGL.GlobalServices;
using ProjetGL.Models;

namespace ProjetGL.Pages
{
    public class AuthenticationModel : PageModel
    {
        Account account;
        string msgErr;


        public Account Account { get => account; set => account = value; }
        public string MsgErr { get => msgErr; set => msgErr = value; }

        public override bool Equals(object? obj)
        {
            return obj is AuthenticationModel model &&
                   MsgErr == model.MsgErr;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost(Account account)
        {
            Account x = ServicesPages.managerAccount.Find(account.Username);
            if (x != null && x.Password == account.Password)
            {
                if (x.UserRole == Role.Responsable)
                    return RedirectToPage("/Roles/Responsable");

                else if (x.UserRole == Role.ChefDepartement)
                    return RedirectToPage("/Roles/ChefDepartement");
  
                else if (x.UserRole == Role.Enseignant)
                    return RedirectToPage("/Roles/Enseignant");

                else if (x.UserRole == Role.Fournisseur)
                    return RedirectToPage("/Roles/Fournisseur");

                else if (x.UserRole == Role.Technicien)
                    return RedirectToPage("/Roles/Technicien");

                else return RedirectToPage("/Index");
            }
            else
            {
                MsgErr = "Login or password incorrect";
                return Page();
            }
        }
    }
}
