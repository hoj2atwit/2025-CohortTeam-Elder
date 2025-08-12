using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartGym.Models;
using SmartGym.Services.NotificationService;

namespace SmartGym.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NotificationController : ControllerBase
	{
		// private readonly NotificationService _notificationService;

		// public NotificationController(NotificationService notificationService)
		// {
		// 	_notificationService = notificationService;
		// }

		// [HttpPost("confirm")]
		// public async Task<IActionResult> SendBookingConfirm([FromBody] NotificationMessage.BookingMessage message)
		// {
		// 	await _notificationService.SendBookingConfirm(message.UserId, message.ClassSessionId);
		// 	return Ok();
		// }

		// [HttpPost("send-waitlist-notification")]
		// public async Task<IActionResult> SendWaitlistNotification([FromBody] NotificationMessage.WaitlistMessage message)
		// {
		// 	await _notificationService.SendWaitlistNotification(message.UserId, message.WaitlistId);
		// 	return Ok();
		// }

		// [HttpPost("send-general-notification")]
		// public async Task<IActionResult> SendGeneralNotification([FromBody] NotificationMessage.GeneralMassage message)
		// {
		// 	await _notificationService.SendGeneralNotificationA(message.UserId, message.Title, message.Contents);
		// 	return Ok();
		// }

		// [HttpGet("user/{userId}")]
		// public async Task<IActionResult> GetUserNotifications(int userId)
		// {
		// 	var notifications = await _notificationService.GetUserNotifications(userId);
		// 	return Ok(notifications);
		// }

		// [HttpPost("mark-as-opened/{notificationId}")]
		// public async Task<IActionResult> MarkAsOpened(int notificationId)
		// {
		// 	await _notificationService.MarkAsOpenedAsync(notificationId);
		// 	return Ok();
		// }
	}
}
