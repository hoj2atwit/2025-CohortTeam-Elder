using System;
using System.ComponentModel.DataAnnotations;

namespace SmartGym.Models;

public class UserPreferences
{
	[Required]
	public int Id { get; set; }
}
