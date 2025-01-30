namespace ProjetGL.Models
{
	public class Proposition
	{
		private static int cp = 0;
		private int id;
		private Fournisseur fournisseur;
		private DateTime dateLivraison;
		private int dureeGarantie;
		private string marque;
		private double prix;
		private double totale;
		private AppelOffre appelOffre;
		private int accepte=-1;

		public int Id { get => id; set => id = value; }
		public Fournisseur Fournisseur { get => fournisseur; set => fournisseur = value; }
		public DateTime DateLivraison { get => dateLivraison; set => dateLivraison = value; }
		public int DureeGarantie { get => dureeGarantie; set => dureeGarantie = value; }
		public string Marque { get => marque; set => marque = value; }
		public double Prix { get => prix; set => prix = value; }
		public double Totale { get => totale; set => totale = value; }
		public AppelOffre AppelOffre { get => appelOffre; set => appelOffre = value; }
		public int Accepte { get => accepte; set => accepte = value; }

		public Proposition(Fournisseur fournisseur, DateTime dateLivraison, int dureeGarantie, string marque, double prix, double totale, AppelOffre appelOffre)
		{
			cp = cp + 1;
			id = cp;
			Fournisseur = fournisseur;
			DateLivraison = dateLivraison;
			DureeGarantie = dureeGarantie;
			Marque = marque;
			Prix = prix;
			Totale = totale;
			AppelOffre = appelOffre;
		}
	}
}
