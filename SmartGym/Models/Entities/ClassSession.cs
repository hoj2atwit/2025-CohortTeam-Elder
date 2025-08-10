using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using SmartGym.Constants.Enums;

namespace SmartGym.Models
{
	[Table("ClassSessions")]
	public class ClassSession
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public int ClassId { get; set; }
		public Class Class { get; set; }
		[Required]
		public int InstructorId { get; set; }
		[Required]
		public DateTime SessionDateTime { get; set; }
		[Required]
		public int HeadCount { get; set; }
		public int MaxCapacity { get; set; }
		[Required]
		public AccessPoint LocationId { get; set; }
		public string Description { get; set; } = string.Empty;


	}
}