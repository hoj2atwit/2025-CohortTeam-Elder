using System;
using SmartGym.Models;

namespace SmartGym.Services;

public interface INotificationService
{
	Task SendBookingConfirm(int userId, int classSessionId);
	Task SendWaitlistNotification(int userId, int classSessionId, bool isNew = false);
	Task SendGeneralNotification(int userId, string title, string content);
	Task<IEnumerable<NotificationsDTO>> GetUserNotifications(int userId);
	Task MarkAsOpenedAsync(int notificationId);
	Task SendBlastNotificationToAllUsers(string title, string content);
}
