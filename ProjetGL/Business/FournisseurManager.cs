using ProjetGL.Data;
using ProjetGL.Interfaces;
using ProjetGL.Models;

namespace Projet.Business
{
    public class FournisseurManager : IFournisseurManager
    {
        IGestion_Fournisseur data = new Gestion_Fournisseur();
        public void AddFournisseur(Fournisseur fournisseur)
        {
            if (data.FindFournisseur(fournisseur.Email) == null)
                data.AddFournisseur(fournisseur);
            else
                throw new Exception("fournisseur already exist");
        }

        public Fournisseur FindFournisseur(string email)
        {
            return data.FindFournisseur(email);
        }

        public List<Fournisseur> GetAllFournisseurs()
        {
            return data.GetAllFournisseurs();
        }

        public void UpdateFournisseur(Fournisseur fournisseur, string Email)
        {
            data.UpdateFournisseur(fournisseur,Email);
        }
    }
}
