using SmartGym.Models;

namespace SmartGym.Data;

public class UnitOfWork : IUnitOfWork
{
	private readonly SmartGymContext _context;
	private IRepository<Class>? _classRepository;
	private IRepository<User>? _userRepository;
<<<<<<< HEAD
	private IRepository<Checkin>? _checkinRepository;
=======
	private IRepository<MenuItem>? _menuItemRepository;
>>>>>>> 9351987 (Started work on implementing Cafe menu)

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
<<<<<<< HEAD
	public IRepository<Checkin> CheckinRepository
	{
		get
		{
			_checkinRepository ??= new Repository<Checkin>(_context);
			return _checkinRepository;
=======

	public IRepository<MenuItem> MenuItemRepository
	{
		get
		{
			_menuItemRepository ??= new Repository<MenuItem>(_context);
			return _menuItemRepository;
>>>>>>> 9351987 (Started work on implementing Cafe menu)
		}
	}
	public async Task SaveAsync()
	{
		await _context.SaveChangesAsync();
	}
}
