namespace ProjetGL.Models
{
	public class Enseignant:Account
	{
		private static int cp = 0;
		private int id;
		private Departement departement;

		public int Id { get => id; set => id = value; }
		public Departement Departement { get => departement; set => departement = value; }

		public Enseignant(int id, string username, string password, Role userRole, Departement departement) : base(username, password, userRole)
		{
			Id = id;
			Departement = departement;
		}

		public Enseignant(string username, string password, Role userRole, Departement departement) : base(username, password, userRole)
		{
			cp = cp + 1;
			Id = cp;
			Departement = departement;
		}
	}
}
