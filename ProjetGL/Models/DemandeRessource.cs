namespace ProjetGL.Models
{
    public class DemandeRessource
    {
        public int Id { get; set; } 
        public int DepartementId { get; set; } 
        public Departement Departement { get; set; } 
        public List<Ressource> Ressources { get; set; } = new List<Ressource>(); 
        public DateTime DateDemande { get; set; } 
        public string Statut { get; set; } = "En attente"; 
        public string Description { get; set; } 

        public DemandeRessource()
        {
            DateDemande = DateTime.Now;
        }
    }
}
