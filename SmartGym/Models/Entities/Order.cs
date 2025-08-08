using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartGym.Models;

[Table("Orders")]
public class Order
{
	[Required]
	public int Id { get; set; }
	[Required]
	public decimal TotalPrice { get; set; }
	public DateTime OrderTime { get; set; }
	[Required]
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
	public string OrderCart { get; set; }
	public string Notes { get; set; } = string.Empty;
	[Required]
	public int UserId { get; set; }
	public required AppUser User { get; set; }
}