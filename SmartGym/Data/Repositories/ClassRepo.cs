using System;
using Microsoft.EntityFrameworkCore;
using SmartGym.Models;

namespace SmartGym.Data;

public class ClassRepo : Repository<Class>
{
	private readonly DbSet<Class> _dbSet;
	private readonly SmartGymContext _context;

	public ClassRepo(SmartGymContext context) : base(context)
	{
		_context = context;
		_dbSet = context.Set<Class>();
	}

	#region Custom Methods

	#endregion
}
