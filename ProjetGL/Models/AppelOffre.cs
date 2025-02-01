using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjetGL.Models
{
    public class AppelOffre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppelId { get; set; }

        [Required]
        public DateTime DateDebut { get; set; }

        [Required]
        public DateTime DateFin { get; set; }

        [Required]
        public string Description { get; set; }

        // Relation Many-to-Many avec Ressources
        public List<Ressource> Ressources { get; set; } = new List<Ressource>();
    }
}
