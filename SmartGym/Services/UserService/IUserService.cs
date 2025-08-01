using SmartGym.Models;

namespace SmartGym.Services;

public interface IUserService
{
	void CreateUser(User user);
	void UpdateUser(User user);
	void GetUser(User user);
	void GetUserCheckInHistory(User user);
	void GetAllUsers();
	void GetUserPaymentMethod(User user);
	void GetTrafficData(DateTime date);
	void GetTrafficData(DateTime startDate, DateTime endDate);
}
