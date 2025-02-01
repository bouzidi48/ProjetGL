using ProjetGL.Business;
using ProjetGL.Interfaces;

namespace ProjetGL.GlobalServices
{

	public static class ServicesPages
	{
		public static IAccountManager manager;
		public static IFournisseurManager managerFournisseur;
		public static IAppelOffreManager appelOffreManager;
		public static IPropositionManager propositionManager;
		public static IRessourceManager ressourceManager;
		static ServicesPages()
		{
			manager = new AccountManager(); // on instancie le service
											//manager = new AccountManagerV2();
			managerFournisseur = new FournisseurManager();
			appelOffreManager = new AppelOffreManager();
			propositionManager = new PropositionManager();
			ressourceManager = new RessourceManager();

		}
	}
}
