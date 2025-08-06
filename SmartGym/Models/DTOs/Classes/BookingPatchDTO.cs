namespace SmartGym.Models;

public class BookingPatchDTO
{
  public int UserId { get; set; }
  public int ClassId { get; set; }
  public int Status { get; set; }
  public DateTime ConfirmedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}