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

	#region Menu Items
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
	#endregion

	#region Orders
	/// <summary>
	/// Get entire user order history
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	[HttpGet("orders/user/{id:int}")]
	public async Task<ActionResult<IEnumerable<Order>>> GetAllUserOrders(int id)
	{
		var orderList = await _cafeService.GetAllUserOrders(id);
		return Ok(orderList);
	}
	/// <summary>
	/// Get order by user ID
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	[HttpGet("orders/{id:int}")]
	public async Task<ActionResult<OrderDTO>> GetOrderById(int id)
	{
		var orderItem = await _cafeService.GetOrderById(id);
		if (orderItem == null)
		{
			return NotFound();
		}
		return orderItem;
	}
	/// <summary>
	/// Get order's time by order Id
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	[HttpGet("orders/{id:int}/time")]
	public async Task<ActionResult<DateTime?>> GetOrderTime(int id)
	{
		var orderTime = await _cafeService.GetOrderTime(id);
		return orderTime;
	}
	/// <summary>
	/// Create a new order
	/// </summary>
	/// <param name="newOrderData"></param>
	/// <returns></returns>
	[HttpPost("orders/create")]
	public async Task<ActionResult<OrderDTO>> CreateOrder(OrderDTO newOrderData)
	{
		var created = await _cafeService.CreateOrder(newOrderData);
		return CreatedAtAction(nameof(GetOrderById), new { id = created.Id }, created);
	}

	[HttpPatch("orders/{id:int}")]
	public async Task<ActionResult<OrderDTO>> UpdateOrder(int id, [FromBody] OrderPatchDTO orderToPatch)
	{
		var patchedOrder = await _cafeService.UpdateOrder(id, orderToPatch);
		if (patchedOrder == null)
		{
			return NotFound();
		}
		return Ok(patchedOrder);
	}

	[HttpDelete("orders/{id:int}")]
	public async Task<IActionResult> DeleteOrder(int id)
	{
		var removed = await _cafeService.DeleteOrder(id);
		if (!removed)
			return NotFound();
		return Ok();
	}
	#endregion
}
