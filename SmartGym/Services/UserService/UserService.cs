using AutoMapper;
using SmartGym.Data;
using SmartGym.Models;

namespace SmartGym.Services.UserService;

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
			User newUser = _mapper.Map<User>(newUserData);
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
	public void GetUserCheckInHistory(User user)
	{
		throw new NotImplementedException();
	}

	public void GetUserPaymentMethod(User user)
	{
		throw new NotImplementedException();
	}

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
