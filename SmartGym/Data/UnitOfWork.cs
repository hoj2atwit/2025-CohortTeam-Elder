using SmartGym.Models;

namespace SmartGym.Data;

public class UnitOfWork : IUnitOfWork
{
	private readonly SmartGymContext _context;
	private IRepository<Class>? _classRepository;
	private IRepository<User>? _userRepository;
	private IRepository<Order>? _orderRepository;
	private IRepository<Checkin>? _checkinRepository;
	private IRepository<MenuItem>? _menuItemRepository;

	public UnitOfWork(SmartGymContext context)
	{
		_context = context;
	}
	public IRepository<Class> ClassRepository
	{
		get
		{
			_classRepository ??= new Repository<Class>(_context);
			return _classRepository;
		}
	}
	public IRepository<User> UserRepository
	{
		get
		{
			_userRepository ??= new Repository<User>(_context);
			return _userRepository;
		}
	}
	public IRepository<Order> OrderRepository
	{
		get
		{
			_orderRepository ??= new Repository<Order>(_context);
			return _orderRepository;
		}
	}

	public IRepository<MenuItem> MenuItemRepository
	{
		get
		{
			_menuItemRepository ??= new Repository<MenuItem>(_context);
			return _menuItemRepository;
		}
	}
	public IRepository<Checkin> CheckinRepository
	{
		get
		{
			_checkinRepository ??= new Repository<Checkin>(_context);
			return _checkinRepository;
		}
	}
	public async Task SaveAsync()
	{
		await _context.SaveChangesAsync();
	}
}
