using System.Diagnostics;

namespace ProjetNet.Models
{
	public class ServiceProjet
	{
		public int Id { get; set; }
		public string Nom { get; set; }
		public string DescriptionService { get; set; }
		public int DureeJours { get; set; }

		public Developpeur DeveloppeurAssigne { get; set; }
		public Projet Projet { get; set; }
		public List<Tache> Taches { get; set; }
	}
}
