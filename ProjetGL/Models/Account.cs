using System.ComponentModel.DataAnnotations;

namespace ProjetGL.Models
{
    public class Account
    {
        private string username;
        private string password;
        private string role;

        public Account()
        {
            username = "user";
            password = "";
            role = "Utilisateur"; // Rôle par défaut
        }


        public string Username
        {
            get => username;
            set => username = value;
        }

        // [DataType(DataType.Password)]
        public string? Password
        {
            get => password;
            set => password = value;
        }

        // Champ pour le rôle de l'utilisateur

        public string Role
        {
            get => role;
            set => role = value;
        }
    }
}