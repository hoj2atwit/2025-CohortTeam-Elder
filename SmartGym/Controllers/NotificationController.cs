using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartGym.Models;
using SmartGym.Services;

namespace SmartGym.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NotificationController : ControllerBase
	{
		private readonly INotificationService _notificationService;

		public NotificationController(INotificationService notificationService)
		{
			_notificationService = notificationService;
		}
		[HttpPost("general")]
		public async Task<IActionResult> SendGeneralNotification([FromBody] NotificationsDTO message)
		{
			if (message == null)
				return BadRequest();

			await _notificationService.SendGeneralNotification(message.UserId, message.Title, message.Content);
			return Ok();
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<NotificationsDTO>>> GetAllNotifications()
		{
			var notifications = await _notificationService.GetAllNotifications();
			if (notifications == null)
				return NotFound();
			return Ok(notifications);
		}
		[HttpPost("blast")]
		public async Task<IActionResult> SendBlastNotification([FromBody] NotificationsDTO message, [FromQuery] bool areYouSure = false)
		{
			if (message == null)
				return BadRequest();

			if (!areYouSure)
				return BadRequest("This will send to ALL users. You must confirm this action by setting areYouSure=true.");

			await _notificationService.SendBlastNotificationToAllUsers(message.Title, message.Content);
			return Ok();
		}

		[HttpGet("user/{userId}")]
		public async Task<ActionResult<IEnumerable<NotificationsDTO>>> GetUserNotifications(int userId)
		{
			try
			{
				var notifications = await _notificationService.GetUserNotifications(userId);
				if (notifications == null || !notifications.Any())
					return NotFound();
				return Ok(notifications);
			}
			catch (Exception ex)
			{
				// Log exception as needed
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}
	}
}
