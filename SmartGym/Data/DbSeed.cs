using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Bogus;
using Microsoft.EntityFrameworkCore;
using SmartGym.Constants.Enums;
using SmartGym.Models;

namespace SmartGym.Data
{
	public class DbSeed
	{
		/// <summary>
		/// This is the method the program uses to seed the db anytime you run
		/// </summary>
		/// <param name="services"></param>
		/// <param name="isDevelopment"></param>
		public static void SeedDatabase(IServiceProvider services, bool isDevelopment = false)
		{
			if (isDevelopment)
			{
				using var scope = services.CreateScope();
				var context = scope.ServiceProvider.GetRequiredService<SmartGymContext>();
				// context.Database.Migrate(); // Catch up your database
				//If images arent present on the local machine, create the image
				UpdateImageFolder(context);
				//Users
				if (!context.Users.Any())
				{
					var faker = new Faker<User>()
						 .RuleFor(x => x.Name, f => f.Name.FullName())
						 .RuleFor(x => x.FirstName, f => f.Name.FirstName())
						 .RuleFor(x => x.LastName, f => f.Name.LastName())
						 .RuleFor(x => x.Email, f => f.Internet.Email())
						 .RuleFor(x => x.CreatedDate, f => f.Date.Between(default(DateTime), DateTime.Now.AddYears(9)))
						 .RuleFor(x => x.UpdatedDate, f => f.Date.Between(default(DateTime), DateTime.Now.AddYears(9)))
						 .RuleFor(x => x.DateOfBirth, f => f.Date.Between(default(DateTime), DateTime.Now.AddYears(9)))
						 .RuleFor(x => x.RoleId, f => (int)f.Random.Enum<RoleId>())
						 .RuleFor(x => x.Status, f => (int)f.Random.Enum<UserStatus>());
					var fakeUsers = faker.Generate(300); // Generate 100 random Users
					context.Users.AddRange(fakeUsers);
					context.SaveChanges();
				}

				//Clases
				List<string> classes = new() { "Amp It Up!", "Sweat N Sculpt", "Cardio Blast", "Power Surge", "Ignite Fitness", "Adrenaline Rush", "Torch N Tone", "Velocity HIIT", "Explosion Circuit", "Rhythm N Burn", "Grit N Grind", "Ironclad Core", "Sculpt N Define", "Strong Foundations", "Form N Fire", "Titan Training", "Muscle Mania", "Build N Burn", "Resilience Builders", "Powerhouse Physique", "Zen Zone Flow", "Harmony Stretch", "Inner Balance", "Calm N Core", "Mindful Movement", "Flexibility Fusion", "Serenity Sculpt", "Tranquil Strength", "Root N Rise", "Unwind N Restore", "Apex Athletics", "Synergy Session", "Kinetic Flow", "Metabolic Mayhem", "Bodyweight Blitz", "Urban Warrior", "Circuit Breaker", "The Grindhouse", "Fusion Fitness", "Primal Movement", "Warrior Waves", "Cardio Drumming", "Dance Dynamix", "Pilates Powerhouse", "Barre Burn", "Spin N Sculpt", "TRX Territory", "Kettlebell Kommandos", "Agility Ascent", "Gladiators Guild" };
				if (!context.Classes.Any())
				{
					var faker = new Faker<Class>()
						 .RuleFor(x => x.Name, f => f.Random.ListItem(classes))
						 .RuleFor(x => x.Schedule, f => f.Date.Between(default(DateTime), DateTime.Now.AddYears(9)))
						 .RuleFor(x => x.Capacity, f => f.Random.Int(20, 50))
						 .RuleFor(x => x.TrainerId, f => f.Random.Int(1, 100))
						 .RuleFor(x => x.CategoryId, f => f.Random.Int(1, 5));

					var fakeClasses = faker.Generate(20); // Generate 20 random classes
					context.Classes.AddRange(fakeClasses);
					context.SaveChanges();
				}

				//Orders
				if (!context.Orders.Any())
				{
					var users = context.Users.ToList();

					if (users.Any())
					{
						var orderFaker = new Faker<Order>()
						.RuleFor(o => o.User, f => f.PickRandom(users))
						.RuleFor(o => o.UserId, (f, o) => o.User.Id)
						.RuleFor(o => o.OrderTime, f => f.Date.Recent(90))
						.RuleFor(o => o.TotalPrice, f => f.Finance.Amount(10, 99))
						.RuleFor(o => o.CreatedAt, f => f.Date.Past(1))
						.RuleFor(o => o.UpdatedAt, (f, o) => o.CreatedAt.AddMinutes(f.Random.Int(10, 1000)))
						.RuleFor(o => o.Notes, f => f.Lorem.Sentence());

						var fakeOrders = orderFaker.Generate(50);
						context.Orders.AddRange(fakeOrders);
						context.SaveChanges();
					}
				}
				List<int> userIds = context.Users.Select(x => x.Id).ToList();
				if (!context.Checkins.Any())
				{
					var faker = new Faker<Checkin>()
						 .RuleFor(x => x.CheckinTime, f => f.Date.Between(default(DateTime), DateTime.Now.AddYears(9)))
						 .RuleFor(x => x.Method, f => f.Random.ListItem(new List<string>() { "qr", "desk" }))
						 .RuleFor(x => x.UserId, f => f.Random.ListItem(userIds));

					var fakeCheckins = faker.Generate(1000); // Generate 20 random classes
					context.Checkins.AddRange(fakeCheckins);
					context.SaveChanges();
				}

				List<string> menuItems = new()
				{
					"latte","espresso","americano","cappuccino","flat white","macchiato","mocha","cold brew","nitro cold brew","drip coffee","iced coffee","chai latte","dirty chai","matcha latte","iced matcha","hot chocolate","iced chocolate","turmeric latte","golden milk","london fog","earl grey tea","green tea","black tea","herbal tea","iced tea","lemonade","iced lemonade","sparkling water","still water","protein shake vanilla","protein shake chocolate","protein shake strawberry","protein shake mocha","protein shake peanut butter","protein shake cookies and cream","smoothie berry","smoothie mango","smoothie green","smoothie tropical","smoothie avocado","smoothie peanut butter banana","smoothie protein powder","smoothie collagen","oatmeal plain","oatmeal with berries","oatmeal with nuts","oatmeal with seeds","oatmeal with protein powder","greek yogurt plain","greek yogurt with honey","greek yogurt with granola","greek yogurt with fruit","parfait with yogurt","parfait with granola","parfait with berries","acai bowl","pitaya bowl","smoothie bowl","chia seed pudding","overnight oats","avocado toast","avocado toast with egg","avocado toast with tomato","avocado toast with feta","avocado toast with chili flakes","egg white omelette","scrambled eggs","hard-boiled eggs","turkey bacon","chicken sausage","protein pancakes","protein waffles","spinach and feta wrap","turkey and cheese wrap","chicken caesar wrap","vegetarian wrap","hummus and veggie wrap","tuna salad sandwich","chicken salad sandwich","turkey and avocado sandwich","grilled cheese","BLT sandwich","veggie burger","beef burger","chicken burger","salmon filet","grilled chicken breast","sweet potato fries","quinoa salad","lentil soup","tomato soup","chicken noodle soup","side salad","fruit cup","apple slices","banana","orange","protein bar","protein cookie","energy bites","trail mix","almonds","walnuts","cashews","jerky","rice cakes","rice cakes with peanut butter","rice cakes with avocado","dark chocolate squares","sugar-free gummies","kombucha","coconut water","electrolyte drink","pre-workout drink","post-workout drink","collagen water","almond milk","soy milk","oat milk","coconut milk","protein powder scoop","collagen scoop","BCAA powder","creatine powder","pre-workout scoop","espresso shot","extra shot of syrup","extra shot of flavor","extra shot of espresso","extra shot of protein","extra shot of collagen","extra shot of BCAA","extra shot of creatine","extra shot of whipped cream","extra shot of toppings","extra shot of nuts","extra shot of seeds","extra shot of fruit","extra shot of granola","extra shot of honey","extra shot of maple syrup","extra shot of agave","extra shot of stevia","extra shot of monk fruit","extra shot of sugar","extra shot of salt","extra shot of pepper"
				};
				var uniqueMenuItems = menuItems.Distinct().ToList();
				if (!context.MenuItems.Any())
				{
					var faker = new Faker<MenuItem>()
						 .RuleFor(m => m.Price, f => f.Finance.Amount(1.50m, 15.00m, 2))
						 .RuleFor(m => m.Calories, f => f.Random.Int(100, 3000))
						 .RuleFor(m => m.Ingredients, f => string.Join(", ", f.Commerce.ProductMaterial()))
						 .RuleFor(m => m.Description, f => f.Lorem.Sentence(5))
						 .RuleFor(m => m.Tags, f => string.Join(", ", f.Random.Words(3)));

					var fakeMenuItems = faker.Generate(uniqueMenuItems.Count());
					for (int i = 0; i < uniqueMenuItems.Count(); i++)
					{
						fakeMenuItems[i].Name = uniqueMenuItems[i];
					}
					context.MenuItems.AddRange(fakeMenuItems);
					context.SaveChanges();
				}

				if (!context.Bookings.Any())
				{
					var users = context.Users.ToList();
					var classesToBook = context.Classes.ToList();
					var bookings = new List<Booking>();
					var random = new Random();

					foreach (var gymClass in classesToBook)
					{
						int numberOfBookings = random.Next(0, gymClass.Capacity);

						var bookedUserIds = users.OrderBy(_ => Guid.NewGuid())
												 .Take(numberOfBookings)
												 .Select(u => u.Id)
												 .ToList();

						foreach (var userId in bookedUserIds)
						{
							var status = (BookingStatus)random.Next(0, Enum.GetValues(typeof(BookingStatus)).Length);
							var createdAt = DateTime.Now.AddDays(-random.Next(1, 30));
							var confirmedAt = (status == BookingStatus.Confirmed)
								? createdAt.AddMinutes(random.Next(5, 120))
								: DateTime.MinValue;

							bookings.Add(new Booking
							{
								UserId = userId,
								ClassId = gymClass.Id,
								Status = status,
								CreatedAt = createdAt,
								ConfirmedAt = confirmedAt,
								UpdatedAt = createdAt.AddMinutes(random.Next(10, 500))
							});
						}
					}

					context.Bookings.AddRange(bookings);
					context.SaveChanges();
				}

			}
		}

		private static void UpdateImageFolder(SmartGymContext context)
		{
			var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lib", "images");
			if (!Directory.Exists(wwwrootPath))
			{
				Directory.CreateDirectory(wwwrootPath);
			}
			var fileImages = Directory.GetFiles(wwwrootPath)
				.Select(f => Path.GetFileName(f))
				.ToHashSet(StringComparer.OrdinalIgnoreCase);

			var dbImages = context.Images.ToList();

			foreach (var record in dbImages)
			{
				// Remove any files with the same gui regardless of extension
				var guid = Path.GetFileNameWithoutExtension(record.ImageRef);
				var matchingFiles = Directory.GetFiles(wwwrootPath, guid + ".*", SearchOption.TopDirectoryOnly);

				foreach (var file in matchingFiles)
				{
					if (!file.EndsWith(record.ImageRef, StringComparison.OrdinalIgnoreCase)) //if guid matches but ext does not
					{
						File.Delete(file);
					}
				}

				var imagePath = Path.Combine(wwwrootPath, record.ImageRef);
				bool fileExists = File.Exists(imagePath);
				if (!fileExists || record.UpdatedUtcDate.ToUniversalTime() > File.GetLastWriteTimeUtc(imagePath))
				if (!fileExists || record.UpdatedUtcDate > File.GetLastWriteTimeUtc(imagePath))
				{
					File.WriteAllBytes(imagePath, record.Data);
					File.SetLastWriteTimeUtc(imagePath, record.UpdatedUtcDate); // keep timestamps in sync
				}
			}
		}
	}
}