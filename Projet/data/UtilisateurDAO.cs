using Microsoft.Data.SqlClient;
using ProjetNet.data.db_GL;
using ProjetNet.Models;

namespace ProjetNet.Data
{
	public class UtilisateurDAO : InterfaceCRUD<Utilisateur>
	{
		SqlConnection connection;
		SqlCommand command;
		SqlDataReader rd;

		public UtilisateurDAO()
		{
			connection = DbConnectionFactory.GetOpenConnection();
			command = new SqlCommand();
		}

		public void Add(Utilisateur entity)
		{
			try
			{
				if (connection.State != System.Data.ConnectionState.Open)
				{
					connection = DbConnectionFactory.GetOpenConnection();
				}
				command.Connection = connection;
				command.Parameters.Clear();
				command.CommandText = @"INSERT INTO Utilisateur (nom, prenom, email, motDePasse, role) 
                                   VALUES (@nom, @prenom, @email, @motDePasse, @role)";
				command.Parameters.AddWithValue("@nom", entity.Nom);
				command.Parameters.AddWithValue("@prenom", entity.Prenom);
				command.Parameters.AddWithValue("@email", entity.Email);
				command.Parameters.AddWithValue("@motDePasse", entity.MotDePasse);
				command.Parameters.AddWithValue("@role", entity.Role);

				command.ExecuteNonQuery();
			}
			finally
			{
				if (rd != null && !rd.IsClosed)
					rd.Close();

				if (connection != null && connection.State == System.Data.ConnectionState.Open)
				{
					connection.Close();
					
					connection.Dispose();
					command.Dispose();
				}
					
			}
		}

		public void Delete(int id)
		{
			try
			{
				if (connection.State != System.Data.ConnectionState.Open)
				{
					connection = DbConnectionFactory.GetOpenConnection();

				}
				command.Connection = connection;
				command.Parameters.Clear();
				command.CommandText = @"DELETE FROM Utilisateur WHERE id = @id";
				command.Parameters.AddWithValue("@id", id);
				command.ExecuteNonQuery();
			}
			finally
			{
				if (rd != null && !rd.IsClosed)
					rd.Close();

				if (connection != null && connection.State == System.Data.ConnectionState.Open)
				{
					connection.Close();
					
					connection.Dispose();
					command.Dispose();
				}
			}
		}

		public void Delete(string id)
		{
			try
			{
				if (connection.State != System.Data.ConnectionState.Open)
				{
					connection = DbConnectionFactory.GetOpenConnection();

				}
				command.Connection = connection;
				command.Parameters.Clear();
				command.CommandText = @"DELETE FROM Utilisateur WHERE email = @email";
				command.Parameters.AddWithValue("@email", id);
				command.ExecuteNonQuery();
			}
			finally
			{
				if (rd != null && !rd.IsClosed)
					rd.Close();

				if (connection != null && connection.State == System.Data.ConnectionState.Open)
				{
					connection.Close();
					
					connection.Dispose();
					command.Dispose();
				}
			}
		}

		public List<Utilisateur> GetAll()
		{
			try
			{
				if (connection.State != System.Data.ConnectionState.Open)
				{
					connection = DbConnectionFactory.GetOpenConnection();

				}
				command.Connection = connection;
				command.Parameters.Clear();
				command.CommandText = @"SELECT * FROM Utilisateur";
				rd = command.ExecuteReader();

				List<Utilisateur> utilisateurs = new List<Utilisateur>();
				while (rd.Read())
				{
					Utilisateur utilisateur = new Utilisateur
					{
						Id = rd.GetInt32(0),
						Nom = rd.GetString(1),
						Prenom = rd.GetString(2),
						Email = rd.GetString(3),
						MotDePasse = rd.GetString(4),
						Role = rd.GetString(5)
					};
					utilisateurs.Add(utilisateur);
				}

				rd.Close();
				return utilisateurs;
			}
			finally
			{
				if (rd != null && !rd.IsClosed)
					rd.Close();

				if (connection != null && connection.State == System.Data.ConnectionState.Open)
				{
					connection.Close();
					
					connection.Dispose();
					command.Dispose();
				}
			}
		}

		public Utilisateur GetById(int id)
		{
			try
			{
				if (connection.State != System.Data.ConnectionState.Open)
				{
					connection = DbConnectionFactory.GetOpenConnection();

				}
				command.Connection = connection;
				command.Parameters.Clear();
				command.CommandText = @"SELECT * FROM Utilisateur WHERE id = @id";
				command.Parameters.AddWithValue("@id", id);
				rd = command.ExecuteReader();

				Utilisateur utilisateur = null;
				if (rd.Read())
				{
					utilisateur = new Utilisateur
					{
						Id = rd.GetInt32(0),
						Nom = rd.GetString(1),
						Prenom = rd.GetString(2),
						Email = rd.GetString(3),
						MotDePasse = rd.GetString(4),
						Role = rd.GetString(5)
					};
				}

				rd.Close();
				return utilisateur;
			}
			finally
			{
				if (rd != null && !rd.IsClosed)
					rd.Close();

				if (connection != null && connection.State == System.Data.ConnectionState.Open)
				{
					connection.Close();
				
					connection.Dispose();
					command.Dispose();
				}
			}
		}

		public Utilisateur GetById(string id)
		{
			try
			{
				if (connection.State != System.Data.ConnectionState.Open)
				{
					connection = DbConnectionFactory.GetOpenConnection();

				}
				command.Connection = connection;
				command.Parameters.Clear();
				command.CommandText = @"SELECT * FROM Utilisateur WHERE email = @email";
				command.Parameters.AddWithValue("@email", id);
				rd = command.ExecuteReader();

				Utilisateur utilisateur = null;
				if (rd.Read())
				{
					utilisateur = new Utilisateur
					{
						Id = rd.GetInt32(0),
						Nom = rd.GetString(1),
						Prenom = rd.GetString(2),
						Email = rd.GetString(3),
						MotDePasse = rd.GetString(4),
						Role = rd.GetString(5)
					};
				}

				rd.Close();
				return utilisateur;
			}
			finally
			{
				if (rd != null && !rd.IsClosed)
					rd.Close();

				if (connection != null && connection.State == System.Data.ConnectionState.Open)
				{
					connection.Close();
					
					connection.Dispose();
					command.Dispose();
				}
			}
		}

		public Utilisateur GetByEmail(string email)
		{
			try
			{
				if (connection.State != System.Data.ConnectionState.Open)
				{
					connection = DbConnectionFactory.GetOpenConnection();

				}
				command.Connection = connection;
				command.Parameters.Clear();
				command.CommandText = @"SELECT * FROM Utilisateur WHERE email = @email";
				command.Parameters.AddWithValue("@email", email);
				rd = command.ExecuteReader();

				Utilisateur utilisateur = null;
				if (rd.Read())
				{
					utilisateur = new Utilisateur
					{
						Id = rd.GetInt32(0),
						Nom = rd.GetString(1),
						Prenom = rd.GetString(2),
						Email = rd.GetString(3),
						MotDePasse = rd.GetString(4),
						Role = rd.GetString(5)
					};
				}

				rd.Close();
				return utilisateur;
			}
			finally
			{
				if (rd != null && !rd.IsClosed)
					rd.Close();

				if (connection != null && connection.State == System.Data.ConnectionState.Open)
				{
					connection.Close();
					
					connection.Dispose();
					command.Dispose();
				}
			}
		}

		public void Update(Utilisateur entity)
		{
			try
			{
				if (connection.State != System.Data.ConnectionState.Open)
				{
					connection = DbConnectionFactory.GetOpenConnection();

				}
				command.Connection = connection;
				command.Parameters.Clear();
				command.CommandText = @"UPDATE Utilisateur 
                                   SET nom = @nom, 
                                       prenom = @prenom, 
                                       email = @email, 
                                       motDePasse = @motDePasse, 
                                       role = @role 
                                   WHERE id = @id";
				command.Parameters.AddWithValue("@id", entity.Id);
				command.Parameters.AddWithValue("@nom", entity.Nom);
				command.Parameters.AddWithValue("@prenom", entity.Prenom);
				command.Parameters.AddWithValue("@email", entity.Email);
				command.Parameters.AddWithValue("@motDePasse", entity.MotDePasse);
				command.Parameters.AddWithValue("@role", entity.Role);

				command.ExecuteNonQuery();
			}
			finally
			{
				if (rd != null && !rd.IsClosed)
					rd.Close();

				if (connection != null && connection.State == System.Data.ConnectionState.Open)
				{
					connection.Close();
					
					connection.Dispose();
					command.Dispose();
				}
			}
		}

		// Méthode additionnelle pour vérifier les identifiants d'un utilisateur
		public Utilisateur Authentifier(string email, string motDePasse)
		{
			try
			{
				if (connection.State != System.Data.ConnectionState.Open)
				{
					connection = DbConnectionFactory.GetOpenConnection();

				}
				command.Connection = connection;
				command.Parameters.Clear();
				command.CommandText = @"SELECT * FROM Utilisateur WHERE email = @email AND motDePasse = @motDePasse";
				command.Parameters.AddWithValue("@email", email);
				command.Parameters.AddWithValue("@motDePasse", motDePasse);
				rd = command.ExecuteReader();

				Utilisateur utilisateur = null;
				if (rd.Read())
				{
					utilisateur = new Utilisateur
					{
						Id = rd.GetInt32(0),
						Nom = rd.GetString(1),
						Prenom = rd.GetString(2),
						Email = rd.GetString(3),
						MotDePasse = rd.GetString(4),
						Role = rd.GetString(5)
					};
				}

				rd.Close();
				return utilisateur;
			}
			finally
			{
				if (rd != null && !rd.IsClosed)
					rd.Close();

				if (connection != null && connection.State == System.Data.ConnectionState.Open)
				{
					connection.Close();
					
					connection.Dispose();
					command.Dispose();
				}
			}
		}

		// Méthode qui retourne tous les utilisateurs avec un rôle spécifique
		public List<Utilisateur> GetByRole(string role)
		{
			try
			{
				if (connection.State != System.Data.ConnectionState.Open)
				{
					connection = DbConnectionFactory.GetOpenConnection();

				}
				command.Connection = connection;
				command.Parameters.Clear();
				command.CommandText = @"SELECT * FROM Utilisateur WHERE role = @role";
				command.Parameters.AddWithValue("@role", role);
				rd = command.ExecuteReader();

				List<Utilisateur> utilisateurs = new List<Utilisateur>();
				while (rd.Read())
				{
					Utilisateur utilisateur = new Utilisateur
					{
						Id = rd.GetInt32(0),
						Nom = rd.GetString(1),
						Prenom = rd.GetString(2),
						Email = rd.GetString(3),
						MotDePasse = rd.GetString(4),
						Role = rd.GetString(5)
					};
					utilisateurs.Add(utilisateur);
				}

				rd.Close();
				return utilisateurs;
			}
			finally
			{
				if (rd != null && !rd.IsClosed)
					rd.Close();

				if (connection != null && connection.State == System.Data.ConnectionState.Open)
				{
					connection.Close();
					
					connection.Dispose();
					command.Dispose();
				}
			}
		}

		
	}
}
