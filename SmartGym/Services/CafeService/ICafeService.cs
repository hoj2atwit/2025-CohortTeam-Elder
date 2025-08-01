using SmartGym.Models;

namespace SmartGym.Services;

public interface ICafeService
{
	void GetOrderHistory(User user);
	void GetFullMenu();
	void GetMealPrepItems();
	void GetCafeItemModifications();
	void GetCurrentPromos();

}
