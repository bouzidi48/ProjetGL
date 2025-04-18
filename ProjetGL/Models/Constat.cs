using System.ComponentModel.DataAnnotations;

namespace ProjetGL.Models
{
    public class Constat
    {
        private int idConstat;
        private int idPanne;
        private int idRessource;
        private int idRsponsable;
        private string explication;

        public Constat()
        {
            idConstat = 0;
            idPanne = 0;
            idRessource = 0;
            idRsponsable = 0;
            explication = "ce";
        }

        public Constat(int idConstat, int idPanne, int idRessource, int idRsponsable)
        {
            this.idConstat = idConstat;
            this.idPanne = idPanne;
            this.idRessource = idRessource;
            this.idRsponsable = idRsponsable;
            this.explication = "";
        }
        [Key]
        public int IdPanne { get => idPanne; set => idPanne = value; }
        public int IdRessource { get => idRessource; set => idRessource = value; }
        public string Explication { get => explication; set => explication = value; }
        public int IdRsponsable { get => idRsponsable; set => idRsponsable = value; }
        public int IdConstat { get => idConstat; set => idConstat = value; }
    }
}
