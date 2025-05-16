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


		public void Delete(int id)
		{
			command.Parameters.Clear();
			command.CommandText = @"Update Developpeur SET projetId = null";
			command.Parameters.AddWithValue("@id", id);
			command.ExecuteNonQuery();
			command.Parameters.Clear();
			command.CommandText = @"DELETE FROM Projet WHERE id = @id";
			command.Parameters.AddWithValue("@id", id);
			command.ExecuteNonQuery();
		}

		public void Delete(string id)
		{
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

        public List<Projet> GetAll()
        {
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
                    Technologies = technologies
                };
                projets.Add(projet);
                technologies.Clear();
            }
            rd.Close();
            foreach (Projet item in projets)
            {
                if (item.Directeur != null)
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
                    Technologies = technologies
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
                    Client = rd.IsDBNull(rd.GetOrdinal("clientId")) ? null : rd.GetString(rd.GetOrdinal("client")),
                    Directeur = rd.IsDBNull(rd.GetOrdinal("directeurId")) ? null : new DirecteurInformatique { Id = rd.GetInt32(rd.GetOrdinal("directeurId")) },
                    ChefProjet = rd.IsDBNull(rd.GetOrdinal("chefProjetId")) ? null : new ChefProjet { Id = rd.GetInt32(rd.GetOrdinal("chefProjetId")) },
                    // Utiliser la valeur enum convertie
                    Methodologie = methodologieEnum,
                    DateReunion = rd.IsDBNull(rd.GetOrdinal("dateReunion")) ? (DateTime?)null : rd.GetDateTime(rd.GetOrdinal("dateReunion")),
                    Technologies = technologies
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
			command.Parameters.AddWithValue("@clientId", entity.Client ?? (object)DBNull.Value);
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

        
	}
}
