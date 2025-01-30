namespace ProjetGL.Models
{
    public class Messagerie
    {
        public static int cp = 0;
        private int id;
        private Account emeteur;
        private Account recepteur;
        private string message;
        private int checkMessage=0;

        

        public int Id { get => id; set => id = value; }
        public Account Emeteur { get => emeteur; set => emeteur = value; }
        public Account Recepteur { get => recepteur; set => recepteur = value; }
        public string Message { get => message; set => message=value; }
        public int CheckMessage { get => checkMessage; set => checkMessage=value; }

        public Messagerie(Account emeteur, Account recepteur, string message)
        {
            cp = cp + 1;
            Id = cp;
            Emeteur=emeteur;
            Recepteur=recepteur;
            Message=message;
        }
    }
}
