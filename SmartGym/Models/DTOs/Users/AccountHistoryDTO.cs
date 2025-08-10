using System;
using SmartGym.Constants.Enums;

namespace SmartGym.Models;

public class AccountHistoryDTO
{
	public int Id { get; set; }
	public int UserId { get; set; }
	public UserDto User { get; set; }
	public UserStatus Status { get; set; }
	public DateTime EventDate { get; set; }
}

