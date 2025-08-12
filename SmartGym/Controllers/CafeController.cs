using System;
using Microsoft.AspNetCore.Mvc;
using SmartGym.Models;
using SmartGym.Services;

namespace SmartGym.Controllers;

[ApiController]
[Route("[controller]")]
public class CafeController : ControllerBase
{
	private readonly ICafeService _cafeService;

	public CafeController(ICafeService cafeService)
	{
		_cafeService = cafeService;
	}

	[HttpGet("menu")]
	public async Task<ActionResult<IEnumerable<Order>>> GetFullMenu()
	{
		var menuItems = await _cafeService.GetFullMenu();
		return Ok(menuItems);
	}
	[HttpGet("menu/{id:int}")]
	public async Task<ActionResult<MenuItemsDTO>> GetMenuItem(int id)
	{
		var menuItem = await _cafeService.GetMenuItem(id);
		if (menuItem == null)
		{
			return NotFound();
		}
		return menuItem;
	}
	[HttpPatch("menu/{id:int}")]
	public async Task<ActionResult<MenuItemsDTO>> UpdateMenuItem(int id, [FromBody] MenuItemsDTO menuItemDto)
	{
		var menuItem = await _cafeService.UpdateMenuItem(id, menuItemDto);
		if (menuItem == null)
		{
			return NotFound();
		}
		return Ok(menuItem);
	}

	[HttpDelete("menu/{id:int}")]
	public async Task<IActionResult> DeleteMenuItem(int id)
	{
		var removed = await _cafeService.DeleteMenuItem(id);
		if (!removed)
			return NotFound();
		return Ok();
	}

	// public void GetCafeItemModifications()
	// {
	// 	throw new NotImplementedException();
	// }

	// public void GetCurrentPromos()
	// {
	// 	throw new NotImplementedException();
	// }

	// public void GetMealPrepItems()
	// {
	// 	throw new NotImplementedException();
	// }
}
