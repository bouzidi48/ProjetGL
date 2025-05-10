namespace ProjetNet.Models
{
	public class Notification
	{
		public int Id { get; set; }
		public string Message { get; set; }
		public bool EstLue { get; set; }
		public Utilisateur Destinataire { get; set; }
	}
}
