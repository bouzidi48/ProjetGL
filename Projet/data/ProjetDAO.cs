using Microsoft.Data.SqlClient;
using ProjetNet.data.db_GL;
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
			connection = DbConnectionFactory.GetOpenConnection();
			command = new SqlCommand();
			utilisateurDAO = new UtilisateurDAO();
		
            
		}

		public void Add(Projet entity)
		{
			try
			{
				if (connection.State != System.Data.ConnectionState.Open)
				{
					connection = DbConnectionFactory.GetOpenConnection();

				}
				command.Connection = connection;
				command.Parameters.Clear();
				command.CommandText = @"INSERT INTO Projet (nom, description, dateDemarrage, dateLivraison, nombreJoursDev, client, directeurId, chefProjetId, methodologie, dateReunion, technologies) 
                           VALUES (@nom, @description, @dateDemarrage, @dateLivraison, @nombreJoursDev, @client, @directeurId, @chefProjetId, @methodologie, @dateReunion, @technologies)";

				command.Parameters.AddWithValue("@nom", entity.Nom ?? (object)DBNull.Value);
				command.Parameters.AddWithValue("@description", entity.Description ?? (object)DBNull.Value);
				command.Parameters.AddWithValue("@dateDemarrage", entity.DateDemarrage ?? (object)DBNull.Value);
				command.Parameters.AddWithValue("@dateLivraison", entity.DateLivraison ?? (object)DBNull.Value);
				command.Parameters.AddWithValue("@nombreJoursDev", entity.NombreJoursDev.HasValue ? entity.NombreJoursDev.Value : (object)DBNull.Value);
				command.Parameters.AddWithValue("@client", entity.Client ?? (object)DBNull.Value);
				command.Parameters.AddWithValue("@directeurId", entity.Directeur?.Id ?? (object)DBNull.Value);
				command.Parameters.AddWithValue("@chefProjetId", entity.ChefProjet?.Id ?? (object)DBNull.Value);
				command.Parameters.AddWithValue("@methodologie", entity?.Methodologie ?? (object)DBNull.Value);
				command.Parameters.AddWithValue("@dateReunion", entity.DateReunion ?? (object)DBNull.Value);

				// Conversion de la liste de technologies en string séparé par des virgules
				string technologiesStr = entity.Technologies != null
					? string.Join(",", entity.Technologies.Select(t => t.ToString()))
					: null;
				command.Parameters.AddWithValue("@technologies", (object?)technologiesStr ?? DBNull.Value);


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
				command.CommandText = @"Update Developpeur SET projetId = null";
				command.Parameters.AddWithValue("@id", id);
				command.ExecuteNonQuery();
				command.Parameters.Clear();
				command.CommandText = @"DELETE FROM Projet WHERE id = @id";
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
				Projet projet = GetById(id);
				command.Parameters.Clear();
				command.CommandText = @"Update Developpeur SET projetId = null";
				command.Parameters.AddWithValue("@id", projet.Id);
				command.ExecuteNonQuery();
				command.Parameters.Clear();
				command.CommandText = @"DELETE FROM Projet WHERE nom = @nom";
				command.Parameters.AddWithValue("@nom", id);
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

        public List<Projet> GetAll()
        {
			try
			{
				if (connection.State != System.Data.ConnectionState.Open)
				{
					connection = DbConnectionFactory.GetOpenConnection();

				}
				command.Connection = connection;
				command.Parameters.Clear();
				command.CommandText = @"SELECT * FROM Projet";
				rd = command.ExecuteReader();
				List<Projet> projets = new List<Projet>();
				List<Technologie> technologies = new List<Technologie>();
				while (rd.Read())
				{
					if (!rd.IsDBNull(rd.GetOrdinal("technologies")))
					{
						string techString = rd.GetString(rd.GetOrdinal("technologies"));
						var techSplit = techString.Split(',', StringSplitOptions.RemoveEmptyEntries);
						foreach (var tech in techSplit)
						{
							if (Enum.TryParse<Technologie>(tech.Trim(), true, out var parsedTech))
							{
								technologies.Add(parsedTech);
							}
						}
					}
					Methodologie methodologieEnum = Methodologie.Acune; // Valeur par défaut

					if (!rd.IsDBNull(rd.GetOrdinal("methodologie")))
					{
						string methodologieStr = rd.GetString(rd.GetOrdinal("methodologie"));

						// Essayer de convertir la chaîne en enum
						if (Enum.TryParse(methodologieStr, true, out Methodologie parsedMethodologie))
						{
							methodologieEnum = parsedMethodologie;
						}
					}

					Projet projet = new Projet
					{
						Id = rd.GetInt32(rd.GetOrdinal("id")),
						Nom = rd.IsDBNull(rd.GetOrdinal("nom")) ? null : rd.GetString(rd.GetOrdinal("nom")),
						Description = rd.IsDBNull(rd.GetOrdinal("description")) ? null : rd.GetString(rd.GetOrdinal("description")),
						DateDemarrage = rd.IsDBNull(rd.GetOrdinal("dateDemarrage")) ? (DateTime?)null : rd.GetDateTime(rd.GetOrdinal("dateDemarrage")),
						DateLivraison = rd.IsDBNull(rd.GetOrdinal("dateLivraison")) ? (DateTime?)null : rd.GetDateTime(rd.GetOrdinal("dateLivraison")),
						NombreJoursDev = rd.IsDBNull(rd.GetOrdinal("nombreJoursDev")) ? (int?)null : rd.GetInt32(rd.GetOrdinal("nombreJoursDev")),
						Client = rd.IsDBNull(rd.GetOrdinal("client")) ? null : rd.GetString(rd.GetOrdinal("client")),
						Directeur = rd.IsDBNull(rd.GetOrdinal("directeurId")) ? null : new DirecteurInformatique { Id = rd.GetInt32(rd.GetOrdinal("directeurId")) },
						ChefProjet = rd.IsDBNull(rd.GetOrdinal("chefProjetId")) ? null : new ChefProjet { Id = rd.GetInt32(rd.GetOrdinal("chefProjetId")) },
						// Utiliser la valeur enum convertie
						Methodologie = methodologieEnum,
						DateReunion = rd.IsDBNull(rd.GetOrdinal("dateReunion")) ? (DateTime?)null : rd.GetDateTime(rd.GetOrdinal("dateReunion")),
						Technologies = technologies,
						Services = new List<ServiceProjet>(),
						Developpeurs = new List<Developpeur>()
					};
					projets.Add(projet);
					technologies.Clear();
				}
				rd.Close();
				foreach (Projet item in projets)
				{
					if (item != null)
					{
						if (item.Directeur != null)
						{
							Utilisateur user = utilisateurDAO.GetById(item.Directeur.Id);
							item.Directeur.Id = user.Id;
							item.Directeur.Nom = user.Nom;
							item.Directeur.Prenom = user.Prenom;
							item.Directeur.Email = user.Email;
							item.Directeur.Role = user.Role;
							item.Directeur.MotDePasse = user.MotDePasse;
						}
					}
					if (item != null)
					{
						if (item.ChefProjet != null)
						{
							Utilisateur user = utilisateurDAO.GetById(item.ChefProjet.Id);
							item.ChefProjet.Id = user.Id;
							item.ChefProjet.Nom = user.Nom;
							item.ChefProjet.Prenom = user.Prenom;
							item.ChefProjet.Email = user.Email;
							item.ChefProjet.Role = user.Role;
							item.ChefProjet.MotDePasse = user.MotDePasse;
						}
					}
				}
				return projets;
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

		public List<Projet> GetProjetByChefProjet(int idchef)
		{
			try
			{
				if (connection.State != System.Data.ConnectionState.Open)
				{
					connection = DbConnectionFactory.GetOpenConnection();

				}
				command.Connection = connection;
				command.Parameters.Clear();
				command.CommandText = @"SELECT * FROM Projet WHERE chefProjetId = @idchef";
				command.Parameters.AddWithValue("@idchef", idchef);
				rd = command.ExecuteReader();
				List<Projet> projets = new List<Projet>();
				List<Technologie> technologies = new List<Technologie>();
				while (rd.Read())
				{
					if (!rd.IsDBNull(rd.GetOrdinal("technologies")))
					{
						string techString = rd.GetString(rd.GetOrdinal("technologies"));
						var techSplit = techString.Split(',', StringSplitOptions.RemoveEmptyEntries);
						foreach (var tech in techSplit)
						{
							if (Enum.TryParse<Technologie>(tech.Trim(), true, out var parsedTech))
							{
								technologies.Add(parsedTech);
							}
						}
					}
					Methodologie methodologieEnum = Methodologie.Acune; // Valeur par défaut

					if (!rd.IsDBNull(rd.GetOrdinal("methodologie")))
					{
						string methodologieStr = rd.GetString(rd.GetOrdinal("methodologie"));

						// Essayer de convertir la chaîne en enum
						if (Enum.TryParse(methodologieStr, true, out Methodologie parsedMethodologie))
						{
							methodologieEnum = parsedMethodologie;
						}
					}

					Projet projet = new Projet
					{
						Id = rd.GetInt32(rd.GetOrdinal("id")),
						Nom = rd.IsDBNull(rd.GetOrdinal("nom")) ? null : rd.GetString(rd.GetOrdinal("nom")),
						Description = rd.IsDBNull(rd.GetOrdinal("description")) ? null : rd.GetString(rd.GetOrdinal("description")),
						DateDemarrage = rd.IsDBNull(rd.GetOrdinal("dateDemarrage")) ? (DateTime?)null : rd.GetDateTime(rd.GetOrdinal("dateDemarrage")),
						DateLivraison = rd.IsDBNull(rd.GetOrdinal("dateLivraison")) ? (DateTime?)null : rd.GetDateTime(rd.GetOrdinal("dateLivraison")),
						NombreJoursDev = rd.IsDBNull(rd.GetOrdinal("nombreJoursDev")) ? (int?)null : rd.GetInt32(rd.GetOrdinal("nombreJoursDev")),
						Client = rd.IsDBNull(rd.GetOrdinal("client")) ? null : rd.GetString(rd.GetOrdinal("client")),
						Directeur = rd.IsDBNull(rd.GetOrdinal("directeurId")) ? null : new DirecteurInformatique { Id = rd.GetInt32(rd.GetOrdinal("directeurId")) },
						ChefProjet = rd.IsDBNull(rd.GetOrdinal("chefProjetId")) ? null : new ChefProjet { Id = rd.GetInt32(rd.GetOrdinal("chefProjetId")) },
						// Utiliser la valeur enum convertie
						Methodologie = methodologieEnum,
						DateReunion = rd.IsDBNull(rd.GetOrdinal("dateReunion")) ? (DateTime?)null : rd.GetDateTime(rd.GetOrdinal("dateReunion")),
						Technologies = technologies,
						Services = new List<ServiceProjet>(),
						Developpeurs = new List<Developpeur>()
					};
					projets.Add(projet);
					technologies.Clear();
				}
				rd.Close();
				foreach (Projet item in projets)
				{
					if (item != null)
					{
						if (item.Directeur != null)
						{
							Utilisateur user = utilisateurDAO.GetById(item.Directeur.Id);
							item.Directeur.Id = user.Id;
							item.Directeur.Nom = user.Nom;
							item.Directeur.Prenom = user.Prenom;
							item.Directeur.Email = user.Email;
							item.Directeur.Role = user.Role;
							item.Directeur.MotDePasse = user.MotDePasse;
						}
					}
					if (item != null)
					{
						if (item.ChefProjet != null)
						{
							Utilisateur user = utilisateurDAO.GetById(item.ChefProjet.Id);
							item.ChefProjet.Id = user.Id;
							item.ChefProjet.Nom = user.Nom;
							item.ChefProjet.Prenom = user.Prenom;
							item.ChefProjet.Email = user.Email;
							item.ChefProjet.Role = user.Role;
							item.ChefProjet.MotDePasse = user.MotDePasse;
						}
					}

				}
				return projets;
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

		public List<Projet> GetProjetByDirecteur(int iddir)
		{
			try
			{
				if (connection.State != System.Data.ConnectionState.Open)
				{
					connection = DbConnectionFactory.GetOpenConnection();

				}
				command.Connection = connection;
				command.Parameters.Clear();
				command.CommandText = @"SELECT * FROM Projet WHERE directeurId = @iddir";
				command.Parameters.AddWithValue("@iddir", iddir);
				rd = command.ExecuteReader();
				List<Projet> projets = new List<Projet>();
				List<Technologie> technologies = new List<Technologie>();
				while (rd.Read())
				{
					if (!rd.IsDBNull(rd.GetOrdinal("technologies")))
					{
						string techString = rd.GetString(rd.GetOrdinal("technologies"));
						var techSplit = techString.Split(',', StringSplitOptions.RemoveEmptyEntries);
						foreach (var tech in techSplit)
						{
							if (Enum.TryParse<Technologie>(tech.Trim(), true, out var parsedTech))
							{
								technologies.Add(parsedTech);
							}
						}
					}
					Methodologie methodologieEnum = Methodologie.Acune; // Valeur par défaut

					if (!rd.IsDBNull(rd.GetOrdinal("methodologie")))
					{
						string methodologieStr = rd.GetString(rd.GetOrdinal("methodologie"));

						// Essayer de convertir la chaîne en enum
						if (Enum.TryParse(methodologieStr, true, out Methodologie parsedMethodologie))
						{
							methodologieEnum = parsedMethodologie;
						}
					}

					Projet projet = new Projet
					{
						Id = rd.GetInt32(rd.GetOrdinal("id")),
						Nom = rd.IsDBNull(rd.GetOrdinal("nom")) ? null : rd.GetString(rd.GetOrdinal("nom")),
						Description = rd.IsDBNull(rd.GetOrdinal("description")) ? null : rd.GetString(rd.GetOrdinal("description")),
						DateDemarrage = rd.IsDBNull(rd.GetOrdinal("dateDemarrage")) ? (DateTime?)null : rd.GetDateTime(rd.GetOrdinal("dateDemarrage")),
						DateLivraison = rd.IsDBNull(rd.GetOrdinal("dateLivraison")) ? (DateTime?)null : rd.GetDateTime(rd.GetOrdinal("dateLivraison")),
						NombreJoursDev = rd.IsDBNull(rd.GetOrdinal("nombreJoursDev")) ? (int?)null : rd.GetInt32(rd.GetOrdinal("nombreJoursDev")),
						Client = rd.IsDBNull(rd.GetOrdinal("client")) ? null : rd.GetString(rd.GetOrdinal("client")),
						Directeur = rd.IsDBNull(rd.GetOrdinal("directeurId")) ? null : new DirecteurInformatique { Id = rd.GetInt32(rd.GetOrdinal("directeurId")) },
						ChefProjet = rd.IsDBNull(rd.GetOrdinal("chefProjetId")) ? null : new ChefProjet { Id = rd.GetInt32(rd.GetOrdinal("chefProjetId")) },
						// Utiliser la valeur enum convertie
						Methodologie = methodologieEnum,
						DateReunion = rd.IsDBNull(rd.GetOrdinal("dateReunion")) ? (DateTime?)null : rd.GetDateTime(rd.GetOrdinal("dateReunion")),
						Technologies = technologies,
						Services = new List<ServiceProjet>(),
						Developpeurs = new List<Developpeur>()
					};
					projets.Add(projet);
					technologies.Clear();
				}
				rd.Close();
				foreach (Projet item in projets)
				{
					if (item != null)
					{
						if (item.Directeur != null)
						{
							Utilisateur user = utilisateurDAO.GetById(item.Directeur.Id);
							item.Directeur.Id = user.Id;
							item.Directeur.Nom = user.Nom;
							item.Directeur.Prenom = user.Prenom;
							item.Directeur.Email = user.Email;
							item.Directeur.Role = user.Role;
							item.Directeur.MotDePasse = user.MotDePasse;
						}

					}
					if (item != null)
					{
						if (item.ChefProjet != null)
						{
							Utilisateur user = utilisateurDAO.GetById(item.ChefProjet.Id);
							item.ChefProjet.Id = user.Id;
							item.ChefProjet.Nom = user.Nom;
							item.ChefProjet.Prenom = user.Prenom;
							item.ChefProjet.Email = user.Email;
							item.ChefProjet.Role = user.Role;
							item.ChefProjet.MotDePasse = user.MotDePasse;
						}
					}

				}
				return projets;
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

		

		public Projet GetById(int id)
		{
			try
			{
				if (connection.State != System.Data.ConnectionState.Open)
				{
					connection = DbConnectionFactory.GetOpenConnection();

				}
				command.Connection = connection;
				command.Parameters.Clear();
				command.CommandText = @"SELECT * FROM Projet WHERE id = @id";
				command.Parameters.AddWithValue("@id", id);


				rd = command.ExecuteReader();
				Projet projet = null;
				List<Technologie> technologies = new List<Technologie>();
				if (rd.Read())
				{
					if (!rd.IsDBNull(rd.GetOrdinal("technologies")))
					{
						string techString = rd.GetString(rd.GetOrdinal("technologies"));
						var techSplit = techString.Split(',', StringSplitOptions.RemoveEmptyEntries);
						foreach (var tech in techSplit)
						{
							if (Enum.TryParse<Technologie>(tech.Trim(), true, out var parsedTech))
							{
								technologies.Add(parsedTech);
							}
						}
					}
					Methodologie methodologieEnum = Methodologie.Acune; // Valeur par défaut

					if (!rd.IsDBNull(rd.GetOrdinal("methodologie")))
					{
						string methodologieStr = rd.GetString(rd.GetOrdinal("methodologie"));

						// Essayer de convertir la chaîne en enum
						if (Enum.TryParse(methodologieStr, true, out Methodologie parsedMethodologie))
						{
							methodologieEnum = parsedMethodologie;
						}
					}

					projet = new Projet
					{
						Id = rd.GetInt32(rd.GetOrdinal("id")),
						Nom = rd.IsDBNull(rd.GetOrdinal("nom")) ? null : rd.GetString(rd.GetOrdinal("nom")),
						Description = rd.IsDBNull(rd.GetOrdinal("description")) ? null : rd.GetString(rd.GetOrdinal("description")),
						DateDemarrage = rd.IsDBNull(rd.GetOrdinal("dateDemarrage")) ? (DateTime?)null : rd.GetDateTime(rd.GetOrdinal("dateDemarrage")),
						DateLivraison = rd.IsDBNull(rd.GetOrdinal("dateLivraison")) ? (DateTime?)null : rd.GetDateTime(rd.GetOrdinal("dateLivraison")),
						NombreJoursDev = rd.IsDBNull(rd.GetOrdinal("nombreJoursDev")) ? (int?)null : rd.GetInt32(rd.GetOrdinal("nombreJoursDev")),
						Client = rd.IsDBNull(rd.GetOrdinal("client")) ? null : rd.GetString(rd.GetOrdinal("client")),
						Directeur = rd.IsDBNull(rd.GetOrdinal("directeurId")) ? null : new DirecteurInformatique { Id = rd.GetInt32(rd.GetOrdinal("directeurId")) },
						ChefProjet = rd.IsDBNull(rd.GetOrdinal("chefProjetId")) ? null : new ChefProjet { Id = rd.GetInt32(rd.GetOrdinal("chefProjetId")) },
						// Utiliser la valeur enum convertie
						Methodologie = methodologieEnum,
						DateReunion = rd.IsDBNull(rd.GetOrdinal("dateReunion")) ? (DateTime?)null : rd.GetDateTime(rd.GetOrdinal("dateReunion")),
						Technologies = technologies,
						Services = new List<ServiceProjet>(),
						Developpeurs = new List<Developpeur>()
					};

				}
				rd.Close();
				if (projet != null)
				{
					if (projet.Directeur != null)
					{
						Utilisateur user = utilisateurDAO.GetById(projet.Directeur.Id);
						projet.Directeur.Id = user.Id;
						projet.Directeur.Nom = user.Nom;
						projet.Directeur.Prenom = user.Prenom;
						projet.Directeur.Email = user.Email;
						projet.Directeur.Role = user.Role;
						projet.Directeur.MotDePasse = user.MotDePasse;
					}

				}
				if (projet != null)
				{
					if (projet.ChefProjet != null)
					{
						Utilisateur user = utilisateurDAO.GetById(projet.ChefProjet.Id);
						projet.ChefProjet.Id = user.Id;
						projet.ChefProjet.Nom = user.Nom;
						projet.ChefProjet.Prenom = user.Prenom;
						projet.ChefProjet.Email = user.Email;
						projet.ChefProjet.Role = user.Role;
						projet.ChefProjet.MotDePasse = user.MotDePasse;
					}
				}

				return projet;
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

		public Projet GetById(string id)
		{
			try
			{
				if (connection.State != System.Data.ConnectionState.Open)
				{
					connection = DbConnectionFactory.GetOpenConnection();

				}
				command.Connection = connection;
				command.Parameters.Clear();
				command.CommandText = @"SELECT * FROM Projet WHERE nom = @id";
				command.Parameters.AddWithValue("@id", id);
				rd = command.ExecuteReader();
				Projet projet = null;
				List<Technologie> technologies = new List<Technologie>();
				if (rd.Read())
				{
					if (!rd.IsDBNull(rd.GetOrdinal("technologies")))
					{
						string techString = rd.GetString(rd.GetOrdinal("technologies"));
						var techSplit = techString.Split(',', StringSplitOptions.RemoveEmptyEntries);
						foreach (var tech in techSplit)
						{
							if (Enum.TryParse<Technologie>(tech.Trim(), true, out var parsedTech))
							{
								technologies.Add(parsedTech);
							}
						}
					}
					Methodologie methodologieEnum = Methodologie.Acune; // Valeur par défaut

					if (!rd.IsDBNull(rd.GetOrdinal("methodologie")))
					{
						string methodologieStr = rd.GetString(rd.GetOrdinal("methodologie"));

						// Essayer de convertir la chaîne en enum
						if (Enum.TryParse(methodologieStr, true, out Methodologie parsedMethodologie))
						{
							methodologieEnum = parsedMethodologie;
						}
					}

					projet = new Projet
					{
						Id = rd.GetInt32(rd.GetOrdinal("id")),
						Nom = rd.IsDBNull(rd.GetOrdinal("nom")) ? null : rd.GetString(rd.GetOrdinal("nom")),
						Description = rd.IsDBNull(rd.GetOrdinal("description")) ? null : rd.GetString(rd.GetOrdinal("description")),
						DateDemarrage = rd.IsDBNull(rd.GetOrdinal("dateDemarrage")) ? (DateTime?)null : rd.GetDateTime(rd.GetOrdinal("dateDemarrage")),
						DateLivraison = rd.IsDBNull(rd.GetOrdinal("dateLivraison")) ? (DateTime?)null : rd.GetDateTime(rd.GetOrdinal("dateLivraison")),
						NombreJoursDev = rd.IsDBNull(rd.GetOrdinal("nombreJoursDev")) ? (int?)null : rd.GetInt32(rd.GetOrdinal("nombreJoursDev")),
						Client = rd.IsDBNull(rd.GetOrdinal("client")) ? null : rd.GetString(rd.GetOrdinal("client")),
						Directeur = rd.IsDBNull(rd.GetOrdinal("directeurId")) ? null : new DirecteurInformatique { Id = rd.GetInt32(rd.GetOrdinal("directeurId")) },
						ChefProjet = rd.IsDBNull(rd.GetOrdinal("chefProjetId")) ? null : new ChefProjet { Id = rd.GetInt32(rd.GetOrdinal("chefProjetId")) },
						// Utiliser la valeur enum convertie
						Methodologie = methodologieEnum,
						DateReunion = rd.IsDBNull(rd.GetOrdinal("dateReunion")) ? (DateTime?)null : rd.GetDateTime(rd.GetOrdinal("dateReunion")),
						Technologies = technologies,
						Services = new List<ServiceProjet>(),
						Developpeurs = new List<Developpeur>()
					};

				}
				rd.Close();
				if (projet != null)
				{
					if (projet.Directeur != null)
					{
						Utilisateur user = utilisateurDAO.GetById(projet.Directeur.Id);
						if (user != null)
						{
							projet.Directeur.Id = user.Id;
							projet.Directeur.Nom = user.Nom;
							projet.Directeur.Prenom = user.Prenom;
							projet.Directeur.Email = user.Email;
							projet.Directeur.Role = user.Role;
							projet.Directeur.MotDePasse = user.MotDePasse;
						}
					}
				}
				if (projet != null)
				{
					if (projet.ChefProjet != null)
					{
						Utilisateur user = utilisateurDAO.GetById(projet.ChefProjet.Id);
						if (user != null)
						{

							projet.ChefProjet.Id = user.Id;
							projet.ChefProjet.Nom = user.Nom;
							projet.ChefProjet.Prenom = user.Prenom;
							projet.ChefProjet.Email = user.Email;
							projet.ChefProjet.Role = user.Role;
							projet.ChefProjet.MotDePasse = user.MotDePasse;
						}
					}
				}

				return projet;
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

		public void Update(Projet entity)
		{
			try
			{
				if (connection.State != System.Data.ConnectionState.Open)
				{
					connection = DbConnectionFactory.GetOpenConnection();
					
				}
				command.Connection = connection;
				command.Parameters.Clear();
				command.CommandText = @"
        UPDATE Projet 
        SET 
            nom = @nom,
            description = @description,
            dateDemarrage = @dateDemarrage,
            dateLivraison = @dateLivraison,
            nombreJoursDev = @nombreJoursDev,
            client = @client,
            directeurId = @directeurId,
            chefProjetId = @chefProjetId,
            methodologie = @methodologie,
            dateReunion = @dateReunion,
            technologies = @technologies
        WHERE id = @id";

				command.Parameters.AddWithValue("@id", entity.Id);
				command.Parameters.AddWithValue("@nom", entity.Nom ?? (object)DBNull.Value);
				command.Parameters.AddWithValue("@description", entity.Description ?? (object)DBNull.Value);
				command.Parameters.AddWithValue("@dateDemarrage", entity.DateDemarrage ?? (object)DBNull.Value);
				command.Parameters.AddWithValue("@dateLivraison", entity.DateLivraison ?? (object)DBNull.Value);
				command.Parameters.AddWithValue("@nombreJoursDev", entity.NombreJoursDev.HasValue ? entity.NombreJoursDev.Value : (object)DBNull.Value);
				command.Parameters.AddWithValue("@client", entity.Client ?? (object)DBNull.Value);
				command.Parameters.AddWithValue("@directeurId", entity.Directeur?.Id ?? (object)DBNull.Value);
				command.Parameters.AddWithValue("@chefProjetId", entity.ChefProjet?.Id ?? (object)DBNull.Value);
				command.Parameters.AddWithValue("@methodologie", entity?.Methodologie ?? (object)DBNull.Value);
				command.Parameters.AddWithValue("@dateReunion", entity.DateReunion ?? (object)DBNull.Value);

				// Convertir la liste des technologies en chaîne séparée par virgule
				string techString = entity.Technologies != null && entity.Technologies.Any()
					? string.Join(",", entity.Technologies.Select(t => t.ToString()))
					: null;

				command.Parameters.AddWithValue("@technologies", (object?)techString ?? DBNull.Value);

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

        
	}
}
