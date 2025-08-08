using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartGym.Models;

[Table("Checkins")]
public class Checkin
{
	[Required]
	public int Id { get; set; }
	[Required]
	public DateTime CheckinTime { get; set; }
	[Required]
	public string Method { get; set; }
	[Required]
	public int UserId { get; set; }
	public AppUser User { get; set; }
}
