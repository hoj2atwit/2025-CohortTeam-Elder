using System.ComponentModel.DataAnnotations;

namespace SmartGym.Constants.Enums;

public enum RoleId
{
	[Display(Name = "Base")]
	MemberBase,
	[Display(Name = "Plus")]
	MemberPlus,
	[Display(Name = "Premium")]
	MemberPremium,
	[Display(Name = "Staff Member")]
	Staff,
	[Display(Name = "Trainer")]
	Trainer,
	[Display(Name = "Manager")]
	Manager
}
