using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace SmartGym.Data;

public class Repository<T> : IRepository<T> where T : class
{
	private readonly SmartGymContext _context;
	private readonly DbSet<T> _dbSet;
	public Repository(SmartGymContext context)
	{
		_context = context;
		_dbSet = context.Set<T>();
	}

	public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? filter = null, string includeProperties = "")
	{
		IQueryable<T> query = _dbSet;

		if (filter is not null)
		{
			query = query.Where(filter);
		}

		foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
		{
			query = query.Include(includeProperty);
		}

		return await query.ToListAsync();
	}

	public async Task<T?> GetAsync(object id)
	{
		return await _dbSet.FindAsync(id);
	}

	public async Task AddAsync(T entity)
	{
		await _dbSet.AddAsync(entity);
	}

	public void Update(T entity)
	{
		_dbSet.Update(entity);
	}

	public void Delete(T entity)
	{
		_dbSet.Remove(entity);
	}
}
