using Microsoft.AspNetCore.Mvc;
using SmartGym.Models;
using SmartGym.Services;

namespace SmartGym.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
  private readonly IOrderService _service;

  public OrdersController(IOrderService service)
  {
    _service = service;
  }

  [HttpGet("user/{userId:int}")]
  public async Task<ActionResult<IEnumerable<Order>>> GetAllUserOrders(int userId)
  {
    var orderList = await _service.GetAllUserOrders(userId);
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