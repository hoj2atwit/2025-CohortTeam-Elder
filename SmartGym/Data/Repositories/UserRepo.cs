using Microsoft.EntityFrameworkCore;
using SmartGym.Models;

namespace SmartGym.Data;

public class UserRepo : Repository<User>
{
	private readonly SmartGymContext _context;
	private readonly DbSet<Class> _dbSet;

	public UserRepo(SmartGymContext context) : base(context)
	{
		_context = context;
		_dbSet = context.Set<Class>();
	}
	
	#region Custom Methods

	#endregion
}
