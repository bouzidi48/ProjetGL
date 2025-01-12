using Microsoft.Data.SqlClient;
using ProjetGL.Models;

namespace ProjetGL.Data
{
    public class DataAccount : IDataAccount
    {
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        public DataAccount()
        {
            connection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=bd_GL;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
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
