using Microsoft.EntityFrameworkCore;
using SmartGym.Models;

namespace SmartGym.Data;

public class ImagesRepo : Repository<Images>
{
	private readonly DbSet<Images> _dbSet;

	private readonly SmartGymContext _context;

	public ImagesRepo(SmartGymContext context) : base(context)
	{
		_context = context;
		_dbSet = context.Set<Images>();
	}

	#region Custom Methods

	#endregion
}
