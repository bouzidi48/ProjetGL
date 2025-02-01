using ProjetGL.Models;

namespace ProjetGL.Interfaces
{
    public interface IPropositionManager
    {
        void AddProposition(Proposition proposition);
        Proposition FindProposition(int id);
        List<Proposition> GetAllPropositions();
    }
}
