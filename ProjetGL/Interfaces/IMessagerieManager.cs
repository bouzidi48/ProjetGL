using ProjetGL.Models;

namespace ProjetGL.Interfaces
{
    public interface IMessagerieManager
    {
        void creerMessage(Messagerie messagerie);
        void checkMessagerie(Account recepteur, Account emeteur);
    }
}
