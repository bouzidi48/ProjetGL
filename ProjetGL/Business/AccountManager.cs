using ProjetGL.Data;
using ProjetGL.Models;
using ProjetGL.Interfaces;

namespace ProjetGL.Business
{
    public class AccountManager : IAccountManager
	{
		IGestion_Account data = new Gestion_Account();

        public void AddAccount(Account account)
        {
            if (data.FindAccount(account.Username) == null)
                data.AddAccount(account);
            else
                throw new Exception("account already exist");
        }

        public void DeleteAccount(string username)
        {
            data.DeleteAccount(username);
        }

        public Account FindAccount(string username)
        {
            return data.FindAccount(username);
        }

        public List<Account> GetAllAccounts()
        {
            return data.GetAllAccounts();
        }

        
    }

}
