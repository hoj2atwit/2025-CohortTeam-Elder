using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartGym.Models;

[Table("Classes")]
public class Class
{
	[Required]
	public int Id { get; set; }
	[Required]
	[MaxLength(150)]
	public string Name { get; set; } = string.Empty;
	[Required]
	public DateTime Schedule { get; set; }
	[Required]
	public int Capacity { get; set; }
	[Required]
	public int TrainerId { get; set; }
	[Required]
	public User Trainer { get; set; }
	public int? CategoryId { get; set; }
}