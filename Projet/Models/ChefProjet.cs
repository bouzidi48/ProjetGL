using Microsoft.Identity.Client;

namespace ProjetNet.Models
{
	public class ChefProjet : Utilisateur
	{
		public List<Projet> Projets { get; set; }
	}
}
