namespace ProjetGL.Models
{
	public class Responsable:Account
	{
		private static int cp = 0;
		private int id;

		public int Id { get => id; set => id = value; }

		public Responsable(string username, string password, Role userRole) : base(username, password, userRole)
		{
			cp = cp + 1;
			Id = cp;
		}
	}
}
