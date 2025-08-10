using System.ComponentModel.DataAnnotations;

namespace SmartGym.Constants.Enums;

public enum UserStatus
{
	[Display(Name = "On-Hold")]
	Hold,
	[Display(Name = "Active")]
	Active,
	[Display(Name = "Inactive")]
	Inactive,
	[Display(Name = "Suspended")]
	Suspended,
	[Display(Name = "Banned")]
	Banned,
	[Display(Name = "New")]
	New
}
