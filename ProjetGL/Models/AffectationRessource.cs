namespace ProjetGL.Models
{
    public class AffectationRessource
    {
        public int Id { get; set; }  // Identifiant unique de l'affectation
        public int RessourceId { get; set; }  // Identifiant de la ressource affectée
        public Ressource Ressource { get; set; }  // Relation avec la classe Ressource
        public int DepartementId { get; set; }  // Identifiant du département auquel la ressource est affectée
        public Departement Departement { get; set; }  // Relation avec la classe Departement
        public int? EnseignantId { get; set; }  // Identifiant de l'enseignant auquel la ressource est affectée (optionnel)
        public Enseignant Enseignant { get; set; }  // Relation avec la classe Enseignant (si applicable)
        public DateTime DateAffectation { get; set; }  // Date de l'affectation
       
    }
}
