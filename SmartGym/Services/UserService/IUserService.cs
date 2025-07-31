using SmartGym.Models;

namespace SmartGym.Services;

public interface IUserService
{
	Task<UserDto> CreateUser(UserDto newUserData);
	Task<UserDto> GetUserById(int id);
	void GetUserCheckInHistory(User user);
	Task<List<UserDto>> GetAllUsers();
	void GetUserPaymentMethod(User user);
	void GetTrafficData(DateTime date);
	void GetTrafficData(DateTime startDate, DateTime endDate);
	Task<UserDto?> UpdateUser(int id, UserDto userDto);
	Task<bool> DeleteUser(int id);
}
