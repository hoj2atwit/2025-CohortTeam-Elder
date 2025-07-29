using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Microsoft.EntityFrameworkCore;
using SmartGym.Models;

namespace SmartGym.Data
{
	public class DbSeed
	{
		public static void SeedDatabase(IServiceProvider services, bool isDevelopment = false)
		{
			if (isDevelopment)
			{
				using var scope = services.CreateScope();
				var context = scope.ServiceProvider.GetRequiredService<SmartGymContext>();

				//DROP if
				// context.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS [Users];");
				// context.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS [Classes];");

				// context.Database.Migrate(); // Catch up your database

				//Reseed the database with fresh data
				if (!context.Users.Any())
				{
					var faker = new Faker<User>()
						 .RuleFor(x => x.Name, f => f.Name.FullName())
						 .RuleFor(x => x.Email, f => f.Internet.Email())
						 .RuleFor(x => x.Role, f => f.Random.ListItem(new List<string>() { "member", "trainer", "staff", "manager" }));

					var fakeUsers = faker.Generate(100); // Generate 50 random Users
					context.Users.AddRange(fakeUsers);
					context.SaveChanges();
				}

				List<string> classes = new() { "Amp It Up!", "Sweat N Sculpt", "Cardio Blast", "Power Surge", "Ignite Fitness", "Adrenaline Rush", "Torch N Tone", "Velocity HIIT", "Explosion Circuit", "Rhythm N Burn", "Grit N Grind", "Ironclad Core", "Sculpt N Define", "Strong Foundations", "Form N Fire", "Titan Training", "Muscle Mania", "Build N Burn", "Resilience Builders", "Powerhouse Physique", "Zen Zone Flow", "Harmony Stretch", "Inner Balance", "Calm N Core", "Mindful Movement", "Flexibility Fusion", "Serenity Sculpt", "Tranquil Strength", "Root N Rise", "Unwind N Restore", "Apex Athletics", "Synergy Session", "Kinetic Flow", "Metabolic Mayhem", "Bodyweight Blitz", "Urban Warrior", "Circuit Breaker", "The Grindhouse", "Fusion Fitness", "Primal Movement", "Warrior Waves", "Cardio Drumming", "Dance Dynamix", "Pilates Powerhouse", "Barre Burn", "Spin N Sculpt", "TRX Territory", "Kettlebell Kommandos", "Agility Ascent", "Gladiators Guild" };
				if (!context.Classes.Any())
				{
					var faker = new Faker<Class>()
						 .RuleFor(x => x.Name, f => f.Random.ListItem(classes))
						 .RuleFor(x => x.Schedule, f => f.Date.Between(default(DateTime), DateTime.Now.AddYears(9)))
						 .RuleFor(x => x.Capacity, f => f.Random.Int(20,50))
						 .RuleFor(x => x.TrainerId, f => f.Random.Int(1,10))
						 .RuleFor(x => x.CategoryId, f => f.Random.Int(1,5));

					var fakeClasses = faker.Generate(20); // Generate 50 random classes
					context.Classes.AddRange(fakeClasses);
					context.SaveChanges();
				}
			}
		}
	}
}