using System;

namespace SmartGym.Models;

public class UserDto
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Email { get; set; }
	public int RoleId { get; set; }
	public DateTime DateOfBirth { get; set; }
	public int Status { get; set; }
	public DateTime CreatedDate { get; set; }
	public DateTime UpdatedDate { get; set; }
}
