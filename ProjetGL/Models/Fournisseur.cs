namespace ProjetGL.Models
{
	public class Fournisseur:Account
	{
		private static int cp = 0;
		private int id;
		private string lieu;
		private string adresse;
		private string siteInternet;
		private string gerant;
		private int blackListe;
		private string motif;


		public int Id { get => id; set => id = value; }
		public string Lieu { get => lieu; set => lieu = value; }
		public string Adresse { get => adresse; set => adresse = value; }
		public string SiteInternet { get => siteInternet; set => siteInternet = value; }
		public string Gerant { get => gerant; set => gerant = value; }
		public int BlackListe { get => blackListe; set => blackListe = value; }
		public string Motif { get => motif; set => motif = value; }

		public Fournisseur(string username, string password, Role userRole, string lieu, string adresse, string siteInternet, string gerant, int blackListe, string motif) : base(username, password, userRole)
		{
			cp++;
			Id = cp;
			Lieu = lieu;
			Adresse = adresse;
			SiteInternet = siteInternet;
			Gerant = gerant;
			BlackListe = blackListe;
			Motif = motif;
		}
	}
}
