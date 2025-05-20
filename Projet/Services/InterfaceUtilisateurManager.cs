using ProjetNet.Models;

namespace ProjetNet.Services
{
	public interface InterfaceUtilisateurManager
	{
		public bool connecter (string email, string motDePasse);
		public Utilisateur GetById(string id);

		public Utilisateur GetById(int id);
		public List<Utilisateur> GetByRole(string role);
    }
}
