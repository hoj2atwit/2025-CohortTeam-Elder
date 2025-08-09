using SmartGym.Constants.Enums;

namespace SmartGym.Models;
public class ClassPatchDTO
{
	public string? Name { get; set; }
	public DateTime? Schedule { get; set; }
	public int? Capacity { get; set; }
	public int? TrainerId { get; set; }
	public ClassCategory? CategoryId { get; set; }
}