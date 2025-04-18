using ProjetGL.Data;
using ProjetGL.Interfaces;
using ProjetGL.Models;

namespace Projet.Business
{
    public class AppelOffreManager : IAppelOffreManager
    {
        IGestion_AppelOffre data = new Gestion_AppelOffre();
        public void AddAppelOffre(AppelOffre appel, List<int> ressourcesIds)
        {
            if (data.FindAppelOffre(appel.AppelId) == null)
                data.AddAppelOffre(appel, ressourcesIds); // ✅ Passer la liste des ressources
            else
                throw new Exception("L'appel d'offre existe déjà");
        }


        public AppelOffre FindAppelOffre(int AppelId)
        {
            return data.FindAppelOffre(AppelId);
        }

        public List<AppelOffre> GetAllAppels()
        {
            return data.GetAllAppelOffre();
        }

        public List<AppelOffre> GetAllAppelOffre()
        {
            return data.GetAllAppelOffre(); 
        }

    }
}
