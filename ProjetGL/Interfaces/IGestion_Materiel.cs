using ProjetGL.Models;

namespace ProjetGL.Interfaces
{
	public interface IGestion_Materiel
	{
		void AddOrdinateur(Ordinateur ordinateur);
		void DelOrdinateur(int id);
		bool ExistOrdinateur(int id);
		Ordinateur FindOrdinateur(int id);
		List<Ordinateur> GetOrdinateurs();
		void UpdateOrdinateur(int id, Ordinateur newOrdinateur);

		void AddImprimante(Imprimante imprimante);
		void DelImprimante(int id);
		bool ExistImprimante(int id);
		Imprimante FindImprimante(int id);
		List<Imprimante> GetImprimants();
		void UpdateImprimante(int id, Imprimante newImprimante);

	}
}
