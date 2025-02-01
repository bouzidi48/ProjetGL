using ProjetGL.Models;

namespace ProjetGL.Interfaces
{
    public interface IGestion_Proposition
    {
        void AddProposition(Proposition proposition);
        Proposition FindProposition(int id);
        List<Proposition> GetAllPropositions();
    }
}
