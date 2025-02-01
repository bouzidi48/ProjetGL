using ProjetGL.Models;

namespace ProjetGL.Interfaces
{
	public interface IAccountManager
	{
        void AddAccount(Account account);
        Account FindAccount(string username);
        void DeleteAccount(string username);
        List<Account> GetAllAccounts();
    }
}
