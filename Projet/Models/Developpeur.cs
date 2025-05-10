using Microsoft.Identity.Client;
using System.Diagnostics;

namespace ProjetNet.Models
{
	public class Developpeur : Utilisateur
	{
		public Projet Projet { get; set; }
		public List<ServiceProjet> ServicesAssignes { get; set; }
		public List<Tache> Taches { get; set; }
		public List<Technologie> Technologies { get; set; }
	}
}
