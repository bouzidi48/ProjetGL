namespace ProjetGL.Models
{
    public class Offre
    {
        private int offreId;
        private string offreDescription;
        private string fournisseurId;
        private double price;
        private string state;
        private int appelNum;

        public Offre()
        {

        }

        public Offre(int offreId, string offreDescription, string fournisseurId, double price, string state, int appelNum)
        {
            this.offreId = offreId;
            this.offreDescription = offreDescription;
            this.fournisseurId = fournisseurId;
            this.price = price;
            this.state = state;
            this.appelNum = appelNum;
        }

        public int OffreId { get => offreId; set => offreId = value; }
        public string OffreDescription { get => offreDescription; set => offreDescription = value; }
        public string FournisseurId { get => fournisseurId; set => fournisseurId = value; }
        public double Price { get => price; set => price = value; }
        public string State { get => state; set => state = value; }
        public int AppelNum { get => appelNum; set => appelNum = value; }
    }
}
