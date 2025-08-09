using SmartGym.Constants.Enums;

namespace SmartGym.Models;

public class BookingDTO
{
	public int Id { get; set; }
	public int UserId { get; set; }
	public int ClassId { get; set; }
	public BookingStatus Status { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime ConfirmedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
	public int ClassSessionId { get; set; }
	
}