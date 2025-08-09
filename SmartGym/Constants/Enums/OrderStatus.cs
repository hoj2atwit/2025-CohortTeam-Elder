using System.ComponentModel.DataAnnotations;

namespace SmartGym.Constants
{
	public enum OrderStatus
	{
		[Display(Name = "Incomplete")]
		Incomplete,
		[Display(Name = "Pending")]
		Pending,
		[Display(Name = "Completed")]
		Completed,
		[Display(Name = "Cancelled")]
		Cancelled
	}
}