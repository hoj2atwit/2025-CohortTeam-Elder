using SmartGym.Models;

namespace SmartGym.Data;

public interface IUnitOfWork
{
	public IRepository<Class> ClassRepository { get; }
	public IRepository<User> UserRepository { get; }
	public IRepository<Order> OrderRepository { get; }
	public IRepository<Checkin> CheckinRepository { get; }
	public IRepository<MenuItem> MenuItemRepository { get; }
	public IRepository<Images> ImagesRepository { get; }
	public Task SaveAsync();
}
