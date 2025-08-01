using SmartGym.Models;

namespace SmartGym.Data;

public interface IOrderRepository : IRepository<Order>
{
  Task<List<Order>> GetAllOrdersByUserIdAsync(int userId);
}
