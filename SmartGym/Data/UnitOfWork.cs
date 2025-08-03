using SmartGym.Models;

namespace SmartGym.Data;

public class UnitOfWork : IUnitOfWork
{
	private readonly SmartGymContext _context;
	private IRepository<Class>? _classRepository;
	private IRepository<User>? _userRepository;
	private IRepository<Checkin>? _checkinRepository;

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
