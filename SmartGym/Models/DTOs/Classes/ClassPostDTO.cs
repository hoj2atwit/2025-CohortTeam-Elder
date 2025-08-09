using SmartGym.Constants.Enums;

namespace SmartGym.Models;

public class ClassPostDTO
{
  public string Name { get; set; } = string.Empty;
  public DateTime Schedule { get; set; }
  public int Capacity { get; set; }
  public int TrainerId { get; set; }
  public ClassCategory? CategoryId { get; set; }
}