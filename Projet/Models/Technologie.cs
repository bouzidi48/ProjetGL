namespace ProjetNet.Models
{
	public class Technologie
	{
		public int Id { get; set; }
		public string Nom { get; set; }
		public List<Projet> Projets { get; set; }
		public List<Developpeur> Developpeurs { get; set; }
	}
}
