using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGym.Models
{
	public class ImagesDTO
	{
		public int Id { get; set; }
		public string ImageRef { get; set; } // basically filename guid+ext
		public byte[] Data { get; set; }
		public bool IsUserImage { get; set; }
		public DateTime UpdatedUtcDate { get; set; }

	}
}