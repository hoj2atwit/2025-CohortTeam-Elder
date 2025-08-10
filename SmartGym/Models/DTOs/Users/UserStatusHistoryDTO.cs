using System;
using SmartGym.Constants.Enums;

namespace SmartGym.Models;

public class MemberHistoryDTO
{
	public class MemberStatusHistory
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public AppUser User { get; set; }
		public UserStatus Status { get; set; }
		public DateTime EventDate { get; set; }
	}
}
