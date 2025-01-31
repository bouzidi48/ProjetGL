using Microsoft.Data.SqlClient;
using ProjetGL.GlobalServices;
using ProjetGL.Interfaces;
using ProjetGL.Models;

namespace ProjetGL.Data
{
	public class Gestion_Materiel : IGestion_Materiel
	{

		public Gestion_Materiel()
		{
            Materiel.cp = GetMaxIdMateriel();
        }
        public void AddImprimante(Imprimante imprimante)
        {
			GlobalBD.Bd.Command.CommandText = $@"
			INSERT INTO Materiels (MaterielId, TypeMateriel, Marque)
			VALUES ({imprimante.MaterielId}, 'Imprimante', '{imprimante.Marque}')";
			GlobalBD.Bd.Command.ExecuteNonQuery();

			GlobalBD.Bd.Command.CommandText = $@"
			INSERT INTO Imprimantes (ImprimanteId, VitesseImpression, Resolution, MaterielId)
			VALUES ({imprimante.ImprimanteId}, '{imprimante.VitesseImprimente}', '{imprimante.Resolution}', {imprimante.MaterielId})";
			GlobalBD.Bd.Command.ExecuteNonQuery();
        }



        public void AddOrdinateur(Ordinateur ordinateur)
        {
			GlobalBD.Bd.Command.CommandText = $@"
			INSERT INTO Materiels (MaterielId, TypeMateriel, Marque)
			VALUES ({ordinateur.MaterielId}, 'Ordinateur', '{ordinateur.Marque}')";
			GlobalBD.Bd.Command.ExecuteNonQuery();

			GlobalBD.Bd.Command.CommandText = $@"
			INSERT INTO Ordinateurs (OrdinateurId, CPU, RAM, DisqueDur, Ecran, MaterielId)
			VALUES ({ordinateur.OrdinateurId}, '{ordinateur.Cpu}', '{ordinateur.Ram}', '{ordinateur.DisqueDur}', '{ordinateur.Ecran}', {ordinateur.MaterielId})";
			GlobalBD.Bd.Command.ExecuteNonQuery();
        }


        public void DelImprimante(int id)
        {
			GlobalBD.Bd.Command.CommandText = $@"
			DELETE FROM Materiels WHERE MaterielId = {id}";
			GlobalBD.Bd.Command.ExecuteNonQuery();
        }

        public void DelOrdinateur(int id)
        {
			GlobalBD.Bd.Command.CommandText = $@"
			DELETE FROM Materiels WHERE MaterielId = {id}";
			GlobalBD.Bd.Command.ExecuteNonQuery();
        }

        public bool ExistImprimante(int id)
        {
			GlobalBD.Bd.Command.CommandText = $@"
			SELECT COUNT(*) FROM Imprimantes WHERE ImprimanteId = {id}";
			return (int)GlobalBD.Bd.Command.ExecuteScalar() > 0;
        }

        public bool ExistOrdinateur(int id)
        {
			GlobalBD.Bd.Command.CommandText = $@"
			SELECT COUNT(*) FROM Ordinateurs WHERE OrdinateurId = {id}";
			return (int)GlobalBD.Bd.Command.ExecuteScalar() > 0;
        }

        public Imprimante FindImprimante(int id)
        {
            GlobalBD.Bd.Command.CommandText = $@"
            SELECT * FROM Imprimantes WHERE ImprimanteId = {id}";
            SqlDataReader reader1 = GlobalBD.Bd.Command.ExecuteReader();
            if (reader1.Read())
            {
                GlobalBD.Bd.Command.CommandText = $@"
                SELECT * FROM Materiels WHERE MaterielId = {reader1["MaterielId"]}";
                SqlDataReader reader2 = GlobalBD.Bd.Command.ExecuteReader();
                if(reader2.Read())
                {
                    Imprimante imprimante = new Imprimante { MaterielId = (int)reader1["MaterielId"], Marque = reader2["Marque"].ToString(), Type = (TypeMateriel)Enum.Parse(typeof(Type), reader2["TypeMateriel"].ToString()), ImprimanteId = (int)reader1["ImprimanteId"], VitesseImprimente = reader1["VitesseImpression"].ToString(), Resolution = reader1["Resolution"].ToString() };
                    reader2.Close();
                    return imprimante;
                    
                }
                reader2.Close();
                reader1.Close();
 
                
            }
            reader1.Close();
            return null;
        }

        public Ordinateur FindOrdinateur(int id)
        {
            GlobalBD.Bd.Command.CommandText = $@"
            SELECT * FROM Ordinateurs WHERE OrdinateurId = {id}";
            SqlDataReader reader1 = GlobalBD.Bd.Command.ExecuteReader();
            if (reader1.Read())
            {
                GlobalBD.Bd.Command.CommandText = $@"
                SELECT * FROM Materiels WHERE MaterielId = {reader1["MaterielId"]}";
                SqlDataReader reader2 = GlobalBD.Bd.Command.ExecuteReader();
                if (reader2.Read())
                {
                    Ordinateur ordinateur = new Ordinateur { MaterielId = (int)reader1["MaterielId"], Marque = reader2["Marque"].ToString(), Type = (TypeMateriel)Enum.Parse(typeof(Type), reader2["TypeMateriel"].ToString()), OrdinateurId = (int)reader1["OrdinateurId"], Cpu = reader1["CPU"].ToString(), Ram = reader1["RAM"].ToString(), DisqueDur = reader1["DisqueDur"].ToString(), Ecran = reader1["Ecran"].ToString() };
                    reader2.Close();
                    return ordinateur;
                }
                reader2.Close();
                reader1.Close();

            }
            reader1.Close();
            return null;
        }

        public Materiel FindMateriel(int id)
		{
			GlobalBD.Bd.Command.CommandText = $@"
            SELECT * FROM Materiels WHERE MaterielId = {id}";
			SqlDataReader reader = GlobalBD.Bd.Command.ExecuteReader();
			if (reader.Read())
			{
				if (ExistImprimante(id))
				{
					reader.Close();
					return FindImprimante(id);
				}
				else if (ExistOrdinateur(id))
				{
					reader.Close();
					return FindOrdinateur(id);
				}
			}
			reader.Close();
			return null;
		}


		public List<Imprimante> GetImprimants()
		{
            List<Imprimante> list = new List<Imprimante>();
            GlobalBD.Bd.Command.CommandText = $@"
            SELECT * FROM Imprimantes";
            SqlDataReader reader1 = GlobalBD.Bd.Command.ExecuteReader();
            while(reader1.Read())
            {
                GlobalBD.Bd.Command.CommandText = $@"
                SELECT * FROM Materiels WHERE MaterielId = {reader1["MaterielId"]}";
                SqlDataReader reader2 = GlobalBD.Bd.Command.ExecuteReader();
                if (reader2.Read())
                {
                    Imprimante imprimante = new Imprimante { MaterielId = (int)reader1["MaterielId"], Marque = reader2["Marque"].ToString(), Type = (TypeMateriel)Enum.Parse(typeof(Type), reader2["TypeMateriel"].ToString()), ImprimanteId = (int)reader1["ImprimanteId"], VitesseImprimente = reader1["VitesseImpression"].ToString(), Resolution = reader1["Resolution"].ToString() };
                    list.Add(imprimante);
                    reader2.Close();
                }
                reader2.Close();
            }
            reader1.Close();
            return list;
        }

		public List<Ordinateur> GetOrdinateurs()
		{
            List<Ordinateur> list = new List<Ordinateur>();
            GlobalBD.Bd.Command.CommandText = $@"
            SELECT * FROM Ordinateurs";
            SqlDataReader reader1 = GlobalBD.Bd.Command.ExecuteReader();
            while(reader1.Read())
            {
                GlobalBD.Bd.Command.CommandText = $@"
                SELECT * FROM Materiels WHERE MaterielId = {reader1["MaterielId"]}";
                SqlDataReader reader2 = GlobalBD.Bd.Command.ExecuteReader();
                if (reader2.Read())
                {
                    Ordinateur ordinateur = new Ordinateur { MaterielId = (int)reader1["MaterielId"], Marque = reader2["Marque"].ToString(), Type = (TypeMateriel)Enum.Parse(typeof(Type), reader2["TypeMateriel"].ToString()), OrdinateurId = (int)reader1["OrdinateurId"], Cpu = reader1["CPU"].ToString(), Ram = reader1["RAM"].ToString(), DisqueDur = reader1["DisqueDur"].ToString(), Ecran = reader1["Ecran"].ToString() };
                    list.Add(ordinateur);
                    reader2.Close();
                }
                reader2.Close();

            }
            reader1.Close();
            return list;
        }

        public void UpdateImprimante(int id, Imprimante newImprimante)
        {
            Imprimante imprimante = FindImprimante(id);
            GlobalBD.Bd.Command.CommandText = $@"
            UPDATE Materiels SET Marque = '{newImprimante.Marque}', TypeMateriel = 'Imprimante'
            WHERE MaterielId = {imprimante.MaterielId}";
            GlobalBD.Bd.Command.ExecuteNonQuery();

            GlobalBD.Bd.Command.CommandText = $@"
            UPDATE Imprimantes SET VitesseImpression = '{newImprimante.VitesseImprimente}', Resolution = '{newImprimante.Resolution}'
            WHERE ImprimanteId = {id}";
            GlobalBD.Bd.Command.ExecuteNonQuery();
        }

        public void UpdateOrdinateur(int id, Ordinateur newOrdinateur)
        {
            Ordinateur ordinateur = FindOrdinateur(id);
            GlobalBD.Bd.Command.CommandText = $@"
            UPDATE Materiels SET Marque = '{newOrdinateur.Marque}', TypeMateriel = 'Ordinateur'
            WHERE MaterielId = {ordinateur.MaterielId}";
            GlobalBD.Bd.Command.ExecuteNonQuery();

            GlobalBD.Bd.Command.CommandText = $@"
            UPDATE Ordinateurs SET CPU = '{newOrdinateur.Cpu}', RAM = '{newOrdinateur.Ram}', DisqueDur = '{newOrdinateur.DisqueDur}', Ecran = '{newOrdinateur.Ecran}'
            WHERE OrdinateurId = {id}";
            GlobalBD.Bd.Command.ExecuteNonQuery();
        }


        public int GetMaxIdMateriel()
        {
            try
            {
                int maxId = 0;
                // Requête pour récupérer le maximum de IdUser
                GlobalBD.Bd.Command.CommandText = $@"SELECT ISNULL(MAX(MaterielId), 0) FROM Materiels";

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
