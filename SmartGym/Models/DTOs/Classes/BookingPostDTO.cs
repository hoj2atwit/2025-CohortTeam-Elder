namespace SmartGym.Models;

public class BookingPostDTO
{
	public int Id { get; set; }
	public int UserId { get; set; }
	public int SessionId { get; set; }
	public DateTime CreatedAt { get; set; }
}