using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjetGL.GlobalServices;
using ProjetGL.Models;

namespace ProjetGL.Pages
{
    public class CreationCompteModel : PageModel
    {
        Account account;


        string msg;
        public Account Account { get => account; set => account = value; }
        public string Msg { get => msg; set => msg = value; }

        public List<SelectListItem> Roles { get; set; }
        public void OnGet()
        {
            Roles = Enum.GetValues(typeof(Role))
                        .Cast<Role>()
                        .Where(r => r != Role.Fournisseur) // Exclure Fournisseur
                        .Select(r => new SelectListItem
                        {
                            Value = r.ToString(),
                            Text = r.ToString()
                        })
                        .ToList();
        }
        public IActionResult OnPost(Account account)
        {

            if (ModelState.IsValid)
            {

                if (ServicesPages.managerAccount.Exist(account.Username) == false)
                {// s'il n'est pas dans la liste
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
