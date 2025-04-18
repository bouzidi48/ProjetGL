using Microsoft.Data.SqlClient;

namespace ProjetGL.Data
{
    public class BD
    {
        private SqlConnection connection = new SqlConnection();
        private SqlCommand command = new SqlCommand();

        public BD()
        {
            Connection.ConnectionString = $@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BDGL;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            Connection.Open();
            Command.Connection = Connection;
        }

        public SqlConnection Connection { get => connection; set => connection = value; }
        public SqlCommand Command { get => command; set => command = value; }
    }
}
