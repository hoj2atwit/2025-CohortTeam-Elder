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
		// _connectionString = options.Value.context;
		_context = context;
	}
#region 
	
	
#endregion
	public void TestInsert()
	{
		// using (IDbConnection db = new SqlConnection(_connectionString))
		// {
		// 	db.Execute("INSERT INTO users (name, email, role) VALUES (@Name, @Email, @Role)", new { Name = "test", Email = "test", Role = "test" });
		// }
		var newUser = new User
		{
			Name = "test",
			Email = "test",
			Role = "test"
		};
		_context.Users.Add(newUser);
		_context.SaveChanges();

	}

}