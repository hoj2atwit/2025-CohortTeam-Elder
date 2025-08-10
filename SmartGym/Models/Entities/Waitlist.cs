using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGym.Models
{
	[Table("Waitlist")]
	public class Waitlist
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public int MemberId { get; set; }
		public AppUser Member { get; set; }
		[Required]
		public int SessionId { get; set; }
		public ClassSession Session { get; set; }
		public DateTime JoinedDateTime { get; set; }
		public int Position { get; set; }
	}
}