using ProjetGL.Models;

namespace ProjetGL.Interfaces
{
	public interface IDepartementManager
	{
		bool ExistDepartement(int id);
		Departement FindDepartement(int id);
		List<Departement> GetDepartements();

	}
}
