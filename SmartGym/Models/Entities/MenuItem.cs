using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartGym.Models;

[Table("MenuItems")]
[Index(nameof(Name), IsUnique = true)]
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
	public string Ingredients { get; set; }
	[Required]
	public string Description { get; set; }
	public string Tags { get; set; }
}

