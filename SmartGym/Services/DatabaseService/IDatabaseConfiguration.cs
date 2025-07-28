using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGym.Services
{
	public interface IDatabaseConfiguration
	{
		string DBConnectionString { get; set; }
	}
}