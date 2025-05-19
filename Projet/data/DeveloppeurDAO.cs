using Microsoft.Data.SqlClient;
using ProjetNet.Data;
using ProjetNet.Models;

namespace ProjetNet.data
{
    public class DeveloppeurDAO : InterfaceCRUD<Developpeur>
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader rd;
        UtilisateurDAO utilisateurDAO;
        ProjetDAO projetDAO;
        ServiceDAO serviceDAO;
        TacheDAO tacheDAO;
        public DeveloppeurDAO()
        {
            connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\oo\Documents\esisa_4eme_annee\Porjet .NET\projet .NET\ProjetGL\Projet\data\db_GL\db_GL.mdf"";Integrated Security=True");
            connection.Open();
            command = new SqlCommand();
            command.Connection = connection;
            utilisateurDAO = new UtilisateurDAO();
            projetDAO = new ProjetDAO();
            serviceDAO = new ServiceDAO();
            tacheDAO = new TacheDAO();
        }

        public void Add(Developpeur entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public List<Developpeur> GetAll()
        {
            throw new NotImplementedException();
        }

        public Developpeur GetById(int id)
        {
			Developpeur developpeur = new Developpeur();

			command.Parameters.Clear();
			command.CommandText = @"
        SELECT d.id, d.technonlogies, d.projetId
        FROM Developpeur d
        WHERE d.id = @id";

			command.Parameters.AddWithValue("@id", id);

			using (var reader = command.ExecuteReader())
			{
				if (reader.Read())
				{
					developpeur = new Developpeur
					{
						Id = reader.GetInt32(reader.GetOrdinal("id")),
						Projet = rd.IsDBNull(rd.GetOrdinal("projetId")) ? null : new Projet { Id = rd.GetInt32(rd.GetOrdinal("projetId")) },
						Technologies = rd.IsDBNull(rd.GetOrdinal("technonlogies"))
						? new List<Technologie>()
						: rd.GetString(rd.GetOrdinal("technonlogies"))
							  .Split(',', StringSplitOptions.RemoveEmptyEntries)
							  .Select(t => Enum.TryParse(t.Trim(), true, out Technologie result) ? result : (Technologie?)null)
							  .Where(t => t.HasValue)
							  .Select(t => t.Value)
							  .ToList(),
						ServicesAssignes = new List<ServiceProjet>(),
						Taches = new List<Tache>()
					};

				}
			}

			if(developpeur != null) {
                if (developpeur.Projet != null)
                {
                    developpeur.Projet = projetDAO.GetById(id);
                }
				developpeur.ServicesAssignes = serviceDAO.getSerByDev(developpeur);
				developpeur.Taches = tacheDAO.GetByIdDeveloppeur(developpeur);
				Utilisateur user = utilisateurDAO.GetById(developpeur.Id);
				developpeur.Nom = user.Nom;
				developpeur.Prenom = user.Prenom;
				developpeur.Role = user.Role;
				developpeur.Email = user.Email;
				developpeur.MotDePasse = user.MotDePasse;
			}

			return developpeur;
		}

        public Developpeur GetById(string id)
        {
            throw new NotImplementedException();
        }

		public List<Developpeur> GetDevByPro(Projet pro)
		{
			List<Developpeur> developpeurs = new List<Developpeur>();

			command.Parameters.Clear();
			command.CommandText = @"
        SELECT d.id, d.technonlogies
        FROM Developpeur d
        WHERE d.projetId = @projetId";

			command.Parameters.AddWithValue("@projetId", pro.Id);

			using (var reader = command.ExecuteReader())
			{
				while (reader.Read())
				{
					var dev = new Developpeur
					{
						Id = reader.GetInt32(reader.GetOrdinal("id")),
						Projet = pro,
						Technologies = rd.IsDBNull(rd.GetOrdinal("technonlogies"))
						? new List<Technologie>()
						: rd.GetString(rd.GetOrdinal("technonlogies"))
							  .Split(',', StringSplitOptions.RemoveEmptyEntries)
							  .Select(t => Enum.TryParse(t.Trim(), true, out Technologie result) ? result : (Technologie?)null)
							  .Where(t => t.HasValue)
							  .Select(t => t.Value)
							  .ToList(),
						ServicesAssignes = new List<ServiceProjet>(),
						Taches = new List<Tache>()
					};

					

					// Ajout temporaire à la liste
					developpeurs.Add(dev);
				}
			}

			// Chargement des services et tâches pour chaque développeur
			foreach (var dev in developpeurs)
			{
				dev.ServicesAssignes = serviceDAO.getSerByDev(dev);
				dev.Taches = tacheDAO.GetByIdDeveloppeur(dev);
				Utilisateur user = utilisateurDAO.GetById(dev.Id);
				dev.Nom = user.Nom;
				dev.Prenom = user.Prenom;
				dev.Role = user.Role;
				dev.Email = user.Email;
				dev.MotDePasse = user.MotDePasse;
			}

			return developpeurs;
		}


		public void Update(Developpeur entity)
        {
            // 2. Mise à jour de la table Developpeur
            command.Parameters.Clear();
            command.CommandText = @"
                    UPDATE Developpeur
                    SET projetId = @projetId,
                        technologies = @technologies
                    WHERE id = @id";

            command.Parameters.AddWithValue("@id", entity.Id);
            command.Parameters.AddWithValue("@projetId", entity.Projet?.Id ?? (object)DBNull.Value);

            // Convertir la liste des technologies en chaîne séparée par virgule
            string techString = entity.Technologies != null && entity.Technologies.Any()
                ? string.Join(",", entity.Technologies.Select(t => t.ToString()))
                : null;

            command.Parameters.AddWithValue("@technologies", (object?)techString ?? DBNull.Value);

            command.ExecuteNonQuery();


        }



        public List<Developpeur> GetByTechnos(List<Technologie> technologies)
        {
            List<Developpeur> developpeurs = new List<Developpeur>();

            if (technologies == null || technologies.Count == 0)
                return developpeurs;

            // Construire la condition SQL dynamiquement
            List<string> conditions = new List<string>();
            for (int i = 0; i < technologies.Count; i++)
            {
                string paramName = $"@tech{i}";
                conditions.Add($"technonlogies LIKE '%' + {paramName} + '%'");
            }

			string whereClause = $"({string.Join(" OR ", conditions)}) AND projetId IS NULL";
			command.Parameters.Clear();
            command.CommandText = $"SELECT * FROM Developpeur WHERE {whereClause}";

            for (int i = 0; i < technologies.Count; i++)
            {
                command.Parameters.AddWithValue($"@tech{i}", technologies[i].ToString());
            }

            rd = command.ExecuteReader();
            while (rd.Read())
            {
                Developpeur dev = new Developpeur
                {
                    Id = rd.GetInt32(rd.GetOrdinal("id")),
                    Projet = rd.IsDBNull(rd.GetOrdinal("projetId")) ? null : new Projet { Id = rd.GetInt32(rd.GetOrdinal("projetId")) },
                    Technologies = rd.IsDBNull(rd.GetOrdinal("technonlogies"))
                        ? new List<Technologie>()
                        : rd.GetString(rd.GetOrdinal("technonlogies"))
                              .Split(',', StringSplitOptions.RemoveEmptyEntries)
                              .Select(t => Enum.TryParse(t.Trim(), true, out Technologie result) ? result : (Technologie?)null)
                              .Where(t => t.HasValue)
                              .Select(t => t.Value)
                              .ToList()
                };

                developpeurs.Add(dev);
            }

            rd.Close();
            foreach(Developpeur developpeur in developpeurs)
            {
                if(developpeur.Projet != null)
                {
					developpeur.Projet = projetDAO.GetById(developpeur.Projet.Id);
				}
                developpeur.ServicesAssignes = serviceDAO.getSerByDev(developpeur);
                developpeur.Taches = tacheDAO.GetByIdDeveloppeur(developpeur);
                Utilisateur user = utilisateurDAO.GetById(developpeur.Id);
                developpeur.Nom = user.Nom;
                developpeur.Prenom = user.Prenom;
                developpeur.Role = user.Role;
                developpeur.Email = user.Email;
                developpeur.MotDePasse = user.MotDePasse;
            }
            return developpeurs;
        }
    }
}
