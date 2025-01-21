using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetGL.GlobalServices;
using ProjetGL.Models;

namespace ProjetGL.Pages
{
    public class InscriptionModel : PageModel
    {
        Account account;


        string msg;
        public Account Account { get => account; set => account = value; }
        public string Msg { get => msg; set => msg = value; }

        public void OnGet()
        {

        }
        public IActionResult OnPost(Account account)
        {
            
            if (ModelState.IsValid)
            {

                if (ServicesPages.managerAccount.Exist(account.Username) == false)
                {// s'il n'est pas dans la liste
                    account.UserRole=Role.Fournisseur;
                    ServicesPages.managerAccount.Add(account);

                    Msg = "account added with success";
                }
                else
                {
                    Msg = "Account already exist";
                }

            }
            return Page();
        }
    }
}
