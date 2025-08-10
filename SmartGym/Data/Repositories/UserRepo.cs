using Microsoft.EntityFrameworkCore;
using SmartGym.Constants.Enums;
using SmartGym.Helpers;
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
		var usersWithRoles = await (
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
					RoleId = EnumHelper.GetRoleIdFromName(role.Name).Value
				}
		  ).ToListAsync();

		return usersWithRoles;
	}

	public async Task<List<AccountHistoryDTO>> GetAccHistByRoleId(RoleId roleId)
	{
		var normalizedName = EnumHelper.GetDisplayName(roleId).ToUpper();
		var historyByRoles = await (
				from history in _context.UserHistory
				join user in _context.Users on history.UserId equals user.Id
				join userRole in _context.UserRoles on user.Id equals userRole.UserId into usr
				from userRole in usr.DefaultIfEmpty()
				join role in _context.Roles on userRole.RoleId equals role.Id into r
				from role in r.DefaultIfEmpty()
				where role.NormalizedName == normalizedName
				select new AccountHistoryDTO
				{
					Id = history.Id,
					UserId = history.UserId,
					Status = history.Status,
					EventDate = history.EventDate
				}
			).ToListAsync();

		return historyByRoles;
	}

	#endregion
}
