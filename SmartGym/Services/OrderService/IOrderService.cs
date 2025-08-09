using SmartGym.Models;

namespace SmartGym.Services;

public interface IOrderService
{
	Task<OrderDTO> CreateOrder(OrderDTO newOrderData);
	Task<OrderDTO> GetOrderById(int id);
	Task<List<OrderDTO>> GetAllUserOrders(int userId);
	Task<DateTime?> GetOrderTime(int id);
	Task<OrderDTO?> UpdateOrder(int id, OrderPatchDTO OrderDto);
	Task<bool> DeleteOrder(int id);
	Task<List<OrderDTO>> GetAllOrdersByStatus();
	Task<List<OrderDTO>> GetAllOrders();
}
