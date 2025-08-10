using System;
using System.ComponentModel.DataAnnotations;
using SmartGym.Constants.Enums;

namespace SmartGym.Models;

public class UserStatusHistory
{
	[Required]
	public int Id { get; set; }
	[Required]
	public int UserId { get; set; }
	public AppUser User { get; set; }
	[Required]
	public UserStatus Status { get; set; }
	[Required]
	public DateTime EventDate { get; set; }
}
