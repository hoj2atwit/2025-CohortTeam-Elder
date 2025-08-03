using System;

namespace SmartGym.Models;

public class MenuItemsDTO
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public decimal Price { get; set; }
	public int Calories { get; set; }
	public int Ingredients { get; set; }
	public string Description { get; set; }
	public int Tag { get; set; }
}
