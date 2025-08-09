using System.ComponentModel.DataAnnotations;

namespace SmartGym.Constants.Enums
{
	public enum ClassCategory
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