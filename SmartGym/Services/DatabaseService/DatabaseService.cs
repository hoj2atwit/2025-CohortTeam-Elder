using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using SmartGym.Data;
using SmartGym.Models;

namespace SmartGym.Services;

public class DatabaseService : IDatabaseService
{
	private readonly string _connectionString;
	private readonly SmartGymContext _context;

	public DatabaseService(SmartGymContext context)
	{
		_context = context;
	}

	#region User

	public void CreateUser(User user)
	{
		//TODO: Pass in user obj and create new user that way.
		// var newUser = new User
		// {
		// 	Name = "test",
		// 	Email = "test",
		// 	Role = "test"
		// };
		// _context.Users.Add(newUser);
		// _context.SaveChanges();
	}
	public void UpdateUser(User user)
	{

	}
	public void GetUser(User user)
	{

	}
	public void GetUserCheckInHistory(User user)
	{

	}
	public void GetAllUsers()
	{

	}
	public void GetUserPaymentMethod(User user)
	{

	}
	public void GetTrafficData(DateTime date)
	{

	}
	public void GetTrafficData(DateTime startDate, DateTime endDate)
	{

	}

	#endregion

	#region Cafe

	public void GetOrderHistory(User user)
	{

	}
	public void GetFullMenu()
	{

	}
	public void GetMealPrepItems()
	{

	}
	public void GetCafeItemModifications()
	{

	}
	public void GetCurrentPromos()
	{

	}

	#endregion

	#region Classes

	public async Task<bool> CreateClass(ClassDTO newClassData)
	{
		var newClass = new Class
		{
			Name = newClassData.Name,
			Schedule = newClassData.Schedule,
			Capacity = newClassData.Capacity,
			TrainerId = newClassData.TrainerId,
			CategoryId = newClassData.CategoryId
		};
		_context.Classes.Add(newClass);
		try
		{
			await _context.SaveChangesAsync();
		}
		catch (System.Exception ex)
		{
			throw;
		}

		return true;
	}
	
	#endregion

}