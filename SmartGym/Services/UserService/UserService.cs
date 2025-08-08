using AutoMapper;
using SmartGym.Data;
using SmartGym.Models;

namespace SmartGym.Services;

public class UserService : IUserService
{

	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	public UserService(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<UserDto> CreateUser(UserDto newUserData)
	{
		try
		{
			AppUser newUser = _mapper.Map<AppUser>(newUserData);
			await _unitOfWork.UserRepository.AddAsync(newUser);
			await _unitOfWork.SaveAsync();
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
			var users = await _unitOfWork.UserRepository.GetAsync();
			var userList = _mapper.Map<List<UserDto>>(users);
			return userList.ToList();
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
	/// <summary>
	/// gets all checkins for the given user id
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	public async Task<List<CheckinDTO>> GetUserCheckins(int id)
	{
		try
		{
			var checkinEntity = await _unitOfWork.CheckinRepository.GetAsync(x => x.UserId == id);
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
	/// <returns></returns>
	public async Task<List<CheckinDTO>> GetAllUserCheckins()
	{
		try
		{
			var checkinEntity = await _unitOfWork.CheckinRepository.GetAsync();
			return _mapper.Map<List<CheckinDTO>>(checkinEntity);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in GetAllUserCheckins: {ex.Message}");
			return null;
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
}
