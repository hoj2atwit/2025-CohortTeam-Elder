using System;
using SmartGym.Models;

namespace SmartGym.Services;

public interface INotificationService
{
	Task SendBookingConfirm(int userId, int classSessionId);
	Task SendWaitlistNotification(int userId, int classSessionId);
	Task SendGeneralNotificationA(int userId, string title, string contents);
	Task<IEnumerable<NotificationsDTO>> GetUserNotifications(int userId);
	Task MarkAsOpenedAsync(int notificationId);
}
