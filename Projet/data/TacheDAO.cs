using ProjetNet.Data;
using ProjetNet.Models;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using ProjetNet.data.db_GL;

namespace ProjetNet.data
{
    public class TacheDAO : InterfaceCRUD<Tache>
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader rd;
        
    

        public TacheDAO()
        {
			connection = DbConnectionFactory.GetOpenConnection();
			command = new SqlCommand();
            
        }

        public void Add(Tache entity)
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
                INSERT INTO Tache (descriptionTache, pourcentageAvancement, developpeurId, serviceId, nom)
                VALUES (@descriptionTache, @pourcentageAvancement, @developpeurId, @serviceId, @nom)";

                command.Parameters.AddWithValue("@descriptionTache", entity.DescriptionTache ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@pourcentageAvancement", entity.PourcentageAvancement);
                command.Parameters.AddWithValue("@developpeurId", entity.Developpeur?.Id ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@serviceId", entity.Service?.Id ?? throw new ArgumentNullException("Le service associé ne peut pas être nul."));
                command.Parameters.AddWithValue("@nom", entity.Nom ?? (object)DBNull.Value);

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
            try
            {
				if (connection.State != System.Data.ConnectionState.Open)
				{
					connection = DbConnectionFactory.GetOpenConnection();

				}
				command.Connection = connection;
				Tache tache = new Tache();


                command.Parameters.Clear();
                command.CommandText = @"
                    SELECT t.id, t.nom, t.descriptionTache, t.pourcentageAvancement, t.developpeurId, t.serviceId
                    FROM Tache t
                    WHERE t.id=@id";


                command.Parameters.AddWithValue("@id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        tache = new Tache
                        {
                            Id = reader.GetInt32(0),
                            Nom = reader.IsDBNull(1) ? null : reader.GetString(1),
                            DescriptionTache = reader.IsDBNull(2) ? null : reader.GetString(2),
                            Developpeur = new Developpeur { Id = reader.GetInt32(4) },
                            PourcentageAvancement = reader.GetDouble(3),
                            Service = new ServiceProjet { Id = reader.GetInt32(5) }
                        };
                    }
                }




                return tache;
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

        public Tache GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(Tache entity)
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
                    UPDATE Tache 
                    SET 
                        descriptionTache = @descriptionTache,
                        pourcentageAvancement = @pourcentageAvancement,
                        developpeurId = @developpeurId,
                        serviceId = @serviceId,
                        nom = @nom
                    WHERE Id = @id";
                command.Parameters.AddWithValue("@id", entity.Id);
                command.Parameters.AddWithValue("@descriptionTache", entity.DescriptionTache ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@pourcentageAvancement", entity.PourcentageAvancement);
                command.Parameters.AddWithValue("@developpeurId", entity.Developpeur?.Id ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@serviceId", entity.Service?.Id ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@nom", entity.Nom ?? (object)DBNull.Value);
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

        public List<Tache> GetByIdDeveloppeur(Developpeur dev)
        {
            try
            {
				if (connection.State != System.Data.ConnectionState.Open)
				{
					connection = DbConnectionFactory.GetOpenConnection();

				}
				command.Connection = connection;
				List<Tache> taches = new List<Tache>();

                
                    command.Parameters.Clear();
                    command.CommandText = @"
                    SELECT t.id, t.nom, t.descriptionTache, t.pourcentageAvancement, t.developpeurId, t.serviceId
                    FROM Tache t
                    WHERE t.developpeurId=@DevId";


                    command.Parameters.AddWithValue("@DevId", dev.Id);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Tache tache = new Tache
                            {
                                Id = reader.GetInt32(0),
                                Nom = reader.IsDBNull(1) ? null : reader.GetString(1),
                                DescriptionTache = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Developpeur = dev,
                                PourcentageAvancement = reader.GetDouble(3),
                                Service = new ServiceProjet { Id = reader.GetInt32(5) }
                            };
                            taches.Add(tache);
                        }
                    }
               


                return taches;
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

		public List<Tache> GetByIdService(ServiceProjet ser)
		{
            try
            {
				if (connection.State != System.Data.ConnectionState.Open)
				{
					connection = DbConnectionFactory.GetOpenConnection();
					
				}
				command.Connection = connection;
				List<Tache> taches = new List<Tache>();


                command.Parameters.Clear();
                command.CommandText = @"
                    SELECT id, nom, descriptionTache, pourcentageAvancement, developpeurId, serviceId
                    FROM Tache
                    WHERE serviceId = @serviceId and developpeurId=@DevId";

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


                return taches;
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
