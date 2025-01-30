using ProjetGL.Models;

namespace ProjetGL.Interfaces
{
    public interface IGestion_Messagerie
    {
        void creerMessage(Messagerie messagerie);
        void checkMessagerie(Account recepteur,Account emeteur);

    }
}
