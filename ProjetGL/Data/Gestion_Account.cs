using Microsoft.Data.SqlClient;
using ProjetGL.GlobalServices;
using ProjetGL.Interfaces;
using ProjetGL.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProjetGL.Data
{
    public class Gestion_Account : IGestion_Account
    {
        public Gestion_Account()
        {
            Account.cp = GetMaxIdUser();
        }


        public void Add(Account account)
        {
            GlobalBD.Bd.Command.CommandText = $@"insert into account values('{account.Id}','{account.Username}','{account.Password}','{account.UserRole}')";
            GlobalBD.Bd.Command.ExecuteNonQuery();
        }

        public void Del(string login)
        {
            throw new NotImplementedException();
        }

        public bool Exist(string username)
        {
			GlobalBD.Bd.Command.CommandText = $@"select * from account where Username ='{username}'";
            SqlDataReader rd = GlobalBD.Bd.Command.ExecuteReader();
            if (rd.Read())
            {
                rd.Close();
                return true;
            }
            rd.Close();
            return false;
        }

        public Account Find(string Username)
        {
			GlobalBD.Bd.Command.CommandText = $@"select * from account where Username ='{Username}'";
            SqlDataReader rd = GlobalBD.Bd.Command.ExecuteReader();
            if (rd.Read())
            {
                var account = new Account();

                account.Username = rd["Username"].ToString();
                account.Password = rd["Password"].ToString();
                account.UserRole = (Role)Enum.Parse(typeof(Role), rd["UserRole"].ToString());

                rd.Close();
                return account;
            }
            rd.Close();
            return null;
        }


        public List<Account> GetAccounts()
        {
			GlobalBD.Bd.Command.CommandText = "select * from account";
            SqlDataReader rd = GlobalBD.Bd.Command.ExecuteReader();
            List<Account> accounts = new List<Account>();
            while (rd.Read())
            {
                accounts.Add(new Account
                {
                    Username = rd["Username"].ToString(),
                    Password = rd["Password"].ToString()
                });
            }
            rd.Close();
            return accounts;
        }

        public void Update(string Username, Account newAccount)
        {
			GlobalBD.Bd.Command.CommandText = $@"update account set Username = '{newAccount.Username}', Password = '{newAccount.Password}' where Username = '{Username}'";
			GlobalBD.Bd.Command.ExecuteNonQuery();
        }


        public int GetMaxIdUser()
        {
            try
            {
                int maxId = 0;
				// Requête pour récupérer le maximum de IdUser
				GlobalBD.Bd.Command.CommandText = $@"SELECT ISNULL(MAX(Id), 0) FROM account";

                SqlDataReader rs = GlobalBD.Bd.Command.ExecuteReader();
                if (rs.Read() == true)
                {
                    maxId = (int)rs[0];
                    rs.Close();
                }
                return maxId;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération du MaxIdUser : {ex.Message}");
                return 0; // Retourne 0 en cas d'erreur
            }
        }


    }
}
