using ProjetNet.Data;
using ProjetNet.Models;
using ProjetNet.Services;

namespace ProjetNet.Bussiness
{
    public class UtilisateurManager : InterfaceUtilisateurManager
    {
        UtilisateurDAO data;
        public UtilisateurManager(UtilisateurDAO data)
        {
            this.data = data;
        }

        public bool connecter(string email, string motDePasse)
        {
            Utilisateur user = data.Authentifier(email, motDePasse);
            if (user != null && user.MotDePasse == motDePasse) 
            {
                return true;
            }
            return false;
        }

		public Utilisateur GetById(string id)
		{
			return data.GetById(id);
		}

		public Utilisateur GetById(int id)
		{
			return data.GetById(id);
		}

		public List<Utilisateur> GetByRole(string role)
		{
			return data.GetByRole(role);
		}
	}
}
