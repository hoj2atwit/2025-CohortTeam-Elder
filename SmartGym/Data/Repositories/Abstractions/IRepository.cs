using System.Linq.Expressions;
using SmartGym.Models;

namespace SmartGym.Data;

public interface IRepository<T>
{
	public Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? filter = null, string includeProperties = "");
	public Task<T?> GetAsync(object id);
	public Task AddAsync(T entity);
	public void Update(T entity);
	public void Delete(T entity);
}