using ProjetGL.Models;

namespace ProjetGL.Interfaces
{
    public interface IGestion_AppelOffre
    {
        void AddAppelOffre(AppelOffre appel, List<int> ressourcesIds);
        AppelOffre FindAppelOffre(int AppelId);
        void DeleteAppelOffre(int AppelId);
        List<AppelOffre> GetAllAppelOffre();
    }
}
