using Microsoft.EntityFrameworkCore;
using SmartGym.Constants.Enums;
using SmartGym.Models;

namespace SmartGym.Data;

public class UserRepo : Repository<AppUser>
{
	private readonly SmartGymContext _context;
	private readonly DbSet<Class> _dbSet;

	public UserRepo(SmartGymContext context) : base(context)
	{
		_context = context;
		_dbSet = context.Set<Class>();
	}

	#region Custom Methods

	/// <summary>
	/// Asynchronously retrieves all users from the database along with their associated roles,
	/// and maps them to a list of <see cref="UserDto"/> objects.
	/// </summary>
	/// <returns>
	/// A task that represents the asynchronous operation. The task result contains a list of <see cref="UserDto"/> 
	/// objects representing all users and their roles.
	/// </returns>
	public async Task<List<UserDto>> GetAllAspUsersAsDto()
	{
		var usersWithRoles = await(
				from user in _context.Users
				join userRole in _context.UserRoles on user.Id equals userRole.UserId into usr
				from userRole in usr.DefaultIfEmpty()
				join role in _context.Roles on userRole.RoleId equals role.Id into r
				from role in r.DefaultIfEmpty()
				select new UserDto
				{
					Id = user.Id,
					FirstName = user.FirstName,
					LastName = user.LastName,
					Name = user.Name,
					Email = user.Email,
					DateOfBirth = user.DateOfBirth,
					Status = user.Status,
					CreatedDate = user.CreatedDate,
					UpdatedDate = user.UpdatedDate,
					ImageRef = user.ImageRef,
					RoleId = (RoleId)role.Id
				}
		  ).ToListAsync();

		return usersWithRoles;
	}

	#endregion
}
