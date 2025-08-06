using Microsoft.EntityFrameworkCore;
using SmartGym.Models;

namespace SmartGym.Data;

public class BookingRepo : Repository<Booking>
{
	private readonly SmartGymContext _context;
	private readonly DbSet<Booking> _dbSet;

	public BookingRepo(SmartGymContext context) : base(context)
	{
		_context = context;
		_dbSet = context.Set<Booking>();
	}

	#region Custom Methods

	#endregion

}
