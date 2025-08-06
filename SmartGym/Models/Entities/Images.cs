using System;

namespace SmartGym.Models
{
	public class Images
	{
		public int Id { get; set; }
		public string ImageRef { get; set; } // basically filename guid+ext
		public byte[] Data { get; set; }
		public bool IsUserImage { get; set; }
		public DateTime UpdatedUtcDate { get; set; }
	}
}
