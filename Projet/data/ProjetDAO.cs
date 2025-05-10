using Microsoft.Data.SqlClient;
using ProjetNet.Data;
using ProjetNet.Models;

namespace ProjetNet.data
{
	public class ProjetDAO : InterfaceCRUD<Projet>
	{
		SqlConnection connection;
		SqlCommand command;
		SqlDataReader rd;
		UtilisateurDAO utilisateurDAO;

		public ProjetDAO()
		{
			connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\oo\Documents\esisa_4eme_annee\Porjet .NET\projet .NET\ProjetGL\Projet\data\db_GL\db_GL.mdf"";Integrated Security=True");
			connection.Open();
			command = new SqlCommand();
			command.Connection = connection;
			utilisateurDAO = new UtilisateurDAO();
		}

		public void Add(Projet entity)
		{
			command.Parameters.Clear();
			command.CommandText = @"INSERT INTO Projet (nom, description, dateDemarrage, dateLivraison, nombreJoursDev, clientId, directeurId, chefProjetId, methodologieId, dateReunion) 
                           VALUES (@nom, @description, @dateDemarrage, @dateLivraison, @nombreJoursDev, @clientId, @directeurId, @chefProjetId, @methodologieId, @dateReunion)";

			command.Parameters.AddWithValue("@nom", entity.Nom ?? (object)DBNull.Value);
			command.Parameters.AddWithValue("@description", entity.Description ?? (object)DBNull.Value);
			command.Parameters.AddWithValue("@dateDemarrage", entity.DateDemarrage ?? (object)DBNull.Value);
			command.Parameters.AddWithValue("@dateLivraison", entity.DateLivraison ?? (object)DBNull.Value);
			command.Parameters.AddWithValue("@nombreJoursDev", entity.NombreJoursDev.HasValue ? entity.NombreJoursDev.Value : (object)DBNull.Value);
			command.Parameters.AddWithValue("@clientId", entity.Client?.Id ?? (object)DBNull.Value);
			command.Parameters.AddWithValue("@directeurId", entity.Directeur?.Id ?? (object)DBNull.Value);
			command.Parameters.AddWithValue("@chefProjetId", entity.ChefProjet?.Id ?? (object)DBNull.Value);
			command.Parameters.AddWithValue("@methodologieId", entity.Methodologie?.Id ?? (object)DBNull.Value);
			command.Parameters.AddWithValue("@dateReunion", entity.DateReunion ?? (object)DBNull.Value);

			command.ExecuteNonQuery();
		}


		public void Delete(int id)
		{
			command.Parameters.Clear();
			command.CommandText = @"DELETE FROM Projet WHERE id = @id";
			command.Parameters.AddWithValue("@id", id);
			command.ExecuteNonQuery();
		}

		public void Delete(string id)
		{
			command.Parameters.Clear();
			command.CommandText = @"DELETE FROM Projet WHERE nom = @nom";
			command.Parameters.AddWithValue("@nom", id);
			command.ExecuteNonQuery();
		}

		public List<Projet> GetAll()
		{
			command.Parameters.Clear();
			command.CommandText = @"SELECT * FROM Projet";
			rd = command.ExecuteReader();

			List<Projet> projets = new List<Projet>();
			while (rd.Read())
			{
				Projet projet = new Projet
				{
					Id = rd.GetInt32(rd.GetOrdinal("id")),
					Nom = rd.IsDBNull(rd.GetOrdinal("nom")) ? null : rd.GetString(rd.GetOrdinal("nom")),
					Description = rd.IsDBNull(rd.GetOrdinal("description")) ? null : rd.GetString(rd.GetOrdinal("description")),
					DateDemarrage = rd.IsDBNull(rd.GetOrdinal("dateDemarrage")) ? (DateTime?)null : rd.GetDateTime(rd.GetOrdinal("dateDemarrage")),
					DateLivraison = rd.IsDBNull(rd.GetOrdinal("dateLivraison")) ? (DateTime?)null : rd.GetDateTime(rd.GetOrdinal("dateLivraison")),
					NombreJoursDev = rd.IsDBNull(rd.GetOrdinal("nombreJoursDev")) ? (int?)null : rd.GetInt32(rd.GetOrdinal("nombreJoursDev")),
					Client = rd.IsDBNull(rd.GetOrdinal("clientId")) ? null : new Client { Id = rd.GetInt32(rd.GetOrdinal("clientId")) },
					Directeur = rd.IsDBNull(rd.GetOrdinal("directeurId")) ? null : new DirecteurInformatique { Id = rd.GetInt32(rd.GetOrdinal("directeurId")) },
					ChefProjet = rd.IsDBNull(rd.GetOrdinal("chefProjetId")) ? null : new ChefProjet { Id = rd.GetInt32(rd.GetOrdinal("chefProjetId")) },
					Methodologie = rd.IsDBNull(rd.GetOrdinal("methodologieId")) ? null : new MethodologieDAO { Id = rd.GetInt32(rd.GetOrdinal("methodologieId")) },
					DateReunion = rd.IsDBNull(rd.GetOrdinal("dateReunion")) ? (DateTime?)null : rd.GetDateTime(rd.GetOrdinal("dateReunion"))
				};
				projets.Add(projet);
			}

			rd.Close();
			foreach (Projet item in projets)
			{
				if(item.Directeur != null) 
				{
					item.Directeur = (DirecteurInformatique)utilisateurDAO.GetById(item.Directeur.Id);
				}
				if (item.ChefProjet != null)
				{
					item.ChefProjet = (ChefProjet)utilisateurDAO.GetById(item.ChefProjet.Id);
				}
			}
			return projets;
		}


		public Projet GetById(int id)
		{
			command.Parameters.Clear();
			command.CommandText = @"SELECT * FROM Projet WHERE id = @id";
			command.Parameters.AddWithValue("@id", id);
			rd = command.ExecuteReader();
			Projet projet = null;

			if (rd.Read())
			{
				projet = new Projet
				{
					Id = rd.GetInt32(rd.GetOrdinal("id")),
					Nom = rd.IsDBNull(rd.GetOrdinal("nom")) ? null : rd.GetString(rd.GetOrdinal("nom")),
					Description = rd.IsDBNull(rd.GetOrdinal("description")) ? null : rd.GetString(rd.GetOrdinal("description")),
					DateDemarrage = rd.IsDBNull(rd.GetOrdinal("dateDemarrage")) ? (DateTime?)null : rd.GetDateTime(rd.GetOrdinal("dateDemarrage")),
					DateLivraison = rd.IsDBNull(rd.GetOrdinal("dateLivraison")) ? (DateTime?)null : rd.GetDateTime(rd.GetOrdinal("dateLivraison")),
					NombreJoursDev = rd.IsDBNull(rd.GetOrdinal("nombreJoursDev")) ? (int?)null : rd.GetInt32(rd.GetOrdinal("nombreJoursDev")),
					Client = rd.IsDBNull(rd.GetOrdinal("clientId")) ? null : new Client { Id = rd.GetInt32(rd.GetOrdinal("clientId")) },
					Directeur = rd.IsDBNull(rd.GetOrdinal("directeurId")) ? null : new DirecteurInformatique { Id = rd.GetInt32(rd.GetOrdinal("directeurId")) },
					ChefProjet = rd.IsDBNull(rd.GetOrdinal("chefProjetId")) ? null : new ChefProjet { Id = rd.GetInt32(rd.GetOrdinal("chefProjetId")) },
					Methodologie = rd.IsDBNull(rd.GetOrdinal("methodologieId")) ? null : new MethodologieDAO { Id = rd.GetInt32(rd.GetOrdinal("methodologieId")) },
					DateReunion = rd.IsDBNull(rd.GetOrdinal("dateReunion")) ? (DateTime?)null : rd.GetDateTime(rd.GetOrdinal("dateReunion"))
				};
			}

			rd.Close();
			
				if (projet.Directeur != null)
				{
					projet.Directeur = (DirecteurInformatique)utilisateurDAO.GetById(projet.Directeur.Id);
				}
				if (projet.ChefProjet != null)
				{
					projet.ChefProjet = (ChefProjet)utilisateurDAO.GetById(projet.ChefProjet.Id);
				}
			
			return projet;
		}

		public Projet GetById(string id)
		{
			command.Parameters.Clear();
			command.CommandText = @"SELECT * FROM Projet WHERE nom = @nom";
			command.Parameters.AddWithValue("@nom", id);
			rd = command.ExecuteReader();
			Projet projet = null;

			if (rd.Read())
			{
				projet = new Projet
				{
					Id = rd.GetInt32(rd.GetOrdinal("id")),
					Nom = rd.IsDBNull(rd.GetOrdinal("nom")) ? null : rd.GetString(rd.GetOrdinal("nom")),
					Description = rd.IsDBNull(rd.GetOrdinal("description")) ? null : rd.GetString(rd.GetOrdinal("description")),
					DateDemarrage = rd.IsDBNull(rd.GetOrdinal("dateDemarrage")) ? (DateTime?)null : rd.GetDateTime(rd.GetOrdinal("dateDemarrage")),
					DateLivraison = rd.IsDBNull(rd.GetOrdinal("dateLivraison")) ? (DateTime?)null : rd.GetDateTime(rd.GetOrdinal("dateLivraison")),
					NombreJoursDev = rd.IsDBNull(rd.GetOrdinal("nombreJoursDev")) ? (int?)null : rd.GetInt32(rd.GetOrdinal("nombreJoursDev")),
					Client = rd.IsDBNull(rd.GetOrdinal("clientId")) ? null : new Client { Id = rd.GetInt32(rd.GetOrdinal("clientId")) },
					Directeur = rd.IsDBNull(rd.GetOrdinal("directeurId")) ? null : new DirecteurInformatique { Id = rd.GetInt32(rd.GetOrdinal("directeurId")) },
					ChefProjet = rd.IsDBNull(rd.GetOrdinal("chefProjetId")) ? null : new ChefProjet { Id = rd.GetInt32(rd.GetOrdinal("chefProjetId")) },
					Methodologie = rd.IsDBNull(rd.GetOrdinal("methodologieId")) ? null : new MethodologieDAO { Id = rd.GetInt32(rd.GetOrdinal("methodologieId")) },
					DateReunion = rd.IsDBNull(rd.GetOrdinal("dateReunion")) ? (DateTime?)null : rd.GetDateTime(rd.GetOrdinal("dateReunion"))
				};
			}

			rd.Close();

			if (projet.Directeur != null)
			{
				projet.Directeur = (DirecteurInformatique)utilisateurDAO.GetById(projet.Directeur.Id);
			}
			if (projet.ChefProjet != null)
			{
				projet.ChefProjet = (ChefProjet)utilisateurDAO.GetById(projet.ChefProjet.Id);
			}

			return projet;
		}

		public void Update(Projet entity)
		{
			command.Parameters.Clear();
			command.CommandText = @"
        UPDATE Projet 
        SET 
            nom = @nom,
            description = @description,
            dateDemarrage = @dateDemarrage,
            dateLivraison = @dateLivraison,
            nombreJoursDev = @nombreJoursDev,
            clientId = @clientId,
            directeurId = @directeurId,
            chefProjetId = @chefProjetId,
            methodologieId = @methodologieId,
            dateReunion = @dateReunion
        WHERE id = @id";

			command.Parameters.AddWithValue("@id", entity.Id);
			command.Parameters.AddWithValue("@nom", entity.Nom ?? (object)DBNull.Value);
			command.Parameters.AddWithValue("@description", entity.Description ?? (object)DBNull.Value);
			command.Parameters.AddWithValue("@dateDemarrage", entity.DateDemarrage ?? (object)DBNull.Value);
			command.Parameters.AddWithValue("@dateLivraison", entity.DateLivraison ?? (object)DBNull.Value);
			command.Parameters.AddWithValue("@nombreJoursDev", entity.NombreJoursDev.HasValue ? entity.NombreJoursDev.Value : (object)DBNull.Value);
			command.Parameters.AddWithValue("@clientId", entity.Client?.Id ?? (object)DBNull.Value);
			command.Parameters.AddWithValue("@directeurId", entity.Directeur?.Id ?? (object)DBNull.Value);
			command.Parameters.AddWithValue("@chefProjetId", entity.ChefProjet?.Id ?? (object)DBNull.Value);
			command.Parameters.AddWithValue("@methodologieId", entity.Methodologie?.Id ?? (object)DBNull.Value);
			command.Parameters.AddWithValue("@dateReunion", entity.DateReunion ?? (object)DBNull.Value);

			command.ExecuteNonQuery();
		}

	}
}
