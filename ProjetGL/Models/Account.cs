using System.ComponentModel.DataAnnotations;


namespace ProjetGL.Models
{

    public class Account
    {
        private int id;
        public static int cp=0;
            
        private string username;
        private string password;
        [Required]
        private Role userRole;

        public int Id { get => id; set => id = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public Role UserRole { get => userRole; set => userRole = value; }


        public Account(string username, string password, Role userRole)
        {
            cp = cp + 1;
            Id = cp;
            Username = username;
            Password = password;
            UserRole = userRole;
        }
            
        public Account() {
            cp = cp + 1;
            Id = cp;
        }


        public override string ToString()
        {
            return "Id: " + Id + " Username: " + Username + " Password: " + Password + " Role: " + UserRole;
        }

    }
}
