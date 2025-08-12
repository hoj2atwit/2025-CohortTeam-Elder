using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SmartGym.Components;
using SmartGym.Data;
using SmartGym.Models;
using SmartGym.Services;

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
builder.Services.AddIdentity<AppUser, IdentityRole<int>>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
})
.AddEntityFrameworkStores<SmartGymContext>()
.AddDefaultTokenProviders();
builder.Services.AddAuthorization(options =>
options.FallbackPolicy = new AuthorizationPolicyBuilder()
				.RequireAuthenticatedUser()
				.Build());
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "SmartGymAuth";
    options.LoginPath = "/";
    options.AccessDeniedPath = "/forbidden";
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
    options.SlidingExpiration = true;
});
//Automapper
builder.Services.AddAutoMapper(typeof(MappingProfile));
//Data Repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//Services

builder.Services.AddScoped<IClassService, ClassService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<ICafeService, CafeService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, CustomClaimsPrincipalFactory>();
builder.Services.AddHttpContextAccessor();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

//app.UseStatusCodePages(async context =>
//{
//    if (context.HttpContext.Response.StatusCode == 401)
//    {
//        context.HttpContext.Response.ContentType = "application/json";
//        await context.HttpContext.Response.WriteAsync("{\"error\":\"Unauthorized\"}");
//    }
//});
app.UseStatusCodePagesWithRedirects("/forbidden");


app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets().AllowAnonymous();
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
	await DbSeed.SeedDatabaseAsync(services, app.Environment.IsDevelopment());
}

app.Run();
