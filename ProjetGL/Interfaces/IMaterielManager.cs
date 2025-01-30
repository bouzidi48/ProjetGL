using ProjetGL.Models;

namespace ProjetGL.Interfaces
{
	public interface IMaterielManager
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
		Ordinateur FindImprimante(int id);
		List<Ordinateur> GetImprimants();
		void UpdateImprimante(int id, Imprimante newImprimante);
	}
}
