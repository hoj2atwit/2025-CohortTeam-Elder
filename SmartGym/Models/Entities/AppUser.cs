using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using SmartGym.Constants.Enums;

namespace SmartGym.Models;

public class AppUser : IdentityUser<int>
{
	[Required]
	[MaxLength(100)]
	public string FirstName { get; set; }
	[Required]
	[MaxLength(100)]
	public string LastName { get; set; }
	[NotMapped]
	[MaxLength(100)]
	public string Name => $"{FirstName} {LastName}";
	[Required]
	public DateTime DateOfBirth { get; set; }
	[Required]
	public UserStatus Status { get; set; }
	public DateTime CreatedDate { get; set; }
	public DateTime UpdatedDate { get; set; }
	public ICollection<Checkin>? Checkins { get; set; }
	public string? ImageRef { get; set; }
}
