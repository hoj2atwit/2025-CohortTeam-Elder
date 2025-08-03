using SmartGym.Models;

namespace SmartGym.Models;

public class OrderDTO
{
	public int Id { get; set; }
	public decimal TotalPrice { get; set; }
	public DateTime OrderTime { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
	public List<MenuItemsDTO> OrderCartList { get; set; }
	public string? OrderCart { get; set; }
	public string? Notes { get; set; }
	public int UserId { get; set; }
}