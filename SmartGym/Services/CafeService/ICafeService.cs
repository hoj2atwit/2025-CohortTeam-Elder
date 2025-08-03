using SmartGym.Models;

namespace SmartGym.Services;

public interface ICafeService
{
	#region Menu Items
	Task<List<MenuItemsDTO>> GetFullMenu();
	Task<MenuItemsDTO> GetMenuItem(int itemId);
	Task<MenuItemsDTO?> UpdateMenuItem(int itemId, MenuItemsDTO menuItemDto);
	Task<bool> DeleteMenuItem(int itemId);
	// void GetMealPrepItems();
	// void GetCafeItemModifications();
	// void GetCurrentPromos();
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
