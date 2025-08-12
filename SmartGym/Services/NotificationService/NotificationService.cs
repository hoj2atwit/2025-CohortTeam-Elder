using System;
using AutoMapper;
using SmartGym.Data;
using SmartGym.Helpers;
using SmartGym.Models;
namespace SmartGym.Services;

public class NotificationService : INotificationService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	private readonly IClassService _classService;
	private readonly IBookingService _bookingService;

	public NotificationService(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<IEnumerable<NotificationsDTO>> GetAllNotifications()
	{
		try
		{
			var notifications = await _unitOfWork.NotificationRepository.GetAsync();
			return _mapper.Map<IEnumerable<NotificationsDTO>>(notifications);
		}
		catch (Exception ex)
		{
			// Log exception as needed
			throw new Exception("Failed to retrieve notifications.", ex);
		}
	}

	public async Task SendBookingConfirm(int userId, int classSessionId)
	{
		try
		{
			var classSession = await _classService.GetClassById(classSessionId);
			if (classSession == null)
			{
				throw new Exception($"Class session with ID {classSessionId} not found.");
			}

			string levelDisplay = classSession.Level.HasValue
				? EnumHelper.GetDisplayName(classSession.Level.Value)
				: "N/A";
			string messageBody = $"Class: {classSession.Name}\n" +
								 $"Schedule: {classSession.Schedule:dddd, MMMM d, yyyy h:mm tt}\n" +
								 $"Trainer ID: {classSession.TrainerId}\n" +
								 $"Level: {levelDisplay}\n" +
								 $"Description: {classSession.Description}";

			var notification = new Notification
			{
				UserId = userId,
				Title = "Booking Confirmed",
				Contents = $"Your booking for class session {classSessionId} is confirmed:\n\n{messageBody}",
				WasOpened = false,
				TimeStamp = DateTime.UtcNow,
				ClassSessionId = classSessionId
			};
			await _unitOfWork.NotificationRepository.AddAsync(notification);
			await _unitOfWork.SaveAsync();
		}
		catch (Exception ex)
		{
			// Log exception as needed
			throw new Exception("Failed to send booking confirmation notification.", ex);
		}
	}

	public async Task SendWaitlistNotification(int userId, int classSessionId, bool isNew = false)
	{
		try
		{
			var classSession = await _classService.GetClassById(classSessionId);
			var waitlist = await _bookingService.GetSingleWaitlistRecord(classSessionId);
			if (classSession == null)
			{
				throw new Exception($"Class session with ID {classSessionId} not found.");
			}

			string levelDisplay = classSession.Level.HasValue
				? EnumHelper.GetDisplayName(classSession.Level.Value)
				: "N/A";
			string messageBody = $"Class: {classSession.Name}\n" +
								 $"Schedule: {classSession.Schedule:dddd, MMMM d, yyyy h:mm tt}\n" +
								 $"Trainer ID: {classSession.TrainerId}\n" +
								 $"Level: {levelDisplay}\n" +
								 $"Description: {classSession.Description}\n" +
								 $"Position: {waitlist.Position}";
			var update = isNew ? "added to the waitlist" : "updated in the waitlist";
			var notification = new Notification
			{
				UserId = userId,
				Title = "Waitlist Notification",
				Contents = $"You have been {update} for class session {classSessionId}:\n\n{messageBody}",
				WasOpened = false,
				TimeStamp = DateTime.UtcNow,
				ClassSessionId = classSessionId
			};
			await _unitOfWork.NotificationRepository.AddAsync(notification);
			await _unitOfWork.SaveAsync();
		}
		catch (Exception ex)
		{
			// Log exception as needed
			throw new Exception("Failed to send waitlist notification.", ex);
		}
	}

	public async Task SendGeneralNotification(int userId, string title, string content)
	{
		try
		{
			var notification = new Notification
			{
				UserId = userId,
				Title = title,
				Contents = content,
				WasOpened = false,
				TimeStamp = DateTime.UtcNow
			};
			await _unitOfWork.NotificationRepository.AddAsync(notification);
			await _unitOfWork.SaveAsync();
		}
		catch (Exception ex)
		{
			// Log exception as needed
			throw new Exception("Failed to send general notification.", ex);
		}
	}

	public async Task SendBlastNotificationToAllUsers(string title, string content)
	{
		try
		{
			var users = await _unitOfWork.UserRepository.GetAsync();
			foreach (var user in users)
			{
				var notification = new Notification
				{
					UserId = user.Id,
					Title = title,
					Contents = content,
					WasOpened = false,
					TimeStamp = DateTime.UtcNow
				};
				await _unitOfWork.NotificationRepository.AddAsync(notification);
			}
			await _unitOfWork.SaveAsync();
		}
		catch (Exception ex)
		{
			// Log exception as needed
			throw new Exception("Failed to send blast notification to all users.", ex);
		}
	}

	public async Task<IEnumerable<NotificationsDTO>> GetUserNotifications(int userId)
	{
		try
		{
			var notifications = await _unitOfWork.NotificationRepository.GetAsync(n => n.UserId == userId);
			return _mapper.Map<IEnumerable<NotificationsDTO>>(notifications);
		}
		catch (Exception ex)
		{
			// Log exception as needed
			throw new Exception($"Failed to retrieve notifications for user {userId}.", ex);
		}
	}

	public async Task MarkAsOpenedAsync(int notificationId)
	{
		try
		{
			var notification = await _unitOfWork.NotificationRepository.GetAsync(notificationId);
			if (notification != null)
			{
				notification.WasOpened = true;
				_unitOfWork.NotificationRepository.Update(notification);
				await _unitOfWork.SaveAsync();
			}
		}
		catch (Exception ex)
		{
			// Log exception as needed
			throw new Exception($"Failed to mark notification {notificationId} as opened.", ex);
		}
	}
}

