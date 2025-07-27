using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Dapper;
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
	#region 


	#endregion
	public void CreateNewUser()
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

}