using SmartGym.Components;
using Microsoft.Extensions.Configuration;
using SmartGym.Services;
using Microsoft.AspNetCore.Mvc;
using SmartGym.Data;
using Microsoft.EntityFrameworkCore;
using SmartGym.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
		.AddInteractiveServerComponents();
builder.Services.AddAntiforgery();
builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Database
builder.Services.Configure<DatabaseConfiguration>(builder.Configuration.GetSection(DatabaseConfiguration.ConnectionStrings));
builder.Services.AddDbContext<SmartGymContext>(options =>
{
	var connectionString = builder.Configuration.GetConnectionString("DBConnectionString");
	options.UseSqlServer(connectionString);
});
//Identity
builder.Services.AddIdentityCore<AppUser>(options =>
		{
			options.User.RequireUniqueEmail = true;
			options.SignIn.RequireConfirmedAccount = false;
			options.Password.RequireDigit = true;
			options.Password.RequireUppercase = true;
			options.Password.RequireNonAlphanumeric = false;
			options.Password.RequiredLength = 8;
		})
		.AddRoles<IdentityRole<int>>()
		.AddEntityFrameworkStores<SmartGymContext>()
		.AddSignInManager()
		.AddDefaultTokenProviders();
builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
.AddIdentityCookies();
builder.Services.AddAuthorization();
//Automapper
builder.Services.AddAutoMapper(typeof(MappingProfile));
//Data Repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//Services
builder.Services.AddScoped<IClassService, ClassService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<ICafeService, CafeService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
		.AddInteractiveServerRenderMode();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	// await DbSeed.SeedDatabaseAsync(services, app.Environment.IsDevelopment());
}

app.Run();
