using ProjetGL.Models;


namespace ProjetGL.Interfaces
{
    public interface IGestion_Account
    {
        void Add(Account account);
        void Del(string Username);
        bool Exist(string Username);
        Account Find(string Username);
        List<Account> GetAccounts();
        void Update(string Username, Account newAccount);

    }
}
