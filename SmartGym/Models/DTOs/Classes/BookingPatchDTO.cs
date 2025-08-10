using SmartGym.Constants.Enums;

namespace SmartGym.Models;

public class BookingPatchDTO
{
	public int Id { get; set; }
	public int UserId { get; set; }
	public int ClassId { get; set; }
	public BookingStatus Status { get; set; }
	public DateTime ConfirmedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
}