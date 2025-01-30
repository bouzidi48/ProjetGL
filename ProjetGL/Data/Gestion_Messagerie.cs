using Microsoft.Data.SqlClient;
using ProjetGL.GlobalServices;
using ProjetGL.Interfaces;
using ProjetGL.Models;

namespace ProjetGL.Data
{
    public class Gestion_Messagerie : IGestion_Messagerie
    {
        public Gestion_Messagerie()
        {
            Messagerie.cp = GetMaxIdMessagerie();
        }

        public void checkMessagerie(Account recepteur, Account emeteur)
        {
            GlobalBD.Bd.Command.CommandText = $@"
            UPDATE Messagerie
            SET checkMessage = 1
            WHERE (idEmeteur = {emeteur.Id} AND idRecepteur = {recepteur.Id})";
            GlobalBD.Bd.Command.ExecuteNonQuery();
        }

        public void creerMessage(Messagerie messagerie)
        {
            GlobalBD.Bd.Command.CommandText = $@"insert into Messagerie (idMessagerie,idEmeteur,idRecepteur,Message) values({messagerie.Id},{messagerie.Emeteur.Id},{messagerie.Recepteur.Id},'{messagerie.Message}')";
            GlobalBD.Bd.Command.ExecuteNonQuery();

        }

        public int GetMaxIdMessagerie()
        {
            try
            {
                int maxId = 0;
                // Requête pour récupérer le maximum de IdUser
                GlobalBD.Bd.Command.CommandText = $@"SELECT ISNULL(MAX(IdMessagerie), 0) FROM Messagerie";

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
