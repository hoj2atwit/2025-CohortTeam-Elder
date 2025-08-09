using System;

namespace SmartGym.Models;

public class CartItemsDTO
{
	public int MenuItemId { get; set; }
	public string Name { get; set; } = string.Empty;
	public int Quantity { get; set; }
	private string _imageRef;
	public string ImageRef
	{
		get => string.IsNullOrEmpty(_imageRef) ? "default_food.svg" : _imageRef;
		set => _imageRef = value;
	}
}