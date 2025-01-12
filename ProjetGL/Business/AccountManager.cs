
using ProjetGL.Data;
using ProjetGL.Interfaces;
using ProjetGL.Models;

namespace ProjetGL.Business
{

    public class AccountManager : IAccountManager
    {
        IGestion_Account data = new Gestion_Account();
        public void Add(Account account)
        {
            data.Add(account);
        }

        public void Del(string login)
        {
            data.Del(login);
        }

        public bool Exist(string login)
        {
            return data.Exist(login);
        }

        public Account Find(string login)
        {
            return data.Find(login);
        }

        public List<Account> GetAccounts()
        {
            return data.GetAccounts();
        }

        public void Update(string login, Account newAccount)
        {
            data.Update(login, newAccount);
        }
    }
}
