using System;

namespace SmartGym.Models;

public class CheckinDTO
{
	public int Id { get; set; }
	public int UserId { get; set; }
	public DateTime CheckinTime { get; set; }
	public string Method { get; set; }
	public UserDto User { get; set; }
}
