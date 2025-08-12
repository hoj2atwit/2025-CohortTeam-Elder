using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SmartGym.Constants;
using SmartGym.Models;
using SmartGym.Services;

namespace SmartGym.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
	private readonly IOrderService _service;

	public OrderDTO sample => new OrderDTO
	{
		TotalPrice = 199.99m,
		OrderTime = DateTime.Now,
		CreatedAt = DateTime.Now,
		UpdatedAt = DateTime.Now,
		OrderStatus = OrderStatus.Pending,
		OrderCartList = new List<CartItemsDTO>
		{
			new CartItemsDTO { MenuItemId = 1, Name = "latte", Quantity = 5, Price = 4.50m },
			new CartItemsDTO { MenuItemId = 2, Name = "espresso", Quantity = 4, Price = 3.00m },
			new CartItemsDTO { MenuItemId = 3, Name = "americano", Quantity = 3, Price = 3.50m },
			new CartItemsDTO { MenuItemId = 4, Name = "cappuccino", Quantity = 2, Price = 4.00m },
			new CartItemsDTO { MenuItemId = 5, Name = "flat white", Quantity = 6, Price = 4.25m },
			new CartItemsDTO { MenuItemId = 6, Name = "macchiato", Quantity = 2, Price = 3.75m },
			new CartItemsDTO { MenuItemId = 7, Name = "mocha", Quantity = 3, Price = 4.75m },
			new CartItemsDTO { MenuItemId = 8, Name = "cold brew", Quantity = 4, Price = 4.00m },
			new CartItemsDTO { MenuItemId = 9, Name = "nitro cold brew", Quantity = 2, Price = 4.50m },
			new CartItemsDTO { MenuItemId = 10, Name = "drip coffee", Quantity = 5, Price = 2.50m },
			new CartItemsDTO { MenuItemId = 11, Name = "iced coffee", Quantity = 3, Price = 3.25m },
			new CartItemsDTO { MenuItemId = 12, Name = "dirty chai", Quantity = 2, Price = 4.50m },
			new CartItemsDTO { MenuItemId = 13, Name = "matcha latte", Quantity = 4, Price = 4.75m },
			new CartItemsDTO { MenuItemId = 14, Name = "iced matcha", Quantity = 2, Price = 4.75m },
			new CartItemsDTO { MenuItemId = 15, Name = "hot chocolate", Quantity = 3, Price = 3.50m },
			new CartItemsDTO { MenuItemId = 16, Name = "iced chocolate", Quantity = 2, Price = 3.75m },
			new CartItemsDTO { MenuItemId = 17, Name = "turmeric latte", Quantity = 3, Price = 4.25m },
			new CartItemsDTO { MenuItemId = 18, Name = "golden milk", Quantity = 2, Price = 4.00m },
			new CartItemsDTO { MenuItemId = 19, Name = "london fog", Quantity = 4, Price = 4.25m },
			new CartItemsDTO { MenuItemId = 20, Name = "earl grey tea", Quantity = 2, Price = 2.75m },
			new CartItemsDTO { MenuItemId = 21, Name = "green tea", Quantity = 3, Price = 2.75m },
			new CartItemsDTO { MenuItemId = 22, Name = "black tea", Quantity = 2, Price = 2.75m }
		},
		OrderCart = null,
		Notes = "Please package drinks separately and label each item.",
		UserId = 5
	};

	public OrdersController(IOrderService service)
	{
		_service = service;
	}

	[HttpGet("user/{id:int}")]
	public async Task<ActionResult<IEnumerable<Order>>> GetAllUserOrders(int id)
	{
		var orderList = await _service.GetAllUserOrders(id);
		return Ok(orderList);
	}
	[HttpGet("{id:int}")]
	public async Task<ActionResult<OrderDTO>> GetOrderById(int id)
	{
		var orderItem = await _service.GetOrderById(id);
		if (orderItem == null)
		{
			return NotFound();
		}
		return orderItem;
	}
	[Authorize(Roles = "Admin, Staff")]
	[HttpGet("getAllOrders")]
	public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
	{
		var orderList = await _service.GetAllOrders();
		if (orderList == null || orderList.Count == 0)
		{
			return NotFound();
		}
		return Ok(orderList);
	}
	[HttpGet("getOrderByStatus/{status}")]
	public async Task<ActionResult<IEnumerable<Order>>> GetOrderByStatus(OrderStatus status)
	{
		var orderList = await _service.GetAllOrdersByStatus(status);
		if (orderList == null || orderList.Count == 0)
		{
			return NotFound();
		}
		return Ok(orderList);
	}
	[HttpGet("{id:int}/time")]
	public async Task<ActionResult<DateTime?>> GetOrderTime(int id)
	{
		var orderTime = await _service.GetOrderTime(id);
		return orderTime;
	}

	[HttpPost]
	public async Task<ActionResult<OrderDTO>> CreateOrder([FromBody] OrderDTO newOrderData, [FromQuery] bool useSample = false)
	{
		if (useSample)
		{
			newOrderData = sample;
		} 
		var created = await _service.CreateOrder(newOrderData);
		return CreatedAtAction(nameof(GetOrderById), new { id = created.Id }, created);
	}

	[HttpPatch("{id:int}")]
	public async Task<ActionResult<OrderDTO>> UpdateOrder(int id, [FromBody] OrderPatchDTO orderToPatch)
	{
		var patchedOrder = await _service.UpdateOrder(id, orderToPatch);
		if (patchedOrder == null)
		{
			return NotFound();
		}
		return Ok(patchedOrder);
	}

	[HttpDelete("{id:int}")]
	public async Task<IActionResult> DeleteOrder(int id)
	{
		var removed = await _service.DeleteOrder(id);
		if (!removed)
			return NotFound();
		return Ok();
	}
}