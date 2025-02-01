using Microsoft.Data.SqlClient;
using ProjetGL.Models;
using ProjetGL.GlobalServices;
using ProjetGL.Interfaces;

namespace ProjetGL.Data
{
    public class Gestion_Proposition : IGestion_Proposition
    {

        public Gestion_Proposition()
        {
        }

		public void AddProposition(Proposition proposition)
		{

			GlobalBD.Bd.Command.CommandText = $@"INSERT INTO Proposition (PropositionId, EmailFournisseur, AppelId, DateSoumission, PrixTotal, DateLivraisonEstimee, Garantie, Status) VALUES ('{proposition.PropositionId}', '{proposition.EmailFournisseur}', '{proposition.AppelId}', '{proposition.DateSoumission}', '{proposition.PrixTotal}', '{proposition.DateLivraisonEstimee}', '{proposition.Garantie}', '{proposition.Status}')";
			GlobalBD.Bd.Command.ExecuteNonQuery();  // Exécuter la commande

		}

		public Proposition FindProposition(int id)
		{
			GlobalBD.Bd.Command.CommandText = $@"SELECT * FROM Proposition WHERE PropositionId = '{id}'";
			SqlDataReader rs = GlobalBD.Bd.Command.ExecuteReader();

			if (rs.Read())
			{
				Proposition proposition = new Proposition
				{
					PropositionId = (int)rs["PropositionId"],
					EmailFournisseur = (string)rs["EmailFournisseur"],
					AppelId = (int)rs["AppelId"],
					DateSoumission = (DateTime)rs["DateSoumission"],
					PrixTotal = (decimal)rs["PrixTotal"],
					DateLivraisonEstimee = (DateTime)rs["DateLivraisonEstimee"],
					Garantie = rs["Garantie"].ToString(),
					Status = (string)rs["Status"]
				};

				rs.Close();
				return proposition;
			}

			rs.Close();
			return null;
		}


		public List<Proposition> GetAllPropositions()
		{
			List<Proposition> propositions = new List<Proposition>();

			GlobalBD.Bd.Command.CommandText = "SELECT * FROM Proposition";
			SqlDataReader rs = GlobalBD.Bd.Command.ExecuteReader();

			while (rs.Read())
			{
				Proposition proposition = new Proposition
				{
					PropositionId = (int)rs["PropositionId"],
					EmailFournisseur = (string)rs["EmailFournisseur"],
					AppelId = (int)rs["AppelId"],
					DateSoumission = (DateTime)rs["DateSoumission"],
					PrixTotal = (decimal)rs["PrixTotal"],
					DateLivraisonEstimee = (DateTime)rs["DateLivraisonEstimee"],
					Garantie = (string)rs["Garantie"],
					Status = (string)rs["Status"]
				};
				propositions.Add(proposition);
			}

			rs.Close();
			return propositions;
		}



	}
}
