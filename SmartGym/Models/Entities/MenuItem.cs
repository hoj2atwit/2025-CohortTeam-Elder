using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartGym.Models;
[Table("MenuItems")]
public class MenuItem
{
	[Required]
	public int Id { get; set; }
	[Required]
	[MaxLength(100)]
	public string Name { get; set; } = string.Empty;
	[Required]
	public decimal Price { get; set; }
	[Required]
	public int Calories { get; set; }
	[Required]
	public int Ingredients { get; set; }
	[Required]
	public string Description { get; set; }
	public int Tag { get; set; }
}

