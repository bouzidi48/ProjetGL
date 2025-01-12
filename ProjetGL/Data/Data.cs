using Microsoft.Data.SqlClient;
using ProjetGL.Models;

namespace ProjetGL.Data
{
    public class Data : IData
    {
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        public Data()
        {
            connection.ConnectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Othmane\4eme\DotNet\projet\ProjetGL\ProjetGL\Data\db_GL\db_GL.mdf;Integrated Security=True";
            connection.Open();
            command.Connection = connection;

        }
        public void Add(Account account)
        {
            command.CommandText = $@"insert into account values('{account.Login}','{account.Password}')";
            command.ExecuteNonQuery();
        }

        public void Del(string login)
        {
            throw new NotImplementedException();
        }

        public bool Exist(string login)
        {
            command.CommandText = $@"select * from account where login ='{login}'";
            SqlDataReader rd = command.ExecuteReader();
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
            command.CommandText = $@"select * from account where login ='{login}'";
            SqlDataReader rd = command.ExecuteReader();
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
            command.CommandText = "select * from account";
            SqlDataReader rd = command.ExecuteReader();
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
            command.CommandText = $@"update account set login = '{newAccount.Login}', password = '{newAccount.Password}' where login = '{login}'";
            command.ExecuteNonQuery();
        }
    }
}
