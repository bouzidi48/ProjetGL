using ProjetNet.data;
using ProjetNet.Models;
using ProjetNet.Services;

namespace ProjetNet.Bussiness
{
	public class ProjetManager : InterfaceProjetManager
	{
		ProjetDAO projetDAO;

		public ProjetManager(ProjetDAO projetDAO)
		{
			this.projetDAO = projetDAO;
		}

		public void AffecterChefprojet(Projet projet)
		{
			projetDAO.Update(projet);
		}

		public Projet ConsulterProjet(int idProjet)
		{
			return projetDAO.GetById(idProjet);
		}

		public bool CreerProjet(Projet projet)
		{
			Projet pr = projetDAO.GetById(projet.Nom);
			if (pr == null) 
			{
				projetDAO.Add(projet);
				return true;
			}
			else
			{
				return false;
			}
		}

		public List<Projet> GetProjetParChef(int id)
		{
			return projetDAO.GetProjetByChefProjet(id);
		}

		public Projet GetProjetParDev(int id)
		{
			return projetDAO.GetByIdev(id);
		}

		public List<Projet> GetProjetParDir(int id)
		{
			return projetDAO.GetProjetByDirecteur(id);
		}

		public List<Projet> ListerProjets()
		{
			return projetDAO.GetAll();
		}

		public void ModifierProjet(Projet projet)
		{
			projetDAO.Update(projet);
		}

		public void SupprimerProjet(int id)
		{
			projetDAO.Delete(id);
		}

		public void SupprimerProjet(string nom)
		{
			projetDAO.Delete(nom);
		}
	}
}
