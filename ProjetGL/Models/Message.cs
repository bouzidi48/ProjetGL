namespace ProjetGL.Models
{
    public class Message
    {
        private int messageId;
        private string emetteur;
        private string recepteur;
        private string msg;

        public Message()
        {
            messageId = 0;
            emetteur = "Error";
            recepteur = "Error";
            msg = "Error";
        }

        public Message(string emetteur, string recepteur, string msg)
        {
            MessageId = messageId;
            Emetteur = emetteur;
            Recepteur = recepteur;
            Msg = msg;
        }
        public Message(int messageId, string emetteur, string recepteur, string msg)
        {
            MessageId = messageId;
            Emetteur = emetteur;
            Recepteur = recepteur;
            Msg = msg;
        }

        public int MessageId { get => messageId; set => messageId = value; }
        public string Emetteur { get => emetteur; set => emetteur = value; }
        public string Recepteur { get => recepteur; set => recepteur = value; }
        public string Msg { get => msg; set => msg = value; }
    }
}
