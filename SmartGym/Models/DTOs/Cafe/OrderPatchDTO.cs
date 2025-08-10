using SmartGym.Constants;

namespace SmartGym.Models;

public class OrderPatchDTO
{
	public decimal TotalPrice { get; set; }
	public DateTime OrderTime { get; set; }
	public DateTime UpdatedAt { get; set; }
	public OrderStatus OrderStatus { get; set; }
	public List<CartItemsDTO>? OrderCartList { get; set; }
	public string? Notes { get; set; }
}