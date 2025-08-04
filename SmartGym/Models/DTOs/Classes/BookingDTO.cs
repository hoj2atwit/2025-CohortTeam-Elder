namespace SmartGym.Models;

public class BookingDTO
{
  public int Id { get; set; }
  public int UserId { get; set; }
  public int ClassId { get; set; }
  public int Status { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime ConfirmedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}