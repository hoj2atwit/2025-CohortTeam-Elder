using System;
using Microsoft.EntityFrameworkCore;
using SmartGym.Models;

namespace SmartGym.Data.Repositories;

public class MenuItemsRepo : Repository<Class>
{
	private readonly DbSet<MenuItem> _dbSet;
	private readonly SmartGymContext _context;

	public MenuItemsRepo(SmartGymContext context) : base(context)
	{
		_context = context;
		_dbSet = context.Set<MenuItem>();
	}

	#region Custom Methods

	#endregion
}
