using Microsoft.Data.SqlClient;
using ProjetGL.Models;
using ProjetGL.GlobalServices;
using ProjetGL.Interfaces;

namespace ProjetGL.Data
{
	public class Gestion_Ressource : IGestion_Ressource
	{



		public Gestion_Ressource()
		{
		}
		public void AjouterRessource(Ressource ressource)
		{
			if (ressource == null)
			{
				throw new ArgumentNullException(nameof(ressource), "La ressource ne peut pas être null.");
			}

			GlobalBD.Bd.Command.CommandText = $@"INSERT INTO Ressources (Type, Marque, RAM, CPU, DisqueDur, Ecran, VitesseImpression, Resolution, EnseignantId, Prix, FournisseurMarque, Garantie, Total, IsValidated) VALUES 
                                ('{ressource.Type}', '{ressource.Marque}', '{ressource.RAM}', '{ressource.CPU}', '{ressource.DisqueDur}', '{ressource.Ecran}', '{ressource.VitesseImpression}', '{ressource.Resolution}', '{ressource.EnseignantId}', '{ressource.Prix}', '{ressource.FournisseurMarque}', '{ressource.Garantie}', '{ressource.Total}', '{ressource.IsValidated}')";

			GlobalBD.Bd.Command.ExecuteNonQuery();
		}

		public void SupprimerRessource(int ressourceId)
		{
			GlobalBD.Bd.Command.CommandText = $"DELETE FROM Ressources WHERE RessourceId = '{ressourceId}'";
			GlobalBD.Bd.Command.ExecuteNonQuery();
		}


		public List<Ressource> GetAllRessources()
		{
			List<Ressource> ressources = new List<Ressource>();

			GlobalBD.Bd.Command.CommandText = "SELECT * FROM Ressources"; // Assurez-vous que "Ressources" est le nom correct de votre table

			SqlDataReader reader = GlobalBD.Bd.Command.ExecuteReader();

			while (reader.Read())
			{
				ressources.Add(new Ressource
				{
					RessourceId = reader.GetInt32(0), // Colonne 1 : RessourceId
					Type = reader.GetString(1), // Colonne 2 : Type
					Marque = reader.IsDBNull(2) ? null : reader.GetString(2), // Colonne 3 : Marque
					RAM = reader.IsDBNull(3) ? null : reader.GetInt32(3), // Colonne 4 : RAM
					CPU = reader.IsDBNull(4) ? null : reader.GetString(4), // Colonne 5 : CPU
					DisqueDur = reader.IsDBNull(5) ? null : reader.GetString(5), // Colonne 6 : DisqueDur
					Ecran = reader.IsDBNull(6) ? null : reader.GetString(6), // Colonne 7 : Ecran
					VitesseImpression = reader.IsDBNull(7) ? null : reader.GetString(7), // Colonne 8 : VitesseImpression
					Resolution = reader.IsDBNull(8) ? null : reader.GetString(8), // Colonne 9 : Resolution
					EnseignantId = reader.GetInt32(9), // Colonne 10 : EnseignantId
					Prix = reader.IsDBNull(10) ? null : reader.GetDecimal(10), // Colonne 11 : Prix
					FournisseurMarque = reader.IsDBNull(11) ? null : reader.GetString(11), // Colonne 12 : FournisseurMarque
					Garantie = reader.IsDBNull(12) ? null : reader.GetString(12), // Colonne 13 : Garantie
					Total = reader.IsDBNull(13) ? null : reader.GetDecimal(13), // Colonne 14 : Total
					IsValidated = reader.GetBoolean(14) // Colonne 15 : IsValidated
				});
			}
			return ressources;
		}

		public void AffecterRessource(AffectationRessource affectation)
		{

			GlobalBD.Bd.Command.CommandText = $@"INSERT INTO AffectationRessource (RessourceId, DepartementId, EnseignantId, DateAffectation) VALUES ('{affectation.RessourceId}', '{affectation.DepartementId}', {(affectation.EnseignantId == null ? "NULL" : $"'{affectation.EnseignantId}'")}, '{affectation.DateAffectation}')";
			GlobalBD.Bd.Command.ExecuteNonQuery();
		}


		public List<Enseignant> GetAllEnseignants()
		{
			List<Enseignant> enseignants = new List<Enseignant>();

			GlobalBD.Bd.Command.CommandText = "SELECT Id, Name, Email FROM Enseignant";
			SqlDataReader reader = GlobalBD.Bd.Command.ExecuteReader();

			while (reader.Read())
			{
				enseignants.Add(new Enseignant
				{
					Id = reader.GetInt32(0),
					Name = reader.GetString(1),
					Email = reader.GetString(2)
				});
			}
			return enseignants;
		}


		public List<Departement> GetAllDepartements()
		{
			List<Departement> departements = new List<Departement>();

			GlobalBD.Bd.Command.CommandText = "SELECT Id, Name FROM Departments";
			SqlDataReader reader = GlobalBD.Bd.Command.ExecuteReader();

			while (reader.Read())
			{
				departements.Add(new Departement
				{
					Id = reader.GetInt32(0),
					Name = reader.GetString(1)
				});
			}

			return departements;
		}


	}

}
