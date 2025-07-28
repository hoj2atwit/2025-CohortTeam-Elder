using Microsoft.EntityFrameworkCore;
using SmartGym.Models;

namespace SmartGym.Data;

public class SmartGymContext : DbContext
public class SmartGymContext : DbContext
{
	public SmartGymContext(DbContextOptions<SmartGymContext> options) : base(options)
	{

	}
	public DbSet<Class>? Classes { get; set; }
	public DbSet<User>? Users { get; set; }

}