using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGym.Models
{
	public class Waitlist
	{
		[Required]
		public int WaitlistId { get; set; }
		[Required]
		public int MemberId { get; set; }
		public AppUser Member { get; set; }
		public int SessionId { get; set; }
		public DateTime JoinedDateTime { get; set; }
		public int Position { get; set; }
	}
}