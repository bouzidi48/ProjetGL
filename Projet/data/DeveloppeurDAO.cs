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
        public DeveloppeurDAO()
        {
            connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\oo\Documents\esisa_4eme_annee\Porjet .NET\projet .NET\ProjetGL\Projet\data\db_GL\db_GL.mdf"";Integrated Security=True");
            connection.Open();
            command = new SqlCommand();
            command.Connection = connection;
            utilisateurDAO = new UtilisateurDAO();
            projetDAO = new ProjetDAO();
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
            throw new NotImplementedException();
        }

        public Developpeur GetById(string id)
        {
            throw new NotImplementedException();
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
                developpeur.Projet = projetDAO.GetById(developpeur.Projet.Id);
            }
            return developpeurs;
        }
    }
}
