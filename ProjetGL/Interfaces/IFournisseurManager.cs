using ProjetGL.Models;

namespace ProjetGL.Interfaces
{
    public interface IFournisseurManager
    {
        void AddFournisseur(Fournisseur fournisseur);
        Fournisseur FindFournisseur(string email);
        void UpdateFournisseur(Fournisseur fournisseur, string Email);
        List<Fournisseur> GetAllFournisseurs();
    }
}
