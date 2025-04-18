using ProjetGL.Data;
using ProjetGL.Interfaces;
using ProjetGL.Models;

namespace ProjetGL.Business
{
	public class RessourceManager : IRessourceManager
	{
		IGestion_Ressource data = new Gestion_Ressource();
		public void AffecterRessource(AffectationRessource affectation)
		{
			data.AffecterRessource(affectation);
		}

		public void AjouterRessource(Ressource ressource)
		{
			data.AjouterRessource(ressource);
		}

		public List<Departement> GetAllDepartements()
		{
			return data.GetAllDepartements();
		}

		public List<Enseignant> GetAllEnseignants()
		{
			return data.GetAllEnseignants();
		}

		public List<Ressource> GetAllRessources()
		{
			return data.GetAllRessources();
		}

		public void SupprimerRessource(int ressourceId)
		{
			data.SupprimerRessource(ressourceId);
		}
	}
}
