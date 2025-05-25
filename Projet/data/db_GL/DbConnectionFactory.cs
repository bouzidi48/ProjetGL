using Microsoft.Data.SqlClient;

namespace ProjetNet.data.db_GL
{
	public class DbConnectionFactory
	{
		public static SqlConnection GetOpenConnection()
		{
			var connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\oo\Documents\esisa_4eme_annee\Porjet .NET\projet .NET\ProjetGL\Projet\data\db_GL\db_GL.mdf"";Integrated Security=True");
			connection.Open();
			return connection;
		}
	}
}
