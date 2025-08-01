namespace SmartGym.Models;

public class OrderPatchDTO
{
  public decimal TotalPrice { get; set; }
  public DateTime OrderTime { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public string? Notes { get; set; }
}