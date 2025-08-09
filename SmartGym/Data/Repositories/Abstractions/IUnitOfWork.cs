using SmartGym.Models;

namespace SmartGym.Data;

public interface IUnitOfWork
{
	public IRepository<Class> ClassRepository { get; }
	public IRepository<AppUser> UserRepository { get; }
	public IRepository<Order> OrderRepository { get; }
	public IRepository<Checkin> CheckinRepository { get; }
	public IRepository<MenuItem> MenuItemRepository { get; }
	public IRepository<Images> ImagesRepository { get; }
	public IRepository<Booking> BookingsRepository { get; }
	public IRepository<Waitlist> WaitlistRepository { get; }
	public IRepository<ClassSession> ClassSessionRepository { get; }
	public Task SaveAsync();
}
