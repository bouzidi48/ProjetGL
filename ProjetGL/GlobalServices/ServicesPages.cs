using ProjetGL.Business;
using ProjetGL.Data;
using ProjetGL.Interfaces;


namespace ProjetGL.GlobalServices
{

    public static class ServicesPages
    {
            public static IAccountManager managerAccount;
            public static IMessagerieManager managerMessagerie;
            public static IMaterielManager managerMateriel;
		//public static IDataAccount data ;
		static ServicesPages()
            {
                //manager = new AccountManager(); //on instancie le service
                managerAccount = new AccountManager(); //on instancie le service
                managerMessagerie = new MessagerieManager();
                managerMateriel = new MaterielManager();
		}
    }
}
