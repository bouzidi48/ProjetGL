using ProjetNet.Data;
using ProjetNet.Models;
using Microsoft.Data.SqlClient;

namespace ProjetNet.data
{
    public class ServiceDAO : InterfaceCRUD<ServiceProjet>
    {

        SqlConnection connection;
        SqlCommand command;
        SqlDataReader rd;
        TacheDAO tacheDAO;
        DeveloppeurDAO developpeurDAO;
        ProjetDAO projetDAO;

        public ServiceDAO()
        {
            connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\oo\Documents\esisa_4eme_annee\Porjet .NET\projet .NET\ProjetGL\Projet\data\db_GL\db_GL.mdf"";Integrated Security=True");
            connection.Open();
            command = new SqlCommand();
            command.Connection = connection;
            tacheDAO = new TacheDAO();
            developpeurDAO = new DeveloppeurDAO();
            projetDAO = new ProjetDAO();
        }

        public void Add(ServiceProjet entity)
        {
            command.Parameters.Clear();
            command.CommandText = @"INSERT INTO Service (nom, descriptionService, dureeJours, developpeurAssigneId, projetId) 
                            VALUES (@nom, @descriptionService, @dureeJours, @developpeurAssigneId, @projetId)";

            command.Parameters.AddWithValue("@nom", entity.Nom ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@descriptionService", entity.DescriptionService ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@dureeJours", entity.DureeJours);
            command.Parameters.AddWithValue("@developpeurAssigneId", entity.DeveloppeurAssigne?.Id ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@projetId", entity.Projet?.Id ?? (object)DBNull.Value);

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

        public List<ServiceProjet> GetAll()
        {
            throw new NotImplementedException();
        }

		public ServiceProjet GetById(int id)
		{
			ServiceProjet service = null;

			command.Parameters.Clear();
			command.CommandText = @"
        SELECT s.id, s.nom, s.descriptionService, s.dureeJours, 
               s.developpeurAssigneId, s.projetId
        FROM Service s
        WHERE s.id = @id";
			command.Parameters.AddWithValue("@id", id);

			using (var reader = command.ExecuteReader())
			{
				if (reader.Read())
				{
					int? devId = reader.IsDBNull(reader.GetOrdinal("developpeurAssigneId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("developpeurAssigneId"));
					int projetId = reader.GetInt32(reader.GetOrdinal("projetId"));

					service = new ServiceProjet
					{
						Id = reader.GetInt32(reader.GetOrdinal("id")),
						Nom = reader.GetString(reader.GetOrdinal("nom")),
						DescriptionService = reader.IsDBNull(reader.GetOrdinal("descriptionService")) ? null : reader.GetString(reader.GetOrdinal("descriptionService")),
						DureeJours = reader.GetInt32(reader.GetOrdinal("dureeJours")),
						DeveloppeurAssigne = rd.IsDBNull(rd.GetOrdinal("developpeurAssigneId")) ? null : new Developpeur { Id = rd.GetInt32(rd.GetOrdinal("developpeurAssigneId")) },
						Projet = rd.IsDBNull(rd.GetOrdinal("projetId")) ? null : new Projet { Id = rd.GetInt32(rd.GetOrdinal("projetId")) }, // Assurez-vous que ProjetDAO a bien une méthode GetById
						Taches = new List<Tache>() // Sera remplie juste après
					};
				}
			}

			if (service != null)
			{
				service.Taches = tacheDAO.GetByIdService(service);
                if (service.DeveloppeurAssigne != null) {
                    service.DeveloppeurAssigne = developpeurDAO.GetById(service.DeveloppeurAssigne.Id); 
                }
                if(service.Projet != null)
                {
                    service.Projet = projetDAO.GetById(service.Projet.Id);

				} 
                
			}

			return service;
		}


		public ServiceProjet GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(ServiceProjet entity)
        {
            throw new NotImplementedException();
        }

        public List<ServiceProjet> getSerByDev(Developpeur dev)
        {
            List<ServiceProjet> services = new List<ServiceProjet>();

            command.Parameters.Clear();
            command.CommandText = @"
                SELECT s.id, s.nom, s.descriptionService, s.dureeJours, 
                       s.developpeurAssigneId, s.projetId
                FROM Service s
                WHERE s.developpeurAssigneId = @devId AND s.projetId = @projetId";

            command.Parameters.AddWithValue("@devId", dev.Id);
            command.Parameters.AddWithValue("@projetId", dev.Projet.Id);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var service = new ServiceProjet
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("id")),
                        Nom = reader.GetString(reader.GetOrdinal("nom")),
                        DescriptionService = reader.IsDBNull(reader.GetOrdinal("descriptionService")) ? null: reader.GetString(reader.GetOrdinal("descriptionService")),
                        DureeJours = reader.GetInt32(reader.GetOrdinal("dureeJours")),
                        DeveloppeurAssigne = dev, // On réutilise l'objet passé en paramètre
                        Projet = dev.Projet,
                        Taches = new List<Tache>() // Initialisation vide (à remplir si besoin plus tard)
                    };

                    services.Add(service);
                }
            }

            foreach (ServiceProjet service in services)
            {
                service.Taches = tacheDAO.GetByIdService(service);
            }
            return services;
        }


        public List<ServiceProjet> getSerByPro(Projet pro)
        {
            List<ServiceProjet> services = new List<ServiceProjet>();

            command.Parameters.Clear();
            command.CommandText = @"
                SELECT s.id, s.nom, s.descriptionService, s.dureeJours, 
                       s.developpeurAssigneId, s.projetId
                FROM Service s
                WHERE s.projetId = @projetId";

            command.Parameters.AddWithValue("@projetId", pro.Id);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var service = new ServiceProjet
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("id")),
                        Nom = reader.GetString(reader.GetOrdinal("nom")),
                        DescriptionService = reader.IsDBNull(reader.GetOrdinal("descriptionService")) ? null : reader.GetString(reader.GetOrdinal("descriptionService")),
                        DureeJours = reader.GetInt32(reader.GetOrdinal("dureeJours")),
                        DeveloppeurAssigne = reader.IsDBNull(reader.GetOrdinal("developpeurAssigneId")) ? null : new Developpeur { Id = reader.GetInt32(rd.GetOrdinal("developpeurAssigneId")) }, // On réutilise l'objet passé en paramètre
                        Projet = pro,
                        Taches = new List<Tache>() // Initialisation vide (à remplir si besoin plus tard)
                    };

                    services.Add(service);
                }
            }
            foreach (ServiceProjet service in services)
            {
                service.DeveloppeurAssigne = new DeveloppeurDAO().GetById(service.DeveloppeurAssigne.Id);
                service.Taches = tacheDAO.GetByIdService(service);
            }

            return services;
        }
    }
}
