namespace ProjetNet.Models
{
	public class Tache
	{
		public int Id { get; set; }
		public string DescriptionTache { get; set; }
		public double PourcentageAvancement { get; set; }

		public Developpeur Developpeur { get; set; }
		public ServiceProjet Service { get; set; }
	}
}
