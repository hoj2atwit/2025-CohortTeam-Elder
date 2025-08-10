using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartGym.Constants.Enums;

namespace SmartGym.Models;

[Table("UserHistory")]
public class AccountHistory
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
