namespace ProjetGL.Models
{
    public class Livraison
    {
        public int LivraisonId { get; set; }  // Identifiant unique de la livraison

        public string EmailFournisseur { get; set; }  // Clé étrangère vers le fournisseur
        public Fournisseur Fournisseur { get; set; }  // Référence à l'entité Fournisseur

        public DateTime DateLivraison { get; set; }  // Date de la livraison
        public int RessourcesLivres { get; set; }  // Nombre de ressources livrées
        public string Etat { get; set; }  // Etat de la livraison (en cours, livrée, retardée)
    }

}
