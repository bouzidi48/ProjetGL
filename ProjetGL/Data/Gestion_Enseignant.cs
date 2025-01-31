using Microsoft.Data.SqlClient;
using ProjetGL.GlobalServices;
using ProjetGL.Interfaces;
using ProjetGL.Models;

namespace ProjetGL.Data
{
	public class Gestion_Enseignant: IGestion_Enseignant
	{
		public Gestion_Enseignant()
		{

		}

		public bool ExistEnseignant(int id)
		{
			GlobalBD.Bd.Command.CommandText = $@"
			SELECT COUNT(*) FROM Enseignant WHERE EnseignantId = {id}";
			return (int)GlobalBD.Bd.Command.ExecuteScalar() > 0;
		}

		public Enseignant FindEnseignant(int id)
		{
			GlobalBD.Bd.Command.CommandText = $@"
            SELECT * FROM Enseignant WHERE EnseignantId = {id}";
			SqlDataReader reader1 = GlobalBD.Bd.Command.ExecuteReader();
			if (reader1.Read())
			{
				GlobalBD.Bd.Command.CommandText = $@"
                SELECT * FROM account WHERE id = {reader1["AccountId"]}";
				SqlDataReader reader2 = GlobalBD.Bd.Command.ExecuteReader();
				if (reader2.Read())
				{
					Departement departement = ServicesPages.managerDepartement.FindDepartement((int)reader1["DepartementId"]);
					Enseignant enseignant = new Enseignant((int)reader2["Id"], reader2["Username"].ToString(), reader2["Password"].ToString(), (Role)Enum.Parse(typeof(Role), reader2["UserRole"].ToString()), departement);
					reader2.Close();
					return enseignant;
				}
				reader2.Close();
				reader1.Close();

			}
			reader1.Close();
			return null;
		}

		public List<Enseignant> GeEnseignants()
		{
			GlobalBD.Bd.Command.CommandText = $@"
            SELECT * FROM Enseignant";
			SqlDataReader reader1 = GlobalBD.Bd.Command.ExecuteReader();
			List<Enseignant> enseignants = new List<Enseignant>();
			while (reader1.Read())
			{
				GlobalBD.Bd.Command.CommandText = $@"
                SELECT * FROM account WHERE id = {reader1["AccountId"]}";
				SqlDataReader reader2 = GlobalBD.Bd.Command.ExecuteReader();
				if (reader2.Read())
				{
					Departement departement = ServicesPages.managerDepartement.FindDepartement((int)reader1["DepartementId"]);
					Enseignant enseignant = new Enseignant((int)reader2["Id"], reader2["Username"].ToString(), reader2["Password"].ToString(), (Role)Enum.Parse(typeof(Role), reader2["UserRole"].ToString()), departement);
					reader2.Close();
					enseignants.Add(enseignant);
				}
				reader2.Close();

			}
			reader1.Close();

			return enseignants;
		}
	}
}
