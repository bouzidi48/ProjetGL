using ProjetGL.Models;
using ProjetGL.Models;


namespace ProjetGL.Business
{

        public interface IAccountManager
        {
            void Add(Account account);
            bool Exist(string login);
            Account Find(string login);
            void Del(string login);
            void Update(string login, Account newAccount);
            List<Account> GetAccounts();
        }
    }