using System;
using SmartGym.Models;

namespace SmartGym.Services;

public interface IDatabaseService
{
	#region User

	void CreateUser(User user);
	void UpdateUser(User user);
	void GetUser(User user);
	void GetUserCheckInHistory(User user);
	void GetAllUsers();
	void GetUserPaymentMethod(User user);
	void GetTrafficData(DateTime date);
	void GetTrafficData(DateTime startDate, DateTime endDate);

	#endregion


	#region Cafe

	void GetOrderHistory(User user);
	void GetFullMenu();
	void GetMealPrepItems();
	void GetCafeItemModifications();
	void GetCurrentPromos();

	#endregion

	#region Classes
	Task<bool> CreateClass(ClassDTO newClassData);


	#endregion


}
