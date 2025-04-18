using ProjetGL.Models;

namespace ProjetGL.Interfaces
{
	public interface IGestion_Account
	{
        void AddAccount(Account account);
        Account FindAccount(string username);
        void UpdateAccount(string Username,Account account);
        void DeleteAccount(string username);
        List<Account> GetAllAccounts();
    }
}
