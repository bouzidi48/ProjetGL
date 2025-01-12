using ProjetGL.Models;

namespace ProjetGL.Interfaces
{
    public interface IAccountManager
    {
        void Add(Account account);
        void Del(string login);
        bool Exist(string login);
        Account Find(string login);
        List<Account> GetAccounts();
        void Update(string login, Account newAccount);
    }
}