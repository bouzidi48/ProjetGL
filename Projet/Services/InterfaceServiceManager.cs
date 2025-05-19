using ProjetNet.Models;

namespace ProjetNet.Services
{
	public interface InterfaceServiceManager
	{
		void AjouterService(ServiceProjet serviceProjet);
		ServiceProjet GetServiceProjet(int id);

		List<ServiceProjet> GetServiceByDeveleppeur(Developpeur dev);

		List<ServiceProjet> getServiceByProjet(Projet pro);
	}
}
