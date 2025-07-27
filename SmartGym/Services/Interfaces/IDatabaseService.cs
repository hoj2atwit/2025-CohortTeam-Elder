using System;
using SmartGym.Models;

namespace SmartGym.Services;

public interface IDatabaseService
{
	//TODO: Update signatures with args when we have built those out more.
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


}
