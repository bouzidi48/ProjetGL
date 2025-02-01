using Microsoft.Data.SqlClient;
using ProjetGL.Models;
using ProjetGL.GlobalServices;
using ProjetGL.Interfaces;

namespace ProjetGL.Data
{
    public class Gestion_AppelOffre : IGestion_AppelOffre
	{
        public Gestion_AppelOffre()
        {
            
        }
		public void AddAppelOffre(AppelOffre appel, List<int> ressourcesIds)
		{
			GlobalBD.Bd.Command.CommandText = $@"
            INSERT INTO AppelDoffre (DateDebut, DateFin, Description) 
            VALUES ('{appel.DateDebut}', '{appel.DateFin}', '{appel.Description}');
            SELECT SCOPE_IDENTITY();";

			int nouvelAppelId = Convert.ToInt32(GlobalBD.Bd.Command.ExecuteScalar());

			foreach (var ressourceId in ressourcesIds)
			{
				GlobalBD.Bd.Command.CommandText = $@"
                INSERT INTO AppelDoffre_Ressources (AppelId, RessourceId) 
                VALUES ('{nouvelAppelId}', '{ressourceId}')";

				GlobalBD.Bd.Command.ExecuteNonQuery();
			}
		}


		public void DeleteAppelOffre(int appelId)
		{
			// Supprimer les ressources associées
			GlobalBD.Bd.Command.CommandText = $@"DELETE FROM AppelDoffre_Ressources WHERE AppelId = '{appelId}'";
			GlobalBD.Bd.Command.ExecuteNonQuery();

			// Supprimer l'appel d'offre lui-même
			GlobalBD.Bd.Command.CommandText = $@"DELETE FROM AppelDoffre WHERE AppelId = '{appelId}'";
			GlobalBD.Bd.Command.ExecuteNonQuery();
		}


		public List<AppelOffre> GetAllAppelOffre()
		{
			List<AppelOffre> appelsOffres = new List<AppelOffre>();

			GlobalBD.Bd.Command.CommandText = "SELECT * FROM AppelDoffre";
			SqlDataReader reader = GlobalBD.Bd.Command.ExecuteReader();

			while (reader.Read())
			{
				AppelOffre appel = new AppelOffre
				{
					AppelId = Convert.ToInt32(reader["AppelId"]),
					DateDebut = Convert.ToDateTime(reader["DateDebut"]),
					DateFin = Convert.ToDateTime(reader["DateFin"]),
					Description = reader["Description"].ToString(),
					Ressources = new List<Ressource>()
				};

				appelsOffres.Add(appel);
			}

			reader.Close();

			// Charger les ressources associées
			foreach (var appel in appelsOffres)
			{
				appel.Ressources = GetRessourcesByAppelId(appel.AppelId);
			}

			return appelsOffres;
		}


		public AppelOffre FindAppelOffre(int appelId)
		{
			AppelOffre appel = null;

			GlobalBD.Bd.Command.CommandText = $@"SELECT * FROM AppelDoffre WHERE AppelId = '{appelId}'";
			SqlDataReader reader = GlobalBD.Bd.Command.ExecuteReader();

			if (reader.Read())
			{
				appel = new AppelOffre
				{
					AppelId = Convert.ToInt32(reader["AppelId"]),
					DateDebut = Convert.ToDateTime(reader["DateDebut"]),
					DateFin = Convert.ToDateTime(reader["DateFin"]),
					Description = reader["Description"].ToString(),
					Ressources = new List<Ressource>() // Initialisation de la liste
				};
			}

			reader.Close();

			// Charger les ressources associées
			if (appel != null)
			{
				appel.Ressources = GetRessourcesByAppelId(appelId); // ✅ Charger les ressources associées
			}

			return appel;
		}


		public List<Ressource> GetRessourcesByAppelId(int appelId)
		{
			List<Ressource> ressources = null;

			GlobalBD.Bd.Command.CommandText = @$"SELECT r.* FROM Ressources r INNER JOIN AppelDoffre_Ressources ar ON r.RessourceId = ar.RessourceId WHERE ar.AppelId = '{appelId}'";
			GlobalBD.Bd.Command.Parameters.Clear();

			SqlDataReader reader = GlobalBD.Bd.Command.ExecuteReader();

			if (reader.HasRows)
			{
				ressources = new List<Ressource>();
				while (reader.Read())
				{
					Ressource ressource = new Ressource
					{
						RessourceId = Convert.ToInt32(reader["RessourceId"]),
						Type = reader["Type"].ToString(),
						Marque = reader.IsDBNull(reader.GetOrdinal("Marque")) ? null : reader["Marque"].ToString(),
						RAM = reader.IsDBNull(reader.GetOrdinal("RAM")) ? (int?)null : Convert.ToInt32(reader["RAM"]),
						CPU = reader.IsDBNull(reader.GetOrdinal("CPU")) ? null : reader["CPU"].ToString(),
						DisqueDur = reader.IsDBNull(reader.GetOrdinal("DisqueDur")) ? null : reader["DisqueDur"].ToString(),
						Ecran = reader.IsDBNull(reader.GetOrdinal("Ecran")) ? null : reader["Ecran"].ToString(),
						VitesseImpression = reader.IsDBNull(reader.GetOrdinal("VitesseImpression")) ? null : reader["VitesseImpression"].ToString(),
						Resolution = reader.IsDBNull(reader.GetOrdinal("Resolution")) ? null : reader["Resolution"].ToString()
					};

					ressources.Add(ressource);
				}
			}

			reader.Close();
			return ressources;
		}

		public List<Ressource> GetRessourcesByAppelOffre(int appelId)
		{
			List<Ressource> ressources = new List<Ressource>();

			GlobalBD.Bd.Command.CommandText = $@"SELECT r.RessourceId, r.Type, r.Marque FROM Ressources r JOIN AppelDoffre_Ressources ar ON r.RessourceId = ar.RessourceId WHERE ar.AppelId = {appelId}";

			SqlDataReader reader = GlobalBD.Bd.Command.ExecuteReader();

			while (reader.Read())
			{
				Ressource ressource = new Ressource
				{
					RessourceId = reader.GetInt32(reader.GetOrdinal("RessourceId")),
					Type = reader.GetString(reader.GetOrdinal("Type")),
					Marque = reader.IsDBNull(reader.GetOrdinal("Marque")) ? null : reader.GetString(reader.GetOrdinal("Marque"))
				};
				ressources.Add(ressource);
			}

			reader.Close();
			return ressources;
		}


		
    }
}
