using ProjetGL.Business;
using ProjetGL.Data;


namespace ProjetGL.GlobalServices
{

        public static class ServicesPages
        {
            public static IAccountManager manager;
            //public static IDataAccount data ;
            static ServicesPages()
            {
                //manager = new AccountManager(); //on instancie le service
                manager = new AccountManagerV2(); //on instancie le service
                                                  //data = new DataAccount();
            }
        }
    }
