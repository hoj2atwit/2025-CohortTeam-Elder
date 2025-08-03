using AutoMapper;
using Newtonsoft.Json;
using SmartGym.Data;
using SmartGym.Models;

namespace SmartGym.Services;

public class OrderService : IOrderService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public void GetOrderHistory(User user)
	{
		throw new NotImplementedException();
	}
	/// <summary>
	/// Creates an order 
	/// </summary>
	/// <param name="newOrderData"></param>
	/// <returns>created order</returns>
	public async Task<OrderDTO> CreateOrder(OrderDTO newOrderData)
	{
		try
		{
			
			newOrderData.OrderCart = JsonConvert.SerializeObject(newOrderData.OrderCartList);

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

	/// <summary>
	/// Gets an order by Id 
	/// </summary>
	/// <param name="id"></param>
	/// <returns>An specific order and its details</returns>
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

	/// <summary>
	/// Gets all orders from a specific user 
	/// </summary>
	/// <param name="userId">comes from query based on the user who is logged in</param>
	/// <returns>order from that specific user</returns>
	public async Task<List<OrderDTO>> GetAllUserOrders(int userId)
	{
		try
		{
			var userOrders = await _unitOfWork.OrderRepository.GetAsync(o => o.UserId == userId);
			var OrderList = _mapper.Map<List<OrderDTO>>(userOrders);
			return OrderList.ToList();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in GetAllUserOrders: {ex.Message}");
			return null;
		}
	}
	/// <summary>
	/// Gets the time that the order is supposed to be ready
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
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
	/// <summary>
	/// Updates an order if anything changes before it is ready
	/// Maybe we can soft delete orders once they're ready (something like changing it to ready or resolved) - otherwise, getting all orders from user wouldn't make sense
	/// Implement status?
	/// </summary>
	/// <param name="id"></param>
	/// <param name="newOrderData"></param>
	/// <returns></returns>
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
	/// <summary>
	/// deletes an order
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
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
