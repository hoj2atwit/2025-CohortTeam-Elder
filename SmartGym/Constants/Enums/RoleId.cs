using System.ComponentModel.DataAnnotations;

namespace SmartGym.Constants.Enums;

public enum RoleId
{
	[Display(Name = "Base")]
	Member,
	[Display(Name = "Plus")]
	MemberPlus,
	[Display(Name = "Premium")]
	MemberPremium,
	[Display(Name = "Staff Member")]
	Staff,
	[Display(Name = "Trainer")]
	Trainer,
	[Display(Name = "Manager")]
	Manager,
	[Display(Name = "Admin")]
	Admin
}
