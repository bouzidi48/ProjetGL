using ProjetNet.data;
using ProjetNet.Models;
using ProjetNet.Services;

namespace ProjetNet.Bussiness
{
	public class ServiceManager : InterfaceServiceManager
	{
		ServiceDAO serviceDAO;

		public ServiceManager(ServiceDAO serviceDAO)
		{
			this.serviceDAO = serviceDAO;
		}

		public void AjouterService(ServiceProjet serviceProjet)
		{
			serviceDAO.Add(serviceProjet);
		}

		public List<ServiceProjet> GetServiceByDeveleppeur(Developpeur dev)
		{
			return serviceDAO.getSerByDev(dev);
		}

		public List<ServiceProjet> getServiceByProjet(Projet pro)
		{
			return serviceDAO.getSerByPro(pro);
		}

		public ServiceProjet GetServiceProjet(int id)
		{
			return serviceDAO.GetById(id);
		}


	}
}
