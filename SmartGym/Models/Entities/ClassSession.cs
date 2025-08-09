using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGym.Models
{
	public class ClassSession
	{
		public int SessionId { get; set; }
		public int ClassId { get; set; }
		public Class Class { get; set; }
		public int InstructorId { get; set; }
		public DateTime SessionDateTime { get; set; }
		public int Capacity { get; set; }
		public int LocationId { get; set; }

	}
}