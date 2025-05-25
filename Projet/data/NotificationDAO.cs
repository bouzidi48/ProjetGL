using ProjetNet.Data;
using ProjetNet.Models;
using Microsoft.Data.SqlClient;
using ProjetNet.data.db_GL;

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
			connection = DbConnectionFactory.GetOpenConnection();
			command = new SqlCommand();
            utilisateurDAO = new UtilisateurDAO();
        }

        public void Add(Notification entity)
        {
            try
            {
				if (connection.State != System.Data.ConnectionState.Open)
				{
					connection = DbConnectionFactory.GetOpenConnection();

				}
				command.Connection = connection;
				command.Parameters.Clear();
                command.CommandText = @"INSERT INTO Notification (message, estLue, IdDestinataire) 
                            VALUES (@message, @estLue, @IdDestinataire)";

                command.Parameters.AddWithValue("@message", entity.Message ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@estLue", entity.EstLue);
                command.Parameters.AddWithValue("@IdDestinataire", entity.Destinataire?.Id ?? (object)DBNull.Value);

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

        public List<Notification> GetAll()
        {
            throw new NotImplementedException();
        }

        public Notification GetById(int id)
        {
            try
            {
				if (connection.State != System.Data.ConnectionState.Open)
				{
					connection = DbConnectionFactory.GetOpenConnection();

				}
				command.Connection = connection;
				command.Parameters.Clear();
                command.CommandText = @"SELECT * FROM Notification WHERE id = @id";
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
                        Destinataire = rd.IsDBNull(rd.GetOrdinal("IdDestinataire")) ? null : new Utilisateur { Id = rd.GetInt32(rd.GetOrdinal("IdDestinataire")) }
                    };
                }

                rd.Close();
                if (notification != null)
                {
                    if (notification.Destinataire != null)
                    {
                        notification.Destinataire = utilisateurDAO.GetById(notification.Destinataire.Id);
                    }
                }
                return notification;
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

		public Notification GetById(string id)
		{
			throw new NotImplementedException();
		}

		public List<Notification> GetByIdAndNotLue(int id)
        {
            try
            {
				if (connection.State != System.Data.ConnectionState.Open)
				{
					connection = DbConnectionFactory.GetOpenConnection();

				}
				command.Connection = connection;
				command.Parameters.Clear();
                command.CommandText = @"SELECT * FROM Notification WHERE IdDestinataire = @id AND estLue = 0";
                command.Parameters.AddWithValue("@id", id);

                rd = command.ExecuteReader();
                List<Notification> notifications = new List<Notification>();

                while (rd.Read())
                {
                    Notification notification = new Notification
                    {
                        Id = rd.GetInt32(rd.GetOrdinal("id")),
                        Message = rd.IsDBNull(rd.GetOrdinal("message")) ? null : rd.GetString(rd.GetOrdinal("message")),
                        EstLue = rd.IsDBNull(rd.GetOrdinal("estLue")) ? false : rd.GetBoolean(rd.GetOrdinal("estLue")),
                        Destinataire = rd.IsDBNull(rd.GetOrdinal("IdDestinataire")) ? null : new Utilisateur { Id = rd.GetInt32(rd.GetOrdinal("IdDestinataire")) },
                    };
                    notifications.Add(notification);
                }
                rd.Close();
                foreach (var item in notifications)
                {
                    if (item.Destinataire != null && item != null)
                    {
                        item.Destinataire = utilisateurDAO.GetById(item.Destinataire.Id);
                    }
                }
                return notifications;
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

		public void Update(Notification entity)
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
