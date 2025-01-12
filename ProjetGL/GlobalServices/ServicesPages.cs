using ProjetGL.Business;
using ProjetGL.Data;
using ProjetGL.Interfaces;


namespace ProjetGL.GlobalServices
{

    public static class ServicesPages
        {
            public static IAccountManager managerAccount;
            //public static IDataAccount data ;
            static ServicesPages()
            {
            //manager = new AccountManager(); //on instancie le service
                managerAccount = new AccountManager(); //on instancie le service
            }
        }
    }
