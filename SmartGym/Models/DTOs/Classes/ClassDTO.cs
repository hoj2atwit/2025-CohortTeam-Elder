using SmartGym.Constants.Enums;

namespace SmartGym.Models;

public class ClassDTO
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public DateTime Schedule { get; set; }
	public int MaxCapacity { get; set; }
	public int TrainerId { get; set; }
	public SkillLevel? Level { get; set; }
	public string Description { get; set; } = string.Empty;
	public string? ImageRef { get; set; }

}