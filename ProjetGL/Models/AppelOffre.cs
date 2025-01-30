namespace ProjetGL.Models
{
	public class AppelOffre
	{
		private static int cp = 0;
		private int id;
		private List<Besoin> besoins;
		private Responsable responsable;
		private DateTime dateDebut;
		private DateTime dateFin;


		public int Id { get => id; set => id = value; }
		public List<Besoin> Besoins { get => besoins; set => besoins = value; }
		public Responsable Responsable { get => responsable; set => responsable = value; }
		public DateTime DateDebut { get => dateDebut; set => dateDebut = value; }
		public DateTime DateFin { get => dateFin; set => dateFin = value; }

		public AppelOffre(List<Besoin> besoins, Responsable responsable,DateTime dateDebut, DateTime dateFin)
		{
			cp = cp + 1;
			Id = cp;
			Besoins = besoins;
			Responsable = responsable;
			DateDebut = dateDebut;
			DateFin = dateFin;
		}
	}
}
