using SmartGym.Models;

namespace SmartGym.Services;

public interface IUserService
{
	Task<UserDto> CreateUser(UserDto newUserData);
	Task<UserDto> GetUserById(int id);
	Task<List<UserDto>> GetAllUsers();
	Task<List<CheckinDTO>> GetUserCheckins(int id);
	Task<List<CheckinDTO>> GetAllUserCheckins();
	void GetUserPaymentMethod(User user);
	void GetTrafficData(DateTime date);
	void GetTrafficData(DateTime startDate, DateTime endDate);
	Task<UserDto?> UpdateUser(int id, UserDto userDto);
	Task<bool> DeleteUser(int id);
}
