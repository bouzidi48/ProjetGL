namespace ProjetGL.Models
{
    public class Fournisseur
    {
        private string email;
        private string password;
        private string fournisseurName;
        private string nomSociete;
        private string gerant;
        private string status;

        public Fournisseur()
        {
            Email = "Erreur@gmail.com";
            Password = "";
            FournisseurName = "Erreur";
            NomSociete = "Erreur";
            Gerant = "Erreur";
            Status = "Non";

        }

        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string FournisseurName { get => fournisseurName; set => fournisseurName = value; }
        public string NomSociete { get => nomSociete; set => nomSociete = value; }
        public string Gerant { get => gerant; set => gerant = value; }
        public string Status { get => status; set => status = value; }
    }
}
