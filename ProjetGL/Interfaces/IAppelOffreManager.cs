using ProjetGL.Models;

namespace ProjetGL.Interfaces
{
    public interface IAppelOffreManager
    {
        void AddAppelOffre(AppelOffre appel, List<int> ressourcesIds);
        AppelOffre FindAppelOffre(int AppelId);
        List<AppelOffre> GetAllAppels();
        List<AppelOffre> GetAllAppelOffre();
    }
}
