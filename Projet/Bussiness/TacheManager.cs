using ProjetNet.data;
using ProjetNet.Models;
using ProjetNet.Services;

namespace ProjetNet.Bussiness
{
	public class TacheManager : InterfaceTacheManager
	{
		TacheDAO tacheDAO;

		public TacheManager(TacheDAO tacheDAO)
		{
			this.tacheDAO = tacheDAO;
		}

		public void AjouterTache(Tache tache)
		{
			tacheDAO.Add(tache);
		}

		public Tache GetTache(int id)
		{
			return tacheDAO.GetById(id);
		}

		public List<Tache> GetTachesByService(ServiceProjet serviceProjet)
		{
			return tacheDAO.GetByIdService(serviceProjet);
		}

		public void UpdateTache(Tache tache)
		{
			tacheDAO.Update(tache);
		}
	}
}
