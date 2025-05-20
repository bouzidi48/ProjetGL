using ProjetNet.Models;

namespace ProjetNet.Services
{
	public interface InterfaceProjetManager
	{
		bool CreerProjet(Projet projet);
		void AffecterChefprojet(Projet projet);
		List<Projet> ListerProjets();
		Projet ConsulterProjet(int idProjet);
		void ModifierProjet(Projet projet);

		void SupprimerProjet(int id);
		void SupprimerProjet(string nom);

		List<Projet> GetProjetParChef(int id);

		Projet GetProjetParDev(int id);

		List<Projet> GetProjetParDir(int id);
		
	}
}
