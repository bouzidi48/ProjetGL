using System.ComponentModel.DataAnnotations;

namespace ProjetGL.Models
{
    public class Departement
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Departement()
        {
            this.Name = " ";
        }

        //public List<DemandeRessource> DemandesRessources { get; set; } = new List<DemandeRessource>();



    }
}
