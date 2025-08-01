using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartGym.Models;

[Table("Users")]
public class User
{
	public int Id { get; set; }
	[Required]
	[MaxLength(150)]
	public string Name { get; set; }
	[Required]
	[MaxLength(100)]
	public string Email { get; set; }
	[Required]
	[MaxLength(50)]
	public string Role { get; set; }
}
