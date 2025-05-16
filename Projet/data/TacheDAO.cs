using ProjetNet.Data;
using ProjetNet.Models;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace ProjetNet.data
{
    public class TacheDAO : InterfaceCRUD<Tache>
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader rd;

        public TacheDAO()
        {
            connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\oo\Documents\esisa_4eme_annee\Porjet .NET\projet .NET\ProjetGL\Projet\data\db_GL\db_GL.mdf"";Integrated Security=True");
            connection.Open();
            command = new SqlCommand();
            command.Connection = connection;
        }

        public void Add(Tache entity)
        {
            command.Parameters.Clear();
            command.CommandText = @"
                INSERT INTO Tache (descriptionTache, pourcentageAvancement, developpeurId, serviceId, nom)
                VALUES (@descriptionTache, @pourcentageAvancement, @developpeurId, @serviceId, @nom)";

            command.Parameters.AddWithValue("@descriptionTache", entity.DescriptionTache ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@pourcentageAvancement", entity.PourcentageAvancement);
            command.Parameters.AddWithValue("@developpeurId", entity.Developpeur?.Id ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@serviceId", entity.Service?.Id ?? throw new ArgumentNullException("Le service associé ne peut pas être nul."));
            command.Parameters.AddWithValue("@nom", entity.Nom ?? (object)DBNull.Value);

            command.ExecuteNonQuery();
        }


        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public List<Tache> GetAll()
        {
            throw new NotImplementedException();
        }

        public Tache GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Tache GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(Tache entity)
        {
            command.Parameters.Clear();
            command.CommandText = @"
                    UPDATE Tache 
                    SET 
                        DescriptionTache = @descriptionTache,
                        PourcentageAvancement = @pourcentageAvancement,
                        IdDeveloppeur = @developpeurId,
                        IdService = @serviceId,
                        Nom = @nom
                    WHERE Id = @id";
            command.Parameters.AddWithValue("@id", entity.Id);
            command.Parameters.AddWithValue("@descriptionTache", entity.DescriptionTache ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@pourcentageAvancement", entity.PourcentageAvancement);
            command.Parameters.AddWithValue("@developpeurId", entity.Developpeur?.Id ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@serviceId", entity.Service?.Id ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@nom", entity.Nom ?? (object)DBNull.Value);
            command.ExecuteNonQuery();
        }

        public List<Tache> GetByIdDev(ServiceProjet ser)
        {
            List<Tache> taches = new List<Tache>();

            try
            {
                command.Parameters.Clear();
                command.CommandText = @"
                    SELECT t.id, t.nom, t.descriptionTache, t.pourcentageAvancement, t.developpeurId, t.serviceId
                    FROM Tache t
                    WHERE t.IdService = @serviceId and t.developpeurId=@DevId";

                command.Parameters.AddWithValue("@serviceId", ser.Id);
                command.Parameters.AddWithValue("@DevId", ser.DeveloppeurAssigne.Id);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Tache tache = new Tache
                        {
                            Id = reader.GetInt32(0),
                            Nom = reader.IsDBNull(1) ? null : reader.GetString(1),
                            DescriptionTache = reader.IsDBNull(2) ? null : reader.GetString(2),
                            Developpeur = ser.DeveloppeurAssigne,
                            PourcentageAvancement = reader.GetDouble(3),
                            Service = ser
                        };
                        taches.Add(tache);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur lors de la récupération des tâches par service: {ex.Message}");
                throw;
            }

            return taches;
        }
    }
}
