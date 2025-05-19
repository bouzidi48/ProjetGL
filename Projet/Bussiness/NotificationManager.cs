using ProjetNet.data;
using ProjetNet.Models;
using ProjetNet.Services;

namespace ProjetNet.Bussiness
{
	public class NotificationManager : InterfaceNotificationManager
	{
		NotificationDAO notificationDAO;

		public NotificationManager(NotificationDAO notificationDAO)
		{
			this.notificationDAO = notificationDAO;
		}

		public void EnvoyerNotification(Notification notification)
		{
			notificationDAO.Add(notification);
		}

		public void ModifierNotification(Notification notification)
		{
			notificationDAO.Update(notification);
		}

		public List<Notification> RecevoirNotification(int Destinataireid)
		{
			return notificationDAO.GetByIdAndNotLue(Destinataireid);
		}

		public Notification GetNotification(int id)
		{
			return notificationDAO.GetById(id);
		}
	}
}
