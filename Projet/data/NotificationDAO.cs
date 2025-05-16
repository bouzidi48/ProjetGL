using ProjetNet.Data;
using ProjetNet.Models;
using Microsoft.Data.SqlClient;

namespace ProjetNet.data
{
    public class NotificationDAO : InterfaceCRUD<Notification>
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader rd;
        UtilisateurDAO utilisateurDAO = null;
        public NotificationDAO()
        {
            connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\oo\Documents\esisa_4eme_annee\Porjet .NET\projet .NET\ProjetGL\Projet\data\db_GL\db_GL.mdf"";Integrated Security=True");
            connection.Open();
            command = new SqlCommand();
            command.Connection = connection;
            utilisateurDAO = new UtilisateurDAO();
        }

        public void Add(Notification entity)
        {
            command.Parameters.Clear();
            command.CommandText = @"INSERT INTO Notification (message, estLue, IdDestinataire) 
                            VALUES (@message, @estLue, @IdDestinataire)";

            command.Parameters.AddWithValue("@message", entity.Message ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@estLue", entity.EstLue);
            command.Parameters.AddWithValue("@IdDestinataire", entity.Destinataire?.Id ?? (object)DBNull.Value);

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

        public List<Notification> GetAll()
        {
            throw new NotImplementedException();
        }

        public Notification GetById(int id)
        {
            command.Parameters.Clear();
            command.CommandText = @"SELECT * FROM Notification WHERE IdDestinataire = @id";
            command.Parameters.AddWithValue("@id", id);

            rd = command.ExecuteReader();
            Notification notification = null;

            if (rd.Read())
            {
                notification = new Notification
                {
                    Id = rd.GetInt32(rd.GetOrdinal("id")),
                    Message = rd.IsDBNull(rd.GetOrdinal("message")) ? null : rd.GetString(rd.GetOrdinal("message")),
                    EstLue = rd.IsDBNull(rd.GetOrdinal("estLue")) ? false : rd.GetBoolean(rd.GetOrdinal("estLue")),
                    Destinataire = rd.IsDBNull(rd.GetOrdinal("IdDestinataire")) ? null :
                        (Utilisateur)utilisateurDAO.GetById(rd.GetInt32(rd.GetOrdinal("IdDestinataire")))
                };
            }

            rd.Close();
            return notification;
        }

		public Notification GetById(string id)
		{
			throw new NotImplementedException();
		}

		public Notification GetByIdAndNotLue(int id)
        {
			command.Parameters.Clear();
			command.CommandText = @"SELECT * FROM Notification WHERE IdDestinataire = @id AND estLue = 0";
			command.Parameters.AddWithValue("@id", id);

			rd = command.ExecuteReader();
			Notification notification = null;

			if (rd.Read())
			{
				notification = new Notification
				{
					Id = rd.GetInt32(rd.GetOrdinal("id")),
					Message = rd.IsDBNull(rd.GetOrdinal("message")) ? null : rd.GetString(rd.GetOrdinal("message")),
					EstLue = rd.IsDBNull(rd.GetOrdinal("estLue")) ? false : rd.GetBoolean(rd.GetOrdinal("estLue")),
					Destinataire = rd.IsDBNull(rd.GetOrdinal("IdDestinataire")) ? null :
						(Utilisateur)utilisateurDAO.GetById(rd.GetInt32(rd.GetOrdinal("IdDestinataire")))
				};
			}

			rd.Close();
			return notification;
		}

		public void Update(Notification entity)
		{
			command.Parameters.Clear();
			command.CommandText = @"
        UPDATE Notification 
        SET 
            message = @message,
            estLue = @estLue,
            IdDestinataire = @destinataireId
        WHERE id = @id";

			command.Parameters.AddWithValue("@id", entity.Id);
			command.Parameters.AddWithValue("@message", entity.Message ?? (object)DBNull.Value);
			command.Parameters.AddWithValue("@estLue", entity.EstLue);
			command.Parameters.AddWithValue("@destinataireId", entity.Destinataire?.Id ?? (object)DBNull.Value);

			command.ExecuteNonQuery();
		}
	}
}
