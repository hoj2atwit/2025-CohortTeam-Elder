using System;

namespace SmartGym.Models;

public class NotificationsDTO
{
	public int Id { get; set; }
	public int UserId { get; set; }
	public string Title { get; set; } = string.Empty;
	public string Content { get; set; } = string.Empty;
	public bool WasOpened { get; set; }
	public DateTime TimeStamp { get; set; }
	public int? ClassSessionId { get; set; }
	public int? WaitlistId { get; set; }
}
