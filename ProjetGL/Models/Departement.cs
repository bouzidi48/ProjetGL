namespace ProjetGL.Models
{
	public class Departement
	{
		private static int cp = 0;
		private int id;
		private string nom;
		private double budget;

		public int Id { get => id; set => id = value; }
		public string Nom { get => nom; set => nom = value; }
		public double Budget { get => budget; set => budget = value; }

		public Departement(string nom, double budget)
		{
			cp = cp + 1;
			Id = cp;
			Nom = nom;
			Budget = budget;
		}

		public override string ToString()
		{
			return "Departement : [id :" + Id + " Nom : " + Nom + " Budget : " + Budget + "]";
		}
	}
}
