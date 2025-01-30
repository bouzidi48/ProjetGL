namespace ProjetGL.Models
{
	public class Imprimante : Materiel
	{
		private static int cp = 0;
		private int imprimanteId;
		private string vitesseImprimente;
		private string resolution;
		public int ImprimanteId { get => imprimanteId; set => imprimanteId = value; }
		public string VitesseImprimente { get => vitesseImprimente; set => vitesseImprimente = value; }
		public string Resolution{ get => resolution; set => resolution = value; }

		public Imprimante() : base()
		{
			cp++;
			ImprimanteId = cp;
			VitesseImprimente = "";
			Resolution = "";
		}
		public Imprimante(int imprimanteId, string vitesseImprimente, string resolution, string marque, TypeMateriel type) : base(marque, type)
		{
			cp++;
			ImprimanteId = cp;
			VitesseImprimente = vitesseImprimente;
			Resolution = resolution;
		}
		public override string ToString()
		{
			return base.ToString() + $", TypeImprimante: {VitesseImprimente}, Couleur: {Resolution}";
		}
	}
}
