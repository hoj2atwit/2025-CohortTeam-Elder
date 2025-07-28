using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartGym.Models;

[Table("Users")]
public class User
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Email { get; set; }
	public string Role { get; set; }
}
