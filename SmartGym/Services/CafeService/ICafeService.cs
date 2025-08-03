using SmartGym.Models;

namespace SmartGym.Services;

public interface ICafeService
{
	#region Menu Items
	void GetFullMenu();
	void GetMealPrepItems();
	void GetCafeItemModifications();
	void GetCurrentPromos();
	#endregion

	#region Orders
	void GetOrderHistory(User user);
	Task<OrderDTO> CreateOrder(OrderDTO newOrderData);
	Task<OrderDTO> GetOrderById(int id);
	Task<List<OrderDTO>> GetAllUserOrders(int userId);
	Task<DateTime?> GetOrderTime(int id);
	Task<OrderDTO?> UpdateOrder(int id, OrderPatchDTO OrderDto);
	Task<bool> DeleteOrder(int id);

	#endregion
}
