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
	private readonly IOrderService _orderService;

	public CafeController(ICafeService cafeService, IOrderService orderService)
	{
		_cafeService = cafeService;
		_orderService = orderService;
	}



	#region Orders
	/// <summary>
	/// Get entire user order history
	/// </summary>
	/// <param name="userId"></param>
	/// <returns></returns>
	[HttpGet("orders/user/{userId:int}")]
	public async Task<ActionResult<IEnumerable<Order>>> GetAllUserOrders(int userId)
	{
		var orderList = await _orderService.GetAllUserOrders(userId);
		return Ok(orderList);
	}
	/// <summary>
	/// Get order by user ID
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	[HttpGet("orders/{orderId:int}")]
	public async Task<ActionResult<OrderDTO>> GetOrderById(int id)
	{
		var orderItem = await _orderService.GetOrderById(id);
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
	[HttpGet("orders/{orderId:int}/time")]
	public async Task<ActionResult<DateTime?>> GetOrderTime(int id)
	{
		var orderTime = await _orderService.GetOrderTime(id);
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
		var created = await _orderService.CreateOrder(newOrderData);
		return CreatedAtAction(nameof(GetOrderById), new { id = created.Id }, created);
	}

	[HttpPatch("orders/{orderId:int}")]
	public async Task<ActionResult<OrderDTO>> UpdateOrder(int id, [FromBody] OrderPatchDTO orderToPatch)
	{
		var patchedOrder = await _orderService.UpdateOrder(id, orderToPatch);
		if (patchedOrder == null)
		{
			return NotFound();
		}
		return Ok(patchedOrder);
	}

	[HttpDelete("orders/{id:int}")]
	public async Task<IActionResult> DeleteOrder(int id)
	{
		var removed = await _orderService.DeleteOrder(id);
		if (!removed)
			return NotFound();
		return Ok();
	}
	#endregion
}
