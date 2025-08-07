using System;

namespace SmartGym.Models;

public class MenuItemsDTO
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public decimal Price { get; set; }
	public int Calories { get; set; }
	public string Ingredients { get; set; }
	public string Description { get; set; }
	public string Tags { get; set; }
	public int StockLevel { get; set; }
	private string _imageRef;
	public string ImageRef // basically filename guid+ext
	{
		get => string.IsNullOrEmpty(_imageRef) ? "default_food.svg" : _imageRef;
		set => _imageRef = value;
	}
}
