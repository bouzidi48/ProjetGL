using ProjetGL.Data;
using ProjetGL.Interfaces;
using ProjetGL.Models;

namespace ProjetGL.Business
{
	public class EnseignantManager: IEnseignantManager
	{
		IGestion_Enseignant data = new Gestion_Enseignant();

		public bool ExistEnseignant(int id)
		{
			return data.ExistEnseignant(id);
		}

		public Enseignant FindEnseignant(int id)
		{
			return data.FindEnseignant(id);
		}

		public List<Enseignant> GeEnseignants()
		{
			return data.GeEnseignants();
		}
	}
}
