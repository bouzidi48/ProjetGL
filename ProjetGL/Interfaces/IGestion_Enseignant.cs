using ProjetGL.Models;

namespace ProjetGL.Interfaces
{
	public interface IGestion_Enseignant
	{
		bool ExistEnseignant(int id);
		Enseignant FindEnseignant(int id);
		List<Enseignant> GeEnseignants();
	}
}
