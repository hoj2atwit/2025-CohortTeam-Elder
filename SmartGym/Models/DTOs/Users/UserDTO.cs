using System;
using SmartGym.Constants.Enums;

namespace SmartGym.Models;

public class UserDto
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Email { get; set; }
	public int RoleId { get; set; }
	public DateTime DateOfBirth { get; set; }
	public int Status { get; set; }
	public DateTime CreatedDate { get; set; }
	public DateTime UpdatedDate { get; set; }
	// public ICollection<CheckinDTO> Checkins { get; set; } //undecided about this

	private string _imageRef;
	public string ImageRef // basically filename guid+ext
	{
		get
		{
			if (!string.IsNullOrEmpty(_imageRef))
				return _imageRef;

			return RoleId switch
			{
				(int)Constants.Enums.RoleId.Trainer => "trainer.svg",
				(int)Constants.Enums.RoleId.Staff => "staff.svg",
				(int)Constants.Enums.RoleId.Manager => "manager.svg",
				_ => "user.svg"
			};
		}
		set => _imageRef = value;
	}
}
