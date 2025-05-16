namespace ProjetNet.Models
{
	public class Projet
	{
		public int Id { get; set; }
		public string Nom { get; set; }
		public string Description { get; set; }
		public DateTime? DateDemarrage { get; set; }
		public DateTime? DateLivraison { get; set; }
		public int? NombreJoursDev { get; set; }
		public DateTime? DateReunion { get; set; }

		public DirecteurInformatique Directeur {  get; set; }

		public string Client { get; set; }
		public ChefProjet ChefProjet { get; set; }
		public Methodologie Methodologie { get; set; }
		public List<ServiceProjet> Services { get; set; }
		public List<Developpeur> Developpeurs { get; set; }
		public List<Technologie> Technologies { get; set; }
	}
}
