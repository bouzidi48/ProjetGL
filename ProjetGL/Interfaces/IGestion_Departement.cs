using ProjetGL.Models;

namespace ProjetGL.Interfaces
{
	public interface IGestion_Departement
	{
		bool ExistDepartement(int id);
		Departement FindDepartement(int id);
		List<Departement> GetDepartements();
		
	}
}
