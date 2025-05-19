using ProjetNet.Models;

namespace ProjetNet.Services
{
	public interface InterfaceUtilisateurManager
	{
		public bool connecter (string email, string motDePasse);
    }
}
