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
            if (user != null && user.MotDePasse != motDePasse) 
            {
                return true;
            }
            return false;
        }
    }
}
