namespace ProjetGL.Models
{
    public class Proposition
    {
        public int PropositionId { get; set; }
        public string EmailFournisseur { get; set; }
        public int AppelId { get; set; }
        public DateTime DateSoumission { get; set; }
        public decimal PrixTotal { get; set; }
        public DateTime DateLivraisonEstimee { get; set; }
        public string Garantie { get; set; }
        public string Status { get; set; }

        // Constructeur pour l'initialisation de Proposition
        public Proposition()
        {
            Console.WriteLine("Proposition initialisée");
        }
    }


}
