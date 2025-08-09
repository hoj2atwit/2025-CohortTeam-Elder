using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartGym.Constants.Enums;

namespace SmartGym.Models
{
    public class ClassSessionDTO
    {
		public int Id { get; set; }
		public int ClassId { get; set; }
		public int InstructorId { get; set; }
		public DateTime SessionDateTime { get; set; }
		public int Capacity { get; set; }
		public AccessPoint LocationId { get; set; }
	}
}