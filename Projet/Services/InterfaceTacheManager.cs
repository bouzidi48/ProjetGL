using ProjetNet.Models;

namespace ProjetNet.Services
{
	public interface InterfaceTacheManager
	{
		void AjouterTache(Tache tache);
		void UpdateTache(Tache tache);
		Tache GetTache(int id);
		List<Tache> GetTachesByService(ServiceProjet serviceProjet);


	}
}
