using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SmartGym.Constants;
using SmartGym.Constants.Enums;
using SmartGym.Data;
using SmartGym.Helpers;
using SmartGym.Models;
using System.Security.Claims;

namespace SmartGym.Services;

public class UserService : IUserService
{

	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	private readonly UserManager<AppUser> _userManager;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
		_userManager = userManager;
    }

    public async Task<UserDto> CreateUser(UserDto newUserData)
	{
		try
		{
			AppUser newUser = _mapper.Map<AppUser>(newUserData);
			await _unitOfWork.UserRepository.AddAsync(newUser);
			await _unitOfWork.SaveAsync();
			if (newUserData.RoleId != null)
			{
				throw new Exception("The required property \"RoleId\" is null.");
			}
			else
			{
				var roleName = EnumHelper.GetDisplayName(newUserData.RoleId);
				await _userManager.AddToRoleAsync(newUser, roleName); // doesnt need SaveAsync
			}
			return _mapper.Map<UserDto>(newUser);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in CreateUser: {ex.Message}");
			throw;
		}
	}
	/// <summary>
	/// returns a list of users
	/// </summary>
	/// <returns></returns>
	public async Task<List<UserDto>> GetAllUsers()
	{
		try
		{
			return await _unitOfWork.UserRepository.GetAllAspUsersAsDto();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in GetAllUsers: {ex.Message}");
			return new List<UserDto>();
		}
	}

	public void GetTrafficData(DateTime date)
	{
		throw new NotImplementedException();
	}

	public void GetTrafficData(DateTime startDate, DateTime endDate)
	{
		throw new NotImplementedException();
	}
	/// <summary>
	/// returns a single user by id
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	public async Task<UserDto> GetUserById(int id)
	{
		try
		{
			var userEntity = await _unitOfWork.UserRepository.GetAsync(id);
			return _mapper.Map<UserDto>(userEntity);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in GetUserById: {ex.Message}");
			return null;
		}
	}

	public async Task<bool> CheckInUser(UserDto user, AccessPoint accessPoint, CheckinMethod checkinMethod)
	{
		try
		{
			var dto = new CheckinDTO()
			{
				UserId = user.Id,
				AccessPoint = accessPoint,
				Method = EnumHelper.GetDisplayName(checkinMethod),
				User = user,
				CheckinTime = DateTime.UtcNow
			};
			Checkin entity = _mapper.Map<Checkin>(dto);
			await _unitOfWork.CheckinRepository.AddAsync(entity);
			await _unitOfWork.SaveAsync();
			return true;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error while checking in user: {ex.Message}");
			return false;
		}
	}

	/// <summary>
	/// gets all checkins for the given user id
	/// </summary>
	/// <param name="id"></param>
	/// <param name="includeUser"></param>
	/// <returns></returns>
	public async Task<List<CheckinDTO>> GetUserCheckins(int id, bool includeUser = false)
	{
		try
		{
			var includeProps = includeUser ? "User" : "";
			var checkinEntity = await _unitOfWork.CheckinRepository.GetAsync(
				x => x.UserId == id, includeProperties: includeProps);
			return _mapper.Map<List<CheckinDTO>>(checkinEntity).ToList();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in GetUserCheckins: {ex.Message}");
			return null;
		}
	}

	/// <summary>
	/// returns all user checkin history
	/// </summary>
	/// <param name="includeUser"></param>
	/// <returns></returns>
	public async Task<List<CheckinDTO>> GetAllUserCheckins(bool includeUser = false)
	{
		try
		{
			var includeProps = includeUser ? "User" : "";
			var checkinEntity = await _unitOfWork.CheckinRepository.GetAsync(includeProperties: includeProps);
			return _mapper.Map<List<CheckinDTO>>(checkinEntity);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in GetAllUserCheckins: {ex.Message}");
			return null;
		}
	}

	/// <summary>
	/// Gets user checkins filtered by access point.
	/// </summary>
	/// <param name="accessPoint"></param>
	/// <param name="includeUser"></param>
	/// <returns></returns>
	public async Task<List<CheckinDTO>> GetCheckinsByAccessPoint(AccessPoint accessPoint, bool includeUser = false)
	{
		try
		{
			var includeProps = includeUser ? "User" : "";
			var checkins = await _unitOfWork.CheckinRepository.GetAsync(
				x => x.AccessPoint == accessPoint, includeProperties: includeProps);
			return _mapper.Map<List<CheckinDTO>>(checkins);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in GetCheckinsByAccessPoint: {ex.Message}");
			return new List<CheckinDTO>();
		}
	}

	/// <summary>
	/// Gets user checkins filtered by time range.
	/// </summary>
	/// <param name="startTime"></param>
	/// <param name="endTime"></param>
	/// <param name="includeUser"></param>
	/// <returns></returns>
	public async Task<List<CheckinDTO>> GetCheckinsByTime(DateTime startTime, DateTime endTime, bool includeUser = false)
	{
		try
		{
			var includeProps = includeUser ? "User" : "";
			var checkins = await _unitOfWork.CheckinRepository.GetAsync(
				x => x.CheckinTime >= startTime && x.CheckinTime <= endTime, includeProperties: includeProps);
			return _mapper.Map<List<CheckinDTO>>(checkins);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in GetCheckinsByTime: {ex.Message}");
			return new List<CheckinDTO>();
		}
	}

	/// <summary>
	/// Gets user checkins filtered by checkin method.
	/// </summary>
	/// <param name="method"></param>
	/// <param name="includeUser"></param>
	/// <returns></returns>
	public async Task<List<CheckinDTO>> GetCheckinsByMethod(CheckinMethod method, bool includeUser = false)
	{
		try
		{
			var includeProps = includeUser ? "User" : "";
			var methodString = EnumHelper.GetDisplayName(method);
			var checkins = await _unitOfWork.CheckinRepository.GetAsync(
				x => x.Method == methodString, includeProperties: includeProps);
			return _mapper.Map<List<CheckinDTO>>(checkins);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in GetCheckinsByMethod: {ex.Message}");
			return new List<CheckinDTO>();
		}
	}


	public void GetUserPaymentMethod(AppUser user)
	{
		throw new NotImplementedException();
	}

	/// <summary>
	/// updates the user in the database
	/// </summary>
	/// <param name="id"></param>
	/// <param name="userDto"></param>
	/// <returns></returns>
	public async Task<UserDto?> UpdateUser(int id, UserDto userDto)
	{
		try
		{
			var userEntity = await _unitOfWork.UserRepository.GetAsync(id);
			if (userEntity == null)
				return null;

			_mapper.Map(userDto, userEntity);

			_unitOfWork.UserRepository.Update(userEntity);
			await _unitOfWork.SaveAsync();
			return _mapper.Map<UserDto>(userEntity);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error while updating user: {ex.Message}");
			return null;
		}
	}
	/// <summary>
	/// Updates the user image in the database and stores the image bytes.
	/// </summary>
	/// <param name="id">user item ID</param>
	/// <param name="imageRef">New image URL or file name</param>
	/// <param name="imageBytes">Image file bytes</param>
	/// <param name="guid">Unique identifier for the image</param>
	/// <returns>True if update was successful, false otherwise</returns>
	public async Task<(bool Success, string Message)> UploadUserImageBlob(int id, string imageRef, byte[] imageBytes, string guid)
	{
		try
		{
			var user = await _unitOfWork.UserRepository.GetAsync(id);
			if (user == null)
				return (false, "user item not found");

			var existingImage = (await _unitOfWork.ImagesRepository
				.GetAsync(x => x.ImageRef != null && x.ImageRef.Contains(guid)))
				.FirstOrDefault();

			if (existingImage != null)
			{
				existingImage.ImageRef = imageRef;
				existingImage.Data = imageBytes;
				existingImage.UpdatedUtcDate = DateTime.UtcNow;
				_unitOfWork.ImagesRepository.Update(existingImage);
				user.ImageRef = existingImage.ImageRef;
			}
			else
			{
				var newImage = new Images
				{
					ImageRef = imageRef,
					Data = imageBytes,
					IsUserImage = false,
					UpdatedUtcDate = DateTime.UtcNow
				};
				await _unitOfWork.ImagesRepository.AddAsync(newImage);
				user.ImageRef = newImage.ImageRef;
			}

			_unitOfWork.UserRepository.Update(user);
			await _unitOfWork.SaveAsync();

			return (true, "User image uploaded and reference updated successfully");
		}
		catch (Exception ex)
		{
			return (false, $"Error while uploading user image: {ex.Message}");
		}
	}
	/// <summary>
	/// removes a user in the database
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	public async Task<bool> DeleteUser(int id)
	{
		try
		{
			var userEntity = await _unitOfWork.UserRepository.GetAsync(id);
			if (userEntity == null)
				return false;

			_unitOfWork.UserRepository.Delete(userEntity);
			await _unitOfWork.SaveAsync();
			return true;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error while deleting user: {ex.Message}");
			return false;
		}
	}

	public async Task<List<AccountHistoryDTO>> GetAccHistory(bool includeUser = false)
	{
		try
		{
			var includeProps = includeUser ? "User" : "";
			var historyEntities = await _unitOfWork.UserHistoryRepository.GetAsync(includeProperties: includeProps);
			return _mapper.Map<List<AccountHistoryDTO>>(historyEntities);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in GetAccHistory: {ex.Message}");
			return new List<AccountHistoryDTO>();
		}
	}

	public async Task<List<AccountHistoryDTO>> GetAccHistoryByStatus(UserStatus userStatus, bool includeUser = false)
	{
		try
		{
			var includeProps = includeUser ? "User" : "";
			var historyEntities = await _unitOfWork.UserHistoryRepository.GetAsync(
				x => x.Status == userStatus, includeProperties: includeProps);
			return _mapper.Map<List<AccountHistoryDTO>>(historyEntities);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in GetAccHistoryByStatus: {ex.Message}");
			return new List<AccountHistoryDTO>();
		}
	}
	/// <summary>
	/// Gets all account history filtered by role. includeUser is NOT an option with this
	/// </summary>
	/// <param name="roleId"></param>
	/// <param name="includeUser"></param>
	/// <returns></returns>
	public async Task<List<AccountHistoryDTO>> GetAccHistoryByRole(RoleId roleId)
	{
		try
		{
			var historyEntities = await _unitOfWork.UserRepository.GetAccHistByRoleId(roleId);
			return _mapper.Map<List<AccountHistoryDTO>>(historyEntities);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in GetAccHistoryByStatus: {ex.Message}");
			return new List<AccountHistoryDTO>();
		}
	}

	public async Task<List<AccountHistoryDTO>> GetAccHistoryByDates(DateTime startTime, DateTime endTime, bool includeUser = false)
	{
		try
		{
			var includeProps = includeUser ? "User" : "";
			var historyEntities = await _unitOfWork.UserHistoryRepository.GetAsync(
				x => x.EventDate >= startTime && x.EventDate <= endTime, includeProperties: includeProps);
			return _mapper.Map<List<AccountHistoryDTO>>(historyEntities);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in GetAccHistoryByDates: {ex.Message}");
			return new List<AccountHistoryDTO>();
		}
	}

	public async Task<List<AccountHistoryDTO>> GetAccHistoryByUser(int id, bool includeUser = false)
	{
		try
		{
			var includeProps = includeUser ? "User" : "";
			var historyEntities = await _unitOfWork.UserHistoryRepository.GetAsync(
				x => x.UserId == id, includeProperties: includeProps);
			return _mapper.Map<List<AccountHistoryDTO>>(historyEntities);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in GetAccHistoryByUser: {ex.Message}");
			return new List<AccountHistoryDTO>();
		}
	}
}
