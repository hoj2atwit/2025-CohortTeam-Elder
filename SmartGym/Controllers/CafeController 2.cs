using System;
using SmartGym.Services;

namespace SmartGym.Controllers;

public class CafeController
{
	private readonly ICafeService _service;


	public CafeController(ICafeService service)
	{
		_service = service;
	}

}
