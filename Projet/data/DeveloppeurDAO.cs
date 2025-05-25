using Microsoft.Data.SqlClient;
using ProjetNet.data.db_GL;
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
        
        
        public DeveloppeurDAO()
        {
			connection = DbConnectionFactory.GetOpenConnection();
			command = new SqlCommand();
            utilisateurDAO = new UtilisateurDAO();
            
            
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
            try
            {
				if (connection.State != System.Data.ConnectionState.Open)
				{
					connection = DbConnectionFactory.GetOpenConnection();

				}
				command.Connection = connection;
				Developpeur developpeur = null;

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
							Projet = reader.IsDBNull(reader.GetOrdinal("projetId")) ? null : new Projet { Id = reader.GetInt32(reader.GetOrdinal("projetId")) },
							Technologies = reader.IsDBNull(reader.GetOrdinal("technonlogies"))
							? new List<Technologie>()
							: reader.GetString(reader.GetOrdinal("technonlogies"))
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

				if (developpeur != null)
				{



					Utilisateur user = utilisateurDAO.GetById(developpeur.Id);
					developpeur.Nom = user.Nom;
					developpeur.Prenom = user.Prenom;
					developpeur.Role = user.Role;
					developpeur.Email = user.Email;
					developpeur.MotDePasse = user.MotDePasse;
				}

				return developpeur;
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

        public Developpeur GetById(string id)
        {
            throw new NotImplementedException();
        }

		public List<Developpeur> GetDevByPro(Projet pro)
		{
            try
            {
				if (connection.State != System.Data.ConnectionState.Open)
				{
					connection = DbConnectionFactory.GetOpenConnection();

				}
				List<Developpeur> developpeurs = new List<Developpeur>();
				command.Connection = connection;
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
                            Technologies = reader.IsDBNull(reader.GetOrdinal("technonlogies"))
                            ? new List<Technologie>()
                            : reader.GetString(reader.GetOrdinal("technonlogies"))
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


                    Utilisateur user = utilisateurDAO.GetById(dev.Id);
                    dev.Nom = user.Nom;
                    dev.Prenom = user.Prenom;
                    dev.Role = user.Role;
                    dev.Email = user.Email;
                    dev.MotDePasse = user.MotDePasse;
                }

                return developpeurs;
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


		public void Update(Developpeur entity)
        {
            try
            {
				if (connection.State != System.Data.ConnectionState.Open)
				{
					connection = DbConnectionFactory.GetOpenConnection();

				}
				command.Connection = connection;
				// 2. Mise à jour de la table Developpeur
				command.Parameters.Clear();
                command.CommandText = @"
                    UPDATE Developpeur
                    SET projetId = @projetId,
                        technonlogies = @technologies
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



        public List<Developpeur> GetByTechnos(List<Technologie> technologies)
        {
            try
            {
				if (connection.State != System.Data.ConnectionState.Open)
				{
					connection = DbConnectionFactory.GetOpenConnection();

				}
				command.Connection = connection;
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
                foreach (Developpeur developpeur in developpeurs)
                {

                    Utilisateur user = utilisateurDAO.GetById(developpeur.Id);
                    developpeur.Nom = user.Nom;
                    developpeur.Prenom = user.Prenom;
                    developpeur.Role = user.Role;
                    developpeur.Email = user.Email;
                    developpeur.MotDePasse = user.MotDePasse;
                }
                return developpeurs;
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
