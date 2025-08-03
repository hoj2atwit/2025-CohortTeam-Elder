using SmartGym.Models;

namespace SmartGym.Data;

public interface IUnitOfWork
{
	public IRepository<Class> ClassRepository { get; }
	public IRepository<User> UserRepository { get; }
	public IRepository<Checkin> CheckinRepository { get; }
	public Task SaveAsync();
}
