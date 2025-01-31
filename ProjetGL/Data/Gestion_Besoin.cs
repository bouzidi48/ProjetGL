using Microsoft.Data.SqlClient;
using ProjetGL.GlobalServices;
using ProjetGL.Interfaces;
using ProjetGL.Models;

namespace ProjetGL.Data
{
	public class GestionBesoin : IGestion_Besoin
	{
		GestionBesoin()
		{
			Besoin.cp = GetMaxIdBesoin();
		}

		public void AddBesoin(Besoin besoin)
		{
			GlobalBD.Bd.Command.CommandText = $@"
			INSERT INTO Besoin (BesoinId, DepartementId, EnseignantId)
			VALUES ({besoin.Id}, {besoin.Departement.Id}, {besoin.Enseignant.Id})";
			GlobalBD.Bd.Command.ExecuteNonQuery();
			foreach (var mat in besoin.Materiels)
			{
				GlobalBD.Bd.Command.CommandText = $@"
				INSERT INTO Avoir (MaterielId, BesoinId)
				VALUES ({mat.MaterielId}, {besoin.Id})";
				GlobalBD.Bd.Command.ExecuteNonQuery();
			}
			
		}

		public void DelBesoin(int id)
		{
			GlobalBD.Bd.Command.CommandText = $@"
			DELETE FROM Avoir WHERE BesoinId = {id}";
			GlobalBD.Bd.Command.ExecuteNonQuery();

			GlobalBD.Bd.Command.CommandText = $@"
			DELETE FROM Besoin WHERE BesoinId = {id}";
			GlobalBD.Bd.Command.ExecuteNonQuery();
		}

		public bool ExistBesoin(int id)
		{
			GlobalBD.Bd.Command.CommandText = $@"
			SELECT COUNT(*) FROM Besoin WHERE BesoinId = {id}";
			return (int)GlobalBD.Bd.Command.ExecuteScalar() > 0;
		}

		public Besoin FindBesoin(int id)
		{
			GlobalBD.Bd.Command.CommandText = $@"
            SELECT * FROM Besoin WHERE BesoinId = {id}";
			SqlDataReader reader1 = GlobalBD.Bd.Command.ExecuteReader();
			if (reader1.Read())
			{

				GlobalBD.Bd.Command.CommandText = $@"
				SELECT * FROM Avoir WHERE BesoinId = {id}";
				SqlDataReader reader2 = GlobalBD.Bd.Command.ExecuteReader();
				int idMateriel = 0;
				List<Materiel> materiels = new List<Materiel>();
				while (reader2.Read())
				{
					idMateriel = (int)reader2["MaterielId"];
					Materiel m = ServicesPages.managerMateriel.FindMateriel(idMateriel);
					if (m != null)
					{
						materiels.Add(m);
					}
				}
				reader2.Close();
				//Departement departement = ServicesPages.managerDepartement.FindDepartement((int)reader1["DepartementId"]);
				//Enseignant enseignant = ServicesPages.managerEnseignant.FindEnseignant((int)reader1["EnseignantId"]);
				//Besoin besoin = new Besoin()



			}
			reader1.Close();
			return null;
		}

		public List<Besoin> GetBesoins()
		{
			throw new NotImplementedException();
		}

		public void UpdateBesoin(int id, Besoin newBesoin)
		{
			throw new NotImplementedException();
		}

		public int GetMaxIdBesoin()
		{
			try
			{
				int maxId = 0;
				// Requête pour récupérer le maximum de IdUser
				GlobalBD.Bd.Command.CommandText = $@"SELECT ISNULL(MAX(BesoinId), 0) FROM Besoin";

				SqlDataReader rs = GlobalBD.Bd.Command.ExecuteReader();
				if (rs.Read() == true)
				{
					maxId = (int)rs[0];
					rs.Close();
				}
				return maxId;

			}
			catch (Exception ex)
			{
				Console.WriteLine($"Erreur lors de la récupération du MaxIdUser : {ex.Message}");
				return 0; // Retourne 0 en cas d'erreur
			}
		}
	}
}
