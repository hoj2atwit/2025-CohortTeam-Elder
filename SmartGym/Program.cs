using SmartGym.Components;
using Microsoft.Extensions.Configuration;
using SmartGym.Services;
using Microsoft.AspNetCore.Mvc;
using SmartGym.Data;
using SmartGym.Models;
using Microsoft.EntityFrameworkCore;
using SmartGym.Services.UserService;
using Microsoft.EntityFrameworkCore.Design;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
		.AddInteractiveServerComponents();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Database
builder.Services.Configure<DatabaseConfiguration>(builder.Configuration.GetSection(DatabaseConfiguration.ConnectionStrings));
builder.Services.AddDbContext<SmartGymContext>(options =>
{
	var connectionString = builder.Configuration.GetConnectionString("DBConnectionString");
	options.UseSqlServer(connectionString);
});
//Data Repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//Services
builder.Services.AddScoped<IClassService, ClassService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<ICafeService, CafeService>();
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

app.Run();
