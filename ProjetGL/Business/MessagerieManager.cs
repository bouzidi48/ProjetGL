using ProjetGL.Data;
using ProjetGL.Interfaces;
using ProjetGL.Models;

namespace ProjetGL.Business
{
    public class MessagerieManager : IMessagerieManager
    {
        IGestion_Messagerie data = new Gestion_Messagerie();

        public void checkMessagerie(Account recepteur, Account emeteur)
        {
            data.checkMessagerie(recepteur, emeteur);
        }

        public void creerMessage(Messagerie messagerie)
        {
            data.creerMessage(messagerie);
        }
    }
}
