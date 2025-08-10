using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Bogus;
using Microsoft.EntityFrameworkCore;
using SmartGym.Constants.Enums;
using SmartGym.Models;
using Microsoft.AspNetCore.Identity;

namespace SmartGym.Data
{
	public class DbSeed
	{
		/// <summary>
		/// This is the method the program uses to seed the db anytime you run
		/// </summary>
		/// <param name="services"></param>
		/// <param name="isDevelopment"></param>
		public static async Task SeedDatabaseAsync(IServiceProvider services, bool isDevelopment = false)
		{
			if (!isDevelopment) return;

			using var scope = services.CreateScope();
			var serviceProvider = scope.ServiceProvider;

			var context = serviceProvider.GetRequiredService<SmartGymContext>();
			var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
			var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

			//Roles
			// Use RoleId enum and EnumHelper to get role names
			var roleNames = Enum.GetValues(typeof(RoleId))
				.Cast<RoleId>()
				.Select(role => SmartGym.Helpers.EnumHelper.GetDisplayName(role))
				.ToArray();
			foreach (var role in roleNames)
			{
				if (!await roleManager.RoleExistsAsync(role))
				{
					await roleManager.CreateAsync(new IdentityRole<int> { Name = role });
				}
			}

			// context.Database.Migrate(); // Catch up your database
			//If images arent present on the local machine, create the image
			UpdateImageFolder(context);
			//Users
			if (!userManager.Users.Any())
			{
				 Dictionary<string, int> roleCounts = new()
				{
					{ "Staff Member", 0 },
					{ "Trainer", 0 },
					{ "Manager", 0 },
					{ "Admin", 0 }
				};
				Dictionary<string, int> roleCaps = new()
				{
					{ "Staff Member", 100 },
					{ "Trainer", 25 },
					{ "Manager", 5 },
					{ "Admin", 3 }
				};
				var faker = new Faker();
				for (int i = 0; i < 500; i++)
				{
					var firstName = faker.Name.FirstName();
					var lastName = faker.Name.LastName();
					var email = faker.Internet.Email(firstName, lastName);
					var date = faker.Date.Between(DateTime.UtcNow.AddMonths(-3), DateTime.UtcNow.AddDays(7));
					var user = new AppUser
					{
						UserName = email,
						Email = email,
						FirstName = firstName,
						LastName = lastName,
						DateOfBirth = faker.Date.Between(new DateTime(1980, 1, 1), new DateTime(2005, 1, 1)),
						Status = UserStatus.New,
						CreatedDate = date,
						UpdatedDate = date.AddDays(faker.Random.Int(1, 7)),
						EmailConfirmed = true
					};

					var result = await userManager.CreateAsync(user, "Password123!");
					if (result.Succeeded)
					{

						string assignedRole;
						var fallbackRoles = new List<string> { "Base", "Plus", "Premium" };
						var availableRoles = roleNames
							 .Where(r => roleCaps.ContainsKey(r) && roleCounts[r] < roleCaps[r])
							 .ToList();

						if (availableRoles.Count == 0)
						{
							assignedRole = faker.PickRandom(fallbackRoles);
						}
						else
						{
							assignedRole = faker.PickRandom(availableRoles);
							roleCounts[assignedRole]++;
						}
						await userManager.AddToRoleAsync(user, assignedRole);

						// Add UserStatusHistory record
						context.UserHistory.Add(new AccountHistory
						{
							UserId = user.Id,
							Status = UserStatus.New,
							EventDate = date
						});
						// emulating activity
						var status = faker.Random.ListItem(new List<UserStatus>() { UserStatus.Hold, UserStatus.Active, UserStatus.Inactive, UserStatus.Suspended, UserStatus.Banned });
						context.UserHistory.Add(new AccountHistory
						{
							UserId = user.Id,
							Status = status,
							EventDate = date.AddDays(faker.Random.Int(1, 7))
						});

						// Update user status to a new random status and save
						user.Status = status;
						user.UpdatedDate = date.AddDays(faker.Random.Int(4, 10));
						await userManager.UpdateAsync(user);
					}
				}
				await context.SaveChangesAsync();
				var adminEmail = "admin@smartgym.com";
				var adminPassword = "Admin123!";

				var existingAdmin = await userManager.FindByEmailAsync(adminEmail);
				if (existingAdmin == null)
				{
					var adminUser = new AppUser
					{
						UserName = adminEmail,
						Email = adminEmail,
						FirstName = "System",
						LastName = "Admin",
						DateOfBirth = new DateTime(1990, 1, 1),
						Status = UserStatus.Active,
						CreatedDate = DateTime.UtcNow,
						UpdatedDate = DateTime.UtcNow,
						EmailConfirmed = true
					};

					var result = await userManager.CreateAsync(adminUser, adminPassword);
					if (result.Succeeded)
					{
						await userManager.AddToRoleAsync(adminUser, "Admin");
					}
				}

			}

			var trainerRoleName = Helpers.EnumHelper.GetDisplayName(RoleId.Trainer);
			var trainerRole = await roleManager.Roles.FirstOrDefaultAsync(r => r.Name == trainerRoleName);
			var trainerRoleId = trainerRole?.Id;
			var trainers = await (from user in context.Users
										 join userRole in context.UserRoles on user.Id equals userRole.UserId
										 join role in context.Roles on userRole.RoleId equals role.Id
										 where role.Name == trainerRoleName
										 select user).ToListAsync();

			//Clases
			List<string> classes = new() { "Amp It Up!", "Sweat N Sculpt", "Cardio Blast", "Power Surge", "Ignite Fitness", "Adrenaline Rush", "Torch N Tone", "Velocity HIIT", "Explosion Circuit", "Rhythm N Burn", "Grit N Grind", "Ironclad Core", "Sculpt N Define", "Strong Foundations", "Form N Fire", "Titan Training", "Muscle Mania", "Build N Burn", "Resilience Builders", "Powerhouse Physique", "Zen Zone Flow", "Harmony Stretch", "Inner Balance", "Calm N Core", "Mindful Movement", "Flexibility Fusion", "Serenity Sculpt", "Tranquil Strength", "Root N Rise", "Unwind N Restore", "Apex Athletics", "Synergy Session", "Kinetic Flow", "Metabolic Mayhem", "Bodyweight Blitz", "Urban Warrior", "Circuit Breaker", "The Grindhouse", "Fusion Fitness", "Primal Movement", "Warrior Waves", "Cardio Drumming", "Dance Dynamix", "Pilates Powerhouse", "Barre Burn", "Spin N Sculpt", "TRX Territory", "Kettlebell Kommandos", "Agility Ascent", "Gladiators Guild" };
			if (!context.Classes.Any())
			{

				var faker = new Faker<Class>()
					 .RuleFor(x => x.Name, f => f.Random.ListItem(classes))
					 .RuleFor(x => x.Schedule, f => f.Date.Between(default(DateTime), DateTime.Now.AddYears(9)))
					 .RuleFor(x => x.MaxCapacity, f => f.Random.Int(20, 50))
					 .RuleFor(x => x.TrainerId, f => f.PickRandom(trainers).Id)
					 .RuleFor(x => x.Description, f => f.Lorem.Sentence())
					 .RuleFor(x => x.Level, f => f.PickRandom<SkillLevel>());

				var fakeClasses = faker.Generate(20); // Generate 20 random classes
				context.Classes.AddRange(fakeClasses);
				await context.SaveChangesAsync();
			}

			//Orders
			if (!context.Orders.Any())
			{
				var users = await userManager.Users.ToListAsync();

				if (users.Any())
				{
					var orderFaker = new Faker<Order>()
					.RuleFor(o => o.User, f => f.PickRandom(users))
					.RuleFor(o => o.UserId, (f, o) => o.User.Id)
					.RuleFor(o => o.OrderTime, f => f.Date.Recent(90))
					.RuleFor(o => o.TotalPrice, f => f.Finance.Amount(10, 99))
					.RuleFor(o => o.CreatedAt, f => f.Date.Past(1))
					.RuleFor(o => o.UpdatedAt, (f, o) => o.CreatedAt.AddMinutes(f.Random.Int(10, 1000)))
					.RuleFor(o => o.Notes, f => f.Lorem.Sentence())
					.RuleFor(o => o.OrderCart, f => "[]");

					var fakeOrders = orderFaker.Generate(50);
					context.Orders.AddRange(fakeOrders);
					await context.SaveChangesAsync();
				}
			}
			List<int> userIds = await userManager.Users.Select(x => x.Id).ToListAsync();
			if (!context.Checkins.Any())
			{
				var faker = new Faker<Checkin>()
					 .RuleFor(x => x.CheckinTime, f => f.Date.Between(DateTime.UtcNow.AddMonths(-3), DateTime.UtcNow.AddDays(7)))
					 .RuleFor(x => x.Method, f => f.Random.ListItem(new List<string>() { "qr", "desk" }))
					 .RuleFor(x => x.UserId, f => f.Random.ListItem(userIds))
					 .RuleFor(x => x.AccessPoint, f => f.PickRandom<AccessPoint>());

				var fakeCheckins = faker.Generate(1000); // Generate 20 random classes
				context.Checkins.AddRange(fakeCheckins);
				await context.SaveChangesAsync();
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
					 .RuleFor(m => m.StockLevel, f => f.Random.Int(0, 999));

				var fakeMenuItems = faker.Generate(uniqueMenuItems.Count());
				for (int i = 0; i < uniqueMenuItems.Count(); i++)
				{
					fakeMenuItems[i].Name = uniqueMenuItems[i];
					var tags = GetTagsForMenuItem(uniqueMenuItems[i]);
					fakeMenuItems[i].Tags = tags.Any() ? string.Join(", ", tags) : "misc";
				}
				context.MenuItems.AddRange(fakeMenuItems);
				await context.SaveChangesAsync();
			}

			// Seed ClassSessions
			if (!context.ClassSessions.Any())
			{
				var classList = context.Classes.ToList();
				var classSessions = new List<ClassSession>();
				var sessionFaker = new Faker();

				foreach (var gymClass in classList)
				{
					// Each class gets 1-3 sessions
					int sessionCount = sessionFaker.Random.Int(1, 3);
					for (int i = 0; i < sessionCount; i++)
					{
						classSessions.Add(new ClassSession
						{
							ClassId = gymClass.Id,
							InstructorId = sessionFaker.PickRandom(trainers).Id,
							SessionDateTime = sessionFaker.Date.Between(DateTime.Now.AddDays(-30), DateTime.Now.AddDays(30)),
							HeadCount = sessionFaker.Random.Int(5, gymClass.MaxCapacity),
							MaxCapacity = gymClass.MaxCapacity,
							Description = sessionFaker.Lorem.Sentence(),
							LocationId = sessionFaker.PickRandom(new[] { AccessPoint.Pool, AccessPoint.RockClimbing, AccessPoint.Class })
						});
					}
				}
				context.ClassSessions.AddRange(classSessions);
				await context.SaveChangesAsync();
			}

			if (!context.Bookings.Any())
			{
				var users = await userManager.Users
					 .Where(u => u.Status == UserStatus.Active)
					 .ToListAsync();
				var classSessions = context.ClassSessions.ToList();
				var bookings = new List<Booking>();
				var waitlist = new List<Waitlist>();
				var random = new Random();

				foreach (var user in users)
				{
					// Pick a random session to sign-up for
					var session = classSessions[random.Next(classSessions.Count)];

					if (session.HeadCount >= session.MaxCapacity)
					{
						// Session is full, add to waitlist
						int nextPosition = waitlist.Count(w => w.SessionId == session.Id) + 1;
						var createdAt = DateTime.Now.AddDays(-random.Next(1, 30));
						waitlist.Add(new Waitlist
						{
							MemberId = user.Id,
							SessionId = session.Id,
							JoinedDateTime = createdAt.AddMinutes(random.Next(5, 120)),
							Position = nextPosition
						});
					}
					else
					{
						// Book the session
						var status = (BookingStatus)random.Next(0, Enum.GetValues(typeof(BookingStatus)).Length);
						var createdAt = DateTime.Now.AddDays(-random.Next(1, 30));
						var confirmedAt = (status == BookingStatus.Confirmed)
							 ? createdAt.AddMinutes(random.Next(5, 120))
							 : DateTime.MinValue;

						bookings.Add(new Booking
						{
							UserId = user.Id,
							ClassSessionId = session.Id,
							Status = status,
							CreatedAt = createdAt,
							ConfirmedAt = confirmedAt,
							UpdatedAt = createdAt.AddMinutes(random.Next(10, 500))
						});
						session.HeadCount++;
					}
				}

				context.Bookings.AddRange(bookings);
				context.Waitlist.AddRange(waitlist);
				context.ClassSessions.UpdateRange(classSessions);
				await context.SaveChangesAsync();
			}
		}

		private static List<string> GetTagsForMenuItem(string name)
		{
			var tags = new List<string>();
			var n = name.ToLower();

			// Drinks
			if (n.Contains("coffee") || n.Contains("latte") || n.Contains("espresso") || n.Contains("mocha") || n.Contains("macchiato") || n.Contains("cappuccino") || n.Contains("drip")) tags.Add("coffee");
			if (n.Contains("tea") || n.Contains("chai") || n.Contains("matcha") || n.Contains("earl grey")) tags.Add("tea");
			if (n.Contains("smoothie")) tags.Add("smoothie");
			if (n.Contains("protein shake")) tags.Add("protein shake");
			if (n.Contains("lemonade")) tags.Add("lemonade");
			if (n.Contains("kombucha")) tags.Add("kombucha");
			if (n.Contains("water")) tags.Add("water");
			if (n.Contains("milk")) tags.Add("milk");
			if (n.Contains("hot chocolate") || n.Contains("iced chocolate")) tags.Add("chocolate");
			if (n.Contains("pre-workout")) tags.Add("pre-workout");
			if (n.Contains("post-workout")) tags.Add("post-workout");
			if (n.Contains("collagen")) tags.Add("collagen");
			if (n.Contains("bcaa")) tags.Add("bcaa");
			if (n.Contains("creatine")) tags.Add("creatine");
			if (n.Contains("electrolyte")) tags.Add("electrolyte");

			// Breakfast
			if (n.Contains("oatmeal") || n.Contains("overnight oats")) tags.Add("oats");
			if (n.Contains("greek yogurt") || n.Contains("parfait")) tags.Add("yogurt");
			if (n.Contains("acai bowl") || n.Contains("pitaya bowl") || n.Contains("smoothie bowl")) tags.Add("bowl");
			if (n.Contains("chia seed")) tags.Add("chia");
			if (n.Contains("avocado toast")) tags.Add("toast");
			if (n.Contains("egg")) tags.Add("egg");
			if (n.Contains("pancake")) tags.Add("pancake");
			if (n.Contains("waffle")) tags.Add("waffle");

			// Lunch/Deli
			if (n.Contains("wrap")) tags.Add("wrap");
			if (n.Contains("sandwich")) tags.Add("sandwich");
			if (n.Contains("burger")) tags.Add("burger");
			if (n.Contains("salad")) tags.Add("salad");
			if (n.Contains("soup")) tags.Add("soup");
			if (n.Contains("fries")) tags.Add("fries");
			if (n.Contains("filet") || n.Contains("grilled chicken")) tags.Add("entree");

			// Snacks
			if (n.Contains("bar")) tags.Add("bar");
			if (n.Contains("cookie")) tags.Add("cookie");
			if (n.Contains("bites")) tags.Add("bites");
			if (n.Contains("trail mix")) tags.Add("trail mix");
			if (n.Contains("jerky")) tags.Add("jerky");
			if (n.Contains("rice cakes")) tags.Add("rice cakes");
			if (n.Contains("dark chocolate")) tags.Add("chocolate");
			if (n.Contains("gummies")) tags.Add("gummies");
			if (n.Contains("fruit cup") || n.Contains("apple") || n.Contains("banana") || n.Contains("orange")) tags.Add("fruit");
			if (n.Contains("almonds") || n.Contains("walnuts") || n.Contains("cashews") || n.Contains("nuts")) tags.Add("nuts");
			if (n.Contains("seeds")) tags.Add("seeds");
			if (n.Contains("granola")) tags.Add("granola");

			// Dietary
			if (n.Contains("veggie") || n.Contains("vegetarian") || n.Contains("plant")) tags.Add("vegetarian");
			if (n.Contains("vegan")) tags.Add("vegan");
			if (n.Contains("gluten free") || n.Contains("gluten-free")) tags.Add("gluten-free");
			if (n.Contains("protein")) tags.Add("high protein");
			if (n.Contains("sugar-free")) tags.Add("sugar-free");
			if (n.Contains("low carb")) tags.Add("low carb");

			// Add-ons/extras
			if (n.Contains("extra shot")) tags.Add("add-on");

			// Misc
			if (n.Contains("honey") || n.Contains("maple syrup") || n.Contains("agave") || n.Contains("stevia") || n.Contains("monk fruit") || n.Contains("sugar")) tags.Add("sweetener");
			if (n.Contains("pepper") || n.Contains("salt")) tags.Add("seasoning");

			return tags.Distinct().ToList();
		}

		private static void UpdateImageFolder(SmartGymContext context)
		{
			var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lib", "db_images");
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