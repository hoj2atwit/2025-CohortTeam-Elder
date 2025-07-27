using SmartGym.Components;
using SmartGym.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using SmartGym.Services;
using Microsoft.AspNetCore.Mvc;
using SmartGym.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<DatabaseConfiguration>(builder.Configuration.GetSection(DatabaseConfiguration.ConnectionStrings));
builder.Services.AddTransient<IDatabaseService, DatabaseService>();
builder.Services.AddDbContext<SmartGymContext>(options =>
{ 
	var connectionString = builder.Configuration.GetConnectionString("DBConnectionString");
	options.UseSqlServer(connectionString);
});

var app = builder.Build();

#region Remove later, for testing only
try
{
	using (var scope = app.Services.CreateScope())
	{
		var db = scope.ServiceProvider.GetRequiredService<IDatabaseService>();
		// var db.CreateNewUser();
	}
}
catch (System.Exception ex)
{
	throw;
}
#endregion

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
