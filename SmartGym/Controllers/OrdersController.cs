using Microsoft.AspNetCore.Mvc;
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
		TotalPrice = 15.99m,
		OrderTime = DateTime.Now,
		CreatedAt = DateTime.Now,
		UpdatedAt = DateTime.Now,
		OrderCartList = new List<MenuItemsDTO>
				{
					 new MenuItemsDTO
					 {
						  Id = 1,
						  Name = "Latte",
						  Price = 4.50m,
						  Calories = 120,
						  Ingredients = "Espresso, Milk",
						  Description = "A creamy latte.",
						  Tags = "coffee,hot"
					 },
					 new MenuItemsDTO
					 {
						  Id = 2,
						  Name = "Protein Bar",
						  Price = 2.99m,
						  Calories = 200,
						  Ingredients = "Whey, Oats, Honey",
						  Description = "High-protein snack.",
						  Tags = "snack,protein"
					 }
				},
		OrderCart = null,
		Notes = "No sugar in the latte, please.",
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