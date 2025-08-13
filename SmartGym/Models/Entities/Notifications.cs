using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartGym.Models;
[Table("Notifications")]
public class Notification
{
	public int Id { get; set; }
	[Required]
	public int UserId { get; set; }
	[Required]
	public string Title { get; set; } = string.Empty;
	[Required]
	public string Contents { get; set; } = string.Empty;
	[Required]
	public bool WasOpened { get; set; }
	[Required]
	public DateTime TimeStamp { get; set; }
	public int? ClassSessionId { get; set; }
	public int? WaitlistId { get; set; }
}
