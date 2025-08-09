using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGym.Models
{
    public class WaitlistDTO
    {
		public int Id { get; set; }
		public int MemberId { get; set; }
		public AppUser Member { get; set; }
		public int SessionId { get; set; }
		public ClassSession Session { get; set; }
		public DateTime JoinedDateTime { get; set; }
		public int Position { get; set; }
	}
}