using Microsoft.EntityFrameworkCore;
using SmartGym.Models;

namespace SmartGym.Data;

public class OrderRepository : Repository<Order>
{
	private readonly DbSet<Order> _dbSet;
	private readonly SmartGymContext _context;

	public OrderRepository(SmartGymContext context) : base(context)
	{
		_context = context;
		_dbSet = context.Set<Order>();
	}


	#region Custom Methods

	#endregion

}
