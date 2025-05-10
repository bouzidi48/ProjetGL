using Microsoft.Identity.Client;

namespace ProjetNet.Models
{
	public class Client
	{
		public int Id { get; set; }
		public string Nom { get; set; }
		public string Prenom { get; set; }
		public string Email { get; set; }
		public List<Projet> Projets { get; set; }
	}
}
