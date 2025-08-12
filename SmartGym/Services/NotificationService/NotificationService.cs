using System;
using AutoMapper;
using SmartGym.Data;
using SmartGym.Helpers;
using SmartGym.Models;
namespace SmartGym.Services.NotificationService;

public class NotificationService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	private readonly IClassService _classService;

	public NotificationService(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task SendBookingConfirm(int userId, int classSessionId)
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

	public async Task SendWaitlistNotification(int userId, int classSessionId)
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
			Title = "Waitlist Notification",
			Contents = $"You have been added to the waitlist for class session {classSessionId}:\n\n{messageBody}",
			WasOpened = false,
			TimeStamp = DateTime.UtcNow,
			ClassSessionId = classSessionId
		};
		await _unitOfWork.NotificationRepository.AddAsync(notification);
		await _unitOfWork.SaveAsync();
	}

	public async Task SendGeneralNotification(int userId, string title, string content)
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

	public async Task SendBlastNotificationToAllUsers(string title, string content)
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

	public async Task<IEnumerable<NotificationsDTO>> GetUserNotifications(int userId)
	{
		var notifications = await _unitOfWork.NotificationRepository.GetAsync(n => n.UserId == userId);
		return _mapper.Map<IEnumerable<NotificationsDTO>>(notifications);
	}

	public async Task MarkAsOpenedAsync(int notificationId)
	{
		var notification = await _unitOfWork.NotificationRepository.GetAsync(notificationId);
		if (notification != null)
		{
			notification.WasOpened = true;
			_unitOfWork.NotificationRepository.Update(notification);
			await _unitOfWork.SaveAsync();
		}
	}
}

