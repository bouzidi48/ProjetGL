using ProjetGL.Models;

namespace ProjetGL.Interfaces
{
	public interface IGestion_Ressource
	{
		void AffecterRessource(AffectationRessource affectation);
		void AjouterRessource(Ressource ressource);
		List<Departement> GetAllDepartements();
		List<Enseignant> GetAllEnseignants();
		List<Ressource> GetAllRessources();
		void SupprimerRessource(int ressourceId);
	}
}