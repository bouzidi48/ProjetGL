using ProjetNet.Models;

namespace ProjetNet.Services
{
	public interface InterfaceDeveloppeurManager
	{
		List<Developpeur> ListerDeveParTech(List<Technologie> technologies);
		Developpeur GetDeveloppeur(int id);
		void ChagerProfile(Developpeur developpeur);
		void affecterProjet(Developpeur developpeur);

		List<Developpeur> GetDeveParProjet(Projet pro);

	}
}
