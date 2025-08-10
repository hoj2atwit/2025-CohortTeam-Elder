using SmartGym.Constants.Enums;

namespace SmartGym.Models;

public class ClassPatchDTO
{
	public string? Name { get; set; }
	public DateTime? Schedule { get; set; }
	public int? MaxCapacity { get; set; }
	public int? TrainerId { get; set; }
	public SkillLevel? CategoryId { get; set; }
	public string Description { get; set; } = string.Empty;
	public string? ImageRef { get; set; }


}