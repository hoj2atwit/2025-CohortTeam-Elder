using System.ComponentModel.DataAnnotations;

namespace SmartGym.Constants.Enums
{
	public enum AccessPoint
	{
		[Display(Name = "Front Desk")]
		FrontDesk,
		[Display(Name = "Website Portal")]
		Website,
		[Display(Name = "Rock Climbing Wall")]
		RockClimbing,
		[Display(Name = "Swimming Pool")]
		Pool,
		[Display(Name = "Sauna Room")]
		Sauna,
		[Display(Name = "Recovery Zone")]
		Recovery,
		[Display(Name = "Staff Login")]
		Staff,
		[Display(Name = "Towel Service Desk")]
		TowelService,
		[Display(Name = "Fitness Class")]
		Class
	}
}