using AutoMapper;
using SmartGym.Data;
using SmartGym.Models;

namespace SmartGym.Services.OrderService;

public class OrderService : IOrderService
{

  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;
  public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
  {
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }

  public async Task<OrderDTO> CreateOrder(OrderDTO newOrderData)
  {
    try
    {
      Order newOrder = _mapper.Map<Order>(newOrderData);
      await _unitOfWork.OrderRepository.AddAsync(newOrder);
      await _unitOfWork.SaveAsync();
      return _mapper.Map<OrderDTO>(newOrder);
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error in CreateOrder: {ex.Message}");
      throw;
    }
  }
  public async Task<OrderDTO> GetOrderById(int id)
  {
    try
    {
      var order = await _unitOfWork.OrderRepository.GetAsync(id);
      return _mapper.Map<OrderDTO>(order);
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error in GetOrderById: {ex.Message}");
      return null;
    }
  }
  public async Task<List<OrderDTO>> GetAllUserOrders(int userId)
  {
    try
    {
      var userOrders = await _unitOfWork.OrderRepository.GetAllOrdersByUserIdAsync(userId);
      var OrderList = _mapper.Map<List<OrderDTO>>(userOrders);
      return OrderList.ToList();
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error in GetAllUserOrders: {ex.Message}");
      return null;
    }
  }
  public async Task<DateTime?> GetOrderTime(int id)
  {
    try
    {
      var order = await _unitOfWork.OrderRepository.GetAsync(id);
      if (order == null)
      {
        Console.WriteLine($"Order with ID {id} not found.");
        return null;
      }
      return order.OrderTime;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error in GetOrderTime: {ex.Message}");
      throw;
    }
  }

  public async Task<OrderDTO?> UpdateOrder(int id, OrderPatchDTO newOrderData)
  {
    try
    {
      var order = await _unitOfWork.OrderRepository.GetAsync(id);
      if (order == null)
        return null;

      _mapper.Map(newOrderData, order);

      _unitOfWork.OrderRepository.Update(order);
      await _unitOfWork.SaveAsync();
      return _mapper.Map<OrderDTO>(order);
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error while updating Order: {ex.Message}");
      return null;
    }
  }
  public async Task<bool> DeleteOrder(int id)
  {
    try
    {
      var order = await _unitOfWork.OrderRepository.GetAsync(id);
      if (order == null)
        return false;

      _unitOfWork.OrderRepository.Delete(order);
      await _unitOfWork.SaveAsync();
      return true;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error while deleting user: {ex.Message}");
      return false;
    }
  }
}
