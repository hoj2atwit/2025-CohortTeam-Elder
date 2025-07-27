using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGym.Services.Interfaces
{
    public interface IDatabaseConfiguration
    {
		string DBConnectionString { get; set; }
	}
}