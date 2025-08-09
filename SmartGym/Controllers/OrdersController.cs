using Microsoft.AspNetCore.Mvc;
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
		OrderCartList = new List<CartItemsDTO>
	 {
		  new CartItemsDTO { MenuItemId = 1, Quantity = 5 },   // latte
        new CartItemsDTO { MenuItemId = 2, Quantity = 4 },   // espresso
        new CartItemsDTO { MenuItemId = 3, Quantity = 3 },   // americano
        new CartItemsDTO { MenuItemId = 4, Quantity = 2 },   // cappuccino
        new CartItemsDTO { MenuItemId = 5, Quantity = 6 },   // flat white
        new CartItemsDTO { MenuItemId = 6, Quantity = 2 },   // macchiato
        new CartItemsDTO { MenuItemId = 7, Quantity = 3 },   // mocha
        new CartItemsDTO { MenuItemId = 8, Quantity = 4 },   // cold brew
        new CartItemsDTO { MenuItemId = 9, Quantity = 2 },   // nitro cold brew
        new CartItemsDTO { MenuItemId = 10, Quantity = 5 },  // drip coffee
        new CartItemsDTO { MenuItemId = 11, Quantity = 3 },  // iced coffee
        new CartItemsDTO { MenuItemId = 12, Quantity = 2 },  // dirty chai
        new CartItemsDTO { MenuItemId = 13, Quantity = 4 },  // matcha latte
        new CartItemsDTO { MenuItemId = 14, Quantity = 2 },  // iced matcha
        new CartItemsDTO { MenuItemId = 15, Quantity = 3 },  // hot chocolate
        new CartItemsDTO { MenuItemId = 16, Quantity = 2 },  // iced chocolate
        new CartItemsDTO { MenuItemId = 17, Quantity = 3 },  // turmeric latte
        new CartItemsDTO { MenuItemId = 18, Quantity = 2 },  // golden milk
        new CartItemsDTO { MenuItemId = 19, Quantity = 4 },  // london fog
        new CartItemsDTO { MenuItemId = 20, Quantity = 2 },  // earl grey tea
        new CartItemsDTO { MenuItemId = 21, Quantity = 3 },  // green tea
        new CartItemsDTO { MenuItemId = 22, Quantity = 2 },  // black tea
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
	[HttpGet("getOrderByStatus/{id:int}")]
	public async Task<ActionResult<IEnumerable<Order>>> GetOrderByStatus(int id)
	{
		var orderList = await _service.GetAllOrdersByStatus((OrderStatus)id);
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
	public async Task<ActionResult<OrderDTO>> CreateOrder(OrderDTO newOrderData)
	{
		if (newOrderData.Id == null || newOrderData.Id == 0)
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