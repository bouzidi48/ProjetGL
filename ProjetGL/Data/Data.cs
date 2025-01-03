using Microsoft.Data.SqlClient;

namespace ProjetGL.Data
{
    public class Data
    {
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();

        public Data()
        {
            connection.ConnectionString = $@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BDGL;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            connection.Open();
            command.Connection = connection;
        }




    }
}
