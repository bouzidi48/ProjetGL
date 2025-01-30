namespace ProjetGL.Models
{
	public class ChefDepartement: Account
	{
		private static int cp = 0;
		private int id;
		private Departement departement;

		public int Id { get => id; set => id = value; }
		public Departement Departement { get => departement; set => departement = value; }

		public ChefDepartement(string username, string password, Role userRole, Departement departement) : base(username, password, userRole)
		{
			cp = cp + 1;
			Id = cp;
			Departement = departement;
		}

		override public string ToString()
		{
			return "ChefDepartement : [id :" + Id + " Departement : " + Departement + " "+ base.ToString() +"]";
		}
	}
}
