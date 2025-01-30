using ProjetGL.Data;
using ProjetGL.Interfaces;
using ProjetGL.Models;

namespace ProjetGL.Business
{
	public class MaterielManager : IMaterielManager
	{
		IGestion_Materiel data = new Gestion_Materiel();
		public void AddImprimante(Imprimante imprimante)
		{
			data.AddImprimante(imprimante);
		}

		public void AddOrdinateur(Ordinateur ordinateur)
		{
			data.AddOrdinateur(ordinateur);
		}

		public void DelImprimante(int id)
		{
			data.DelImprimante(id);
		}

		public void DelOrdinateur(int id)
		{
			data.DelOrdinateur(id);
		}

		public bool ExistImprimante(int id)
		{
			return data.ExistImprimante(id);
		}

		public bool ExistOrdinateur(int id)
		{
			return data.ExistOrdinateur(id);
		}

		public Ordinateur FindImprimante(int id)
		{
			return data.FindImprimante(id);
		}

		public Ordinateur FindOrdinateur(int id)
		{
			return data.FindOrdinateur(id);
		}

		public List<Ordinateur> GetImprimants()
		{
			return data.GetImprimants();
		}

		public List<Ordinateur> GetOrdinateurs()
		{
			return data.GetOrdinateurs();
		}

		public void UpdateImprimante(int id, Imprimante newImprimante)
		{
			data.UpdateImprimante(id, newImprimante);
		}

		public void UpdateOrdinateur(int id, Ordinateur newOrdinateur)
		{
			data.UpdateOrdinateur(id, newOrdinateur);
		}
	}
}
