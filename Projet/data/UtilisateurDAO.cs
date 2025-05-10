using Microsoft.Data.SqlClient;
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
			connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\oo\Documents\esisa_4eme_annee\Porjet .NET\projet .NET\ProjetGL\Projet\data\db_GL\db_GL.mdf"";Integrated Security=True");
			connection.Open();
			command = new SqlCommand();
			command.Connection = connection;
		}

		public void Add(Utilisateur entity)
		{
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

		public void Delete(int id)
		{
			command.Parameters.Clear();
			command.CommandText = @"DELETE FROM Utilisateur WHERE id = @id";
			command.Parameters.AddWithValue("@id", id);
			command.ExecuteNonQuery();
		}

		public void Delete(string id)
		{
			command.Parameters.Clear();
			command.CommandText = @"DELETE FROM Utilisateur WHERE email = @email";
			command.Parameters.AddWithValue("@email", id);
			command.ExecuteNonQuery();
		}

		public List<Utilisateur> GetAll()
		{
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

		public Utilisateur GetById(int id)
		{
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

		public Utilisateur GetById(string id)
		{
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

		public Utilisateur GetByEmail(string email)
		{
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

		public void Update(Utilisateur entity)
		{
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

		// Méthode additionnelle pour vérifier les identifiants d'un utilisateur
		public Utilisateur Authentifier(string email, string motDePasse)
		{
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

		// Méthode qui retourne tous les utilisateurs avec un rôle spécifique
		public List<Utilisateur> GetByRole(string role)
		{
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

		
	}
}
