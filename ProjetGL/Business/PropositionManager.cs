using ProjetGL.Data;
using ProjetGL.Interfaces;
using ProjetGL.Models;

namespace ProjetGL.Business
{
    public class PropositionManager : IPropositionManager
    {
        IGestion_Proposition data = new Gestion_Proposition();
        public void AddProposition(Proposition proposition)
        {
            if (data.FindProposition(proposition.PropositionId) == null)
                data.AddProposition(proposition);
            else
                throw new Exception("proposition already exist");
        }


        public Proposition FindProposition(int id)
        {
            return data.FindProposition(id);
        }

        public List<Proposition> GetAllPropositions()
        {
            return data.GetAllPropositions();
        }

    }
}
