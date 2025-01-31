using ProjetGL.Data;
using ProjetGL.Interfaces;
using ProjetGL.Models;

namespace ProjetGL.Business
{
	public class DepartementManager : IDepartementManager
	{
		IGestion_Departement data = new Gestion_Departement();

		public bool ExistDepartement(int id)
		{
			return data.ExistDepartement(id);
		}

		public Departement FindDepartement(int id)
		{
			return data.FindDepartement(id);
		}

		public List<Departement> GetDepartements()
		{
			return data.GetDepartements();
		}
	}
}
