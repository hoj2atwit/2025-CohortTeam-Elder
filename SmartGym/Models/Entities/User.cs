using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartGym.Models;

[Table("Users")]
public class User
{
	public int Id { get; set; }
	[Required]
	[MaxLength(100)]
	public string Name { get; set; }
	[Required]
	[MaxLength(100)]
	public string FirstName { get; set; }
	[Required]
	[MaxLength(100)]
	public string LastName { get; set; }
	[Required]
	[MaxLength(100)]
	public string Email { get; set; }
	[Required]
	[MaxLength(50)]
	public int RoleId { get; set; }
	[Required]
	public DateTime DateOfBirth { get; set; }
	[Required]
	public int Status { get; set; }
	[Required]
	public DateTime CreatedDate { get; set; }
	[Required]
	public DateTime UpdatedDate { get; set; }
	public ICollection<Checkin>? Checkins { get; set; }
}
