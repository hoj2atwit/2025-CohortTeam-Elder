using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartGym.Models;

namespace SmartGym.Data;

public class SmartGymContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
{
	public SmartGymContext(DbContextOptions<SmartGymContext> options) : base(options)
	{

	}
	public DbSet<Class>? Classes { get; set; }
	// public DbSet<AppUser>? Users { get; set; }
	public DbSet<Order>? Orders { get; set; }
	public DbSet<Checkin>? Checkins { get; set; }
	public DbSet<MenuItem>? MenuItems { get; set; }
	public DbSet<Images>? Images { get; set; }
	public DbSet<Booking>? Bookings { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.Entity<Class>()
			.HasOne(c => c.Trainer)
			.WithMany()
			.HasForeignKey(c => c.TrainerId)
			.OnDelete(DeleteBehavior.Restrict);
	}

}