using Microsoft.Data.SqlClient;
using ProjetGL.Models;
using ProjetGL.GlobalServices;
using ProjetGL.Interfaces;

namespace ProjetGL.Data
{
    public class Gestion_Fournisseur : IGestion_Fournisseur
    {
        public Gestion_Fournisseur()
        {
        }

		public void AddFournisseur(Fournisseur fournisseur)
		{
			GlobalBD.Bd.Command.CommandText = $@"insert into Fournisseur values ('{fournisseur.Email}', '{fournisseur.Password}', '{fournisseur.FournisseurName}', '{fournisseur.NomSociete}', '{fournisseur.Gerant}', '{fournisseur.Status}')";
			GlobalBD.Bd.Command.ExecuteNonQuery();
		}
		public Fournisseur FindFournisseur(string email)
		{
			GlobalBD.Bd.Command.CommandText = $@"SELECT * FROM Fournisseur WHERE Email='{email}'";
			SqlDataReader rs = GlobalBD.Bd.Command.ExecuteReader();

			if (rs.Read())
			{
				Fournisseur fournisseur = new Fournisseur
				{
					Email = (string)rs["Email"],
					Password = (string)rs["Password"],
				};
				rs.Close();
				return fournisseur;
			}

			rs.Close();
			return null;
		}


		public List<Fournisseur> GetAllFournisseurs()
		{
			GlobalBD.Bd.Command.CommandText = "SELECT * FROM Fournisseur";

			SqlDataReader rs = GlobalBD.Bd.Command.ExecuteReader();
			List<Fournisseur> fournisseurs = new List<Fournisseur>();

			while (rs.Read())
			{
				Fournisseur fournisseur = new Fournisseur
				{
					Email = rs["Email"].ToString(),
					Password = rs["Password"].ToString(),
					FournisseurName = rs["FournisseurName"].ToString(),
					NomSociete = rs["NomSociete"].ToString(),
					Gerant = rs["Gerant"].ToString(),
					Status = rs["Status"].ToString()
				};
				fournisseurs.Add(fournisseur);
			}

			rs.Close();
			return fournisseurs;
		}


		public void UpdateFournisseur(Fournisseur fournisseur, string Email)
		{
			GlobalBD.Bd.Command.CommandText = $@"UPDATE Fournisseur SET Password = '{fournisseur.Password}', FournisseurName = '{fournisseur.FournisseurName}', NomSociete = '{fournisseur.NomSociete}', Gerant = '{fournisseur.Gerant}', Status = '{fournisseur.Status} WHERE Email = '{Email}'";
			GlobalBD.Bd.Command.ExecuteNonQuery();
		}


	}
}
