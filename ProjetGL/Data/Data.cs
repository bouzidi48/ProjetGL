using Microsoft.Data.SqlClient;
using ProjetGL.Models;

namespace ProjetGL.Data
{
    public class Data : IData
    {
        private BD bd;

        public Data()
        {
            Bd = new BD();

        }
        public BD Bd { get => bd; set => bd = value; }

        public void Add(Account account)
        {
            Bd.Command.CommandText = $@"insert into account values('{account.Login}','{account.Password}')";
            Bd.Command.ExecuteNonQuery();
        }

        public void Del(string login)
        {
            throw new NotImplementedException();
        }

        public bool Exist(string login)
        {
            Bd.Command.CommandText = $@"select * from account where login ='{login}'";
            SqlDataReader rd = Bd.Command.ExecuteReader();
            if (rd.Read())
            {
                rd.Close();
                return true;
            }
            rd.Close();
            return false;
        }

        public Account Find(string login)
        {
            Bd.Command.CommandText = $@"select * from account where login ='{login}'";
            SqlDataReader rd = Bd.Command.ExecuteReader();
            if (rd.Read())
            {
                var account = new Account
                {
                    Login = rd["login"].ToString(),
                    Password = rd["password"].ToString()
                };
                rd.Close();
                return account;
            }
            rd.Close();
            return null;
        }


        public List<Account> GetAccounts()
        {
            Bd.Command.CommandText = "select * from account";
            SqlDataReader rd = Bd.Command.ExecuteReader();
            List<Account> accounts = new List<Account>();
            while (rd.Read())
            {
                accounts.Add(new Account
                {
                    Login = rd["login"].ToString(),
                    Password = rd["password"].ToString()
                });
            }
            rd.Close();
            return accounts;
        }

        public void Update(string login, Account newAccount)
        {
            Bd.Command.CommandText = $@"update account set login = '{newAccount.Login}', password = '{newAccount.Password}' where login = '{login}'";
            Bd.Command.ExecuteNonQuery();
        }
    }
}
