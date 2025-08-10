using System.ComponentModel.DataAnnotations;

namespace SmartGym.Constants.Enums
{
	public enum SkillLevel
	{
		[Display(Name = "Beginner")]
		Beginner,
		[Display(Name = "Intermediate")]
		Intermediate,
		[Display(Name = "Advanced")]
		Advanced,
		[Display(Name = "Death Wish")]
		DeathWish
	}
}