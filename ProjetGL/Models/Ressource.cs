using System.ComponentModel.DataAnnotations;

namespace ProjetGL.Models
{
    public class Ressource
    {
        public int RessourceId { get; set; } // Auto-incrémenté
       
        public string? Type { get; set; } // Nullable
        public string? Marque { get; set; } // Nullable
        public int? RAM { get; set; } // Nullable pour les ordinateurs uniquement
        public string? CPU { get; set; } // Nullable
        public string? DisqueDur { get; set; } // Nullable
        public string? Ecran { get; set; } // Nullable
        public string? VitesseImpression { get; set; } // Nullable pour les imprimantes uniquement
        public string? Resolution { get; set; } // Nullable pour les imprimantes uniquement
        public int? EnseignantId { get; set; } // Nullable
        public decimal? Prix { get; set; } // Nullable
        public string? FournisseurMarque { get; set; } // Nullable
        public string? Garantie { get; set; } // Nullable
        public decimal? Total { get; set; } // Nullable
        public bool IsValidated { get; set; } // Obligatoire par défaut
        #pragma warning disable CS8618
        public Ressource() { }
        #pragma warning restore CS8618
        // Relation Many-to-Many avec AppelDoffre
        public virtual List<AppelOffre> AppelsDoffres { get; set; } = new List<AppelOffre>();
    }
}
