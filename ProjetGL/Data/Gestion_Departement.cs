using Microsoft.Data.SqlClient;
using ProjetGL.GlobalServices;
using ProjetGL.Interfaces;
using ProjetGL.Models;

namespace ProjetGL.Data
{
	public class Gestion_Departement:IGestion_Departement
	{
		public Gestion_Departement()
		{
		}

		public bool ExistDepartement(int id)
		{
			GlobalBD.Bd.Command.CommandText = $@"
			SELECT COUNT(*) FROM Departement WHERE DepartementId = {id}";
			return (int)GlobalBD.Bd.Command.ExecuteScalar() > 0;
		}

		public Departement FindDepartement(int id)
		{
			GlobalBD.Bd.Command.CommandText = $@"
				SELECT * FROM Departement WHERE DepartementId = {id}";
			SqlDataReader reader2 = GlobalBD.Bd.Command.ExecuteReader();
			if(reader2.Read())
			{
				Departement departement = new Departement ((int)reader2["DepartementId"],reader2["DepartementNom"].ToString(),(double)reader2["DepartementBudget"] );
				reader2.Close();
				return departement;

			}
			reader2.Close();
			return null;
		}

		public List<Departement> GetDepartements()
		{
			List<Departement> departements = new List<Departement>();
			GlobalBD.Bd.Command.CommandText = $@"
            SELECT * FROM Imprimantes";
			SqlDataReader reader1 = GlobalBD.Bd.Command.ExecuteReader();
			while (reader1.Read())
			{
				Departement departement = new Departement((int)reader1["DepartementId"], reader1["DepartementNom"].ToString(), (double)reader1["DepartementBudget"]);
				departements.Add(departement);
			}
			reader1.Close();
			return departements;
		}
	}
}
