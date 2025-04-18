using System.ComponentModel.DataAnnotations;

namespace ProjetGL.Models
{
    public class Panne
    {
        private int idPanne;
        private int idRessource;
        private string explication;
        private DateTime dateApp;
        private string frequence;
        private string typeOrdre;
        public Panne()
        {
            this.idRessource = 1;
            this.explication = "panne1";
            this.dateApp = DateTime.Now;
            this.frequence = "rare";
            typeOrdre = "logiciel";
        }
        public Panne(int idRessource, string explication, DateTime dateApp, string frequence, string ordreLogiciel)
        {
            this.idRessource = idRessource;
            this.explication = explication;
            this.dateApp = dateApp;
            this.frequence = frequence;
            this.typeOrdre = ordreLogiciel;
        }
        [Key]
        public int IdPanne { get => idPanne; set => idPanne = value; }
        public int IdRessource { get => idRessource; set => idRessource = value; }
        public string Explication { get => explication; set => explication = value; }
        public DateTime DateApp { get => dateApp; set => dateApp = value; }
        public string Frequence { get => frequence; set => frequence = value; }
        public string TypeOrdre { get => typeOrdre; set => typeOrdre = value; }

    }
}
