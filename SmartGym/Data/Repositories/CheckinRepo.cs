using Microsoft.EntityFrameworkCore;
using SmartGym.Models;

namespace SmartGym.Data;

public class CheckinRepo : Repository<Checkin>
{
	private readonly SmartGymContext _context;
	private readonly DbSet<Checkin> _dbSet;

	public CheckinRepo(SmartGymContext context) : base(context)
	{
		_context = context;
		_dbSet = context.Set<Checkin>();
	}
	
	#region Custom Methods

	#endregion
}
