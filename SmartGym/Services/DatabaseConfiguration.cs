using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartGym.Services.Interfaces;

namespace SmartGym.Services
{
	public class DatabaseConfiguration : IDatabaseConfiguration
	{
		public const string ConnectionStrings = "ConnectionStrings";
		public string DBConnectionString { get; set; }
	}
}