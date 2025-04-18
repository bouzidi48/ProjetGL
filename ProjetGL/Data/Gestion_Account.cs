using Microsoft.Data.SqlClient;
using ProjetGL.Models;
using ProjetGL.GlobalServices;
using ProjetGL.Interfaces;
namespace ProjetGL.Data
{
    public class Gestion_Account : IGestion_Account
	{
		
        
        public Gestion_Account()
		{
			
		}
		public void UpdateAccount(string Username, Account newAccount)
		{
			GlobalBD.Bd.Command.CommandText = @"UPDATE Compte SET password = '" + newAccount.Password + "', role = '" + newAccount.Role +"' WHERE login = '" + Username + "'";

			GlobalBD.Bd.Command.ExecuteNonQuery();
		}



		public void AddAccount(Account account)
		{
			GlobalBD.Bd.Command.CommandText = $@"INSERT INTO Compte (login, password, role) 
                                         VALUES ('{account.Username}', '{account.Password}', '{account.Role}')";
			GlobalBD.Bd.Command.ExecuteNonQuery();
		}



		public bool Exists(string username)
		{
			GlobalBD.Bd.Command.CommandText = $@"SELECT * FROM Compte WHERE login = '{username}'";
			SqlDataReader rd = GlobalBD.Bd.Command.ExecuteReader();

			if (rd.Read())
			{
				rd.Close();
				return true;
			}

			rd.Close();
			return false;
		}



		public Account FindAccount(string Username)
		{
			GlobalBD.Bd.Command.CommandText = $@"SELECT * FROM Compte WHERE login = '{Username}'";
			SqlDataReader rd = GlobalBD.Bd.Command.ExecuteReader();

			if (rd.Read())
			{
				var account = new Account();

				account.Username = rd["login"].ToString();
				account.Password = rd["password"].ToString();
				account.Role = rd["role"].ToString();

				rd.Close();
				return account;
			}

			rd.Close();
			return null;
		}


		public void DeleteAccount(string username)
		{
			GlobalBD.Bd.Command.CommandText = $@"DELETE FROM Compte WHERE login = '{username}'";
			GlobalBD.Bd.Command.ExecuteNonQuery();
		}



		public List<Account> GetAllAccounts()
		{
			GlobalBD.Bd.Command.CommandText = "SELECT * FROM Compte";
			SqlDataReader rd = GlobalBD.Bd.Command.ExecuteReader();

			List<Account> accounts = new List<Account>();

			while (rd.Read())
			{
				accounts.Add(new Account
				{
					Username = rd["login"].ToString(),
					Password = rd["password"].ToString(),
					Role = rd["role"].ToString()
				});
			}

			rd.Close();
			return accounts;
		}








	}

}
    
