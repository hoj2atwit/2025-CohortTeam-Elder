using Microsoft.EntityFrameworkCore;
using SmartGym.Models;

namespace SmartGym.Data;

public class OrderRepository : Repository<Order>, IOrderRepository
{
	private readonly DbSet<Order> _dbSet;
	private readonly SmartGymContext _context;

	public OrderRepository(SmartGymContext context) : base(context)
	{
		_context = context;
		_dbSet = context.Set<Order>();
	}


	#region Custom Methods
	public async Task<List<Order>> GetAllOrdersByUserIdAsync(int userId)
	{
		return await _dbSet.Where(o => o.UserId == userId).ToListAsync();
	}

	#endregion

}
