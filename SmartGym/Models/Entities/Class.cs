using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartGym.Constants.Enums;

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
	public int MaxCapacity { get; set; }
	[Required]
	public int TrainerId { get; set; }
	[Required]
	public AppUser Trainer { get; set; }
	public SkillLevel? Level { get; set; }
	public string Description { get; set; } = string.Empty;
	public string? ImageRef { get; set; }

}