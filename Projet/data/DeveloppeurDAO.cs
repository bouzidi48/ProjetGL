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
        public DeveloppeurDAO()
        {
            connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\oo\Documents\esisa_4eme_annee\Porjet .NET\projet .NET\ProjetGL\Projet\data\db_GL\db_GL.mdf"";Integrated Security=True");
            connection.Open();
            command = new SqlCommand();
            command.Connection = connection;
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
                    SET projetId = @projetId
                    WHERE id = @id";

            command.Parameters.AddWithValue("@id", entity.Id);
            command.Parameters.AddWithValue("@projetId", entity.Projet?.Id ?? (object)DBNull.Value);

            command.ExecuteNonQuery();

            // 3. Mise à jour des technologies (optionnel mais recommandé)
            // Suppression des anciennes associations
            command.Parameters.Clear();
            command.CommandText = @"DELETE FROM DeveloppeurTechnologie WHERE developpeurId = @id";
            command.Parameters.AddWithValue("@id", entity.Id);
            command.ExecuteNonQuery();

            // Insertion des nouvelles
            if (entity.Technologies != null)
            {
                foreach (var techno in entity.Technologies)
                {
                    command.Parameters.Clear();
                    command.CommandText = @"INSERT INTO DeveloppeurTechnologie (developpeurId, technologieId) VALUES (@devId, @techId)";
                    command.Parameters.AddWithValue("@devId", entity.Id);
                    command.Parameters.AddWithValue("@techId", techno.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Developpeur> GetByTechno(Technologie technologie)
        {
            command.Parameters.Clear();
            command.CommandText = @"SELECT * FROM DeveloppeurTechnologie WHERE technologieId = @id";
            command.Parameters.AddWithValue("@id", technologie.Id);
            rd = command.ExecuteReader();

            List<Developpeur> developpeurs = new List<Developpeur>();
            while (rd.Read())
            {
                Developpeur developpeur = new Developpeur
                {
                    Id = rd.GetInt32(0),
                };
                developpeurs.Add(developpeur);
            }
            rd.Close();
            List<Developpeur> developpeurs1 = new List<Developpeur>();

            foreach (Developpeur developpeur in developpeurs)
            {
                Developpeur dev;

                dev = (Developpeur)utilisateurDAO.GetById(developpeur.Id);
                developpeurs1.Add(dev);
            }
            

            return developpeurs1;
        }

        public List<Developpeur> GetByTechnos(List<Technologie> technologie)
        {
            List<Developpeur> developpeurs = new List<Developpeur>();
            foreach (Technologie techno in technologie)
            {
               List<Developpeur> dev = GetByTechno(techno);
                developpeurs.AddRange(dev);
            }
            return developpeurs;
        }
}
