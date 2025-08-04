using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartGym.Constants.Enums;

namespace SmartGym.Models;

[Table("Bookings")]
public class Booking
{
  [Required]
  public int Id { get; set; }
  [Required]
  public int UserId { get; set; }
  public User User { get; set; }
  [Required]
  public int ClassId { get; set; }
  public Class Class { get; set; }
  [Required]
  public BookingStatus Status { get; set; }
  [Required]
  public DateTime CreatedAt { get; set; }
  public DateTime ConfirmedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}