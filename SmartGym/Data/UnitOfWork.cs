using SmartGym.Models;

namespace SmartGym.Data;

public class UnitOfWork : IUnitOfWork
{
	private readonly SmartGymContext _context;
	private IRepository<Class>? _classRepository;
	private IRepository<AppUser>? _userRepository;
	private IRepository<Order>? _orderRepository;
	private IRepository<Checkin>? _checkinRepository;
	private IRepository<MenuItem>? _menuItemRepository;
	private IRepository<Images>? _imagesRepository;
	private IRepository<Booking>? _bookingsRepository;
	private IRepository<Waitlist>? _waitlistRepository;
	private IRepository<ClassSession>? _classSessionRepository;

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
	public IRepository<AppUser> UserRepository
	{
		get
		{
			_userRepository ??= new Repository<AppUser>(_context);
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
	public IRepository<Images> ImagesRepository
	{
		get
		{
			_imagesRepository ??= new Repository<Images>(_context);
			return _imagesRepository;
		}
	}
	public IRepository<Booking> BookingsRepository
	{
		get
		{
			_bookingsRepository ??= new Repository<Booking>(_context);
			return _bookingsRepository;
		}
	}

	public IRepository<Waitlist> WaitlistRepository
	{
		get
		{
			_waitlistRepository ??= new Repository<Waitlist>(_context);
			return _waitlistRepository;
		}
	}
	public IRepository<ClassSession> ClassSessionRepository
	{
		get
		{
			_classSessionRepository ??= new Repository<ClassSession>(_context);
			return _classSessionRepository;
		}
	}
	public async Task SaveAsync()
	{
		await _context.SaveChangesAsync();
	}
}
