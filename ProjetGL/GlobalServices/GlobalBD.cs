using ProjetGL.Data;

namespace ProjetGL.GlobalServices
{
	public static class GlobalBD
	{
		private static BD bd;
		static GlobalBD()
		{
			Bd = new BD();
		}

		public static BD Bd { get => bd; set => bd = value; }
	}
}
