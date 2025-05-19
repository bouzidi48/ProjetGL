using ProjetNet.data;
using ProjetNet.Models;
using ProjetNet.Services;

namespace ProjetNet.Bussiness
{
	public class DeveloppeurManager : InterfaceDeveloppeurManager
	{
		DeveloppeurDAO developpeurDAO;

		public DeveloppeurManager(DeveloppeurDAO developpeurDAO)
		{
			this.developpeurDAO = developpeurDAO;
		}

		public void affecterProjet(Developpeur developpeur)
		{
			developpeurDAO.Update(developpeur);
		}

		public void ChagerProfile(Developpeur developpeur)
		{
			developpeurDAO.Update(developpeur);
		}

		public Developpeur GetDeveloppeur(int id)
		{
			return developpeurDAO.GetById(id);
		}

		public List<Developpeur> GetDeveParProjet(Projet projet)
		{
			return developpeurDAO.GetDevByPro(projet);
		}

		public List<Developpeur> ListerDeveParTech(List<Technologie> technologies)
		{
			return developpeurDAO.GetByTechnos(technologies);
		}
	}
}
