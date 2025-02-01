using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjetGL.Models
{
    [Table("Enseignant")]
    public class Enseignant
    {
       
           [Key] // Clé primaire
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-incrémentation
            public int Id { get; set; }

            [Required] // Champ obligatoire
            [StringLength(100)] // Taille max de la colonne SQL
            public string Name { get; set; }

            [Required]
            [StringLength(100)]
            [EmailAddress] // Vérifie le format email
            public string Email { get; set; }

            // Si tu veux garder la relation avec Departement et Ressources :
            public virtual List<Ressource> Ressources { get; set; } = new List<Ressource>();

            public Enseignant()
            {
                // Initialisation des listes pour éviter les NullReferenceException
                Ressources = new List<Ressource>();
            }
        }
    }
