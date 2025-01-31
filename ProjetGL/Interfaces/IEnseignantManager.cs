using ProjetGL.Models;

namespace ProjetGL.Interfaces
{
	public interface IEnseignantManager
	{
		bool ExistEnseignant(int id);
		Enseignant FindEnseignant(int id);
		List<Enseignant> GeEnseignants();
	}
}
