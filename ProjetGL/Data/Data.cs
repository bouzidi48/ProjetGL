using Microsoft.Data.SqlClient;

namespace ProjetGL.Data
{
    public class Data
    {
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();

        public Data()
        {
            connection.ConnectionString = $@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BD;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            connection.Open();
            command.Connection = connection;
        }



        public void Add()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }


        public bool Exist()
        {
            throw new NotImplementedException();
        }

        public void Find(string login)
        {
            throw new NotImplementedException();
        }

        public void GetAccounts()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

    }
}
