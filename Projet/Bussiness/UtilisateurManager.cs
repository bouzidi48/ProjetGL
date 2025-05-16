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

        public void connecter(string email, string motDePasse)
        {
            data.Authentifier(email, motDePasse);
        }
    }
}
