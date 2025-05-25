using ProjetNet.Models;

namespace ProjetNet.Services
{
	public interface InterfaceNotificationManager
	{
		void EnvoyerNotification(Notification notification);
		List<Notification> RecevoirNotification(int Destinataireid);
		void ModifierNotification(Notification notification);
		Notification GetNotification(int id);
	}
}
