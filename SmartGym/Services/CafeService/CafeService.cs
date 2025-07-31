using AutoMapper;
using SmartGym.Data;
using SmartGym.Models;

namespace SmartGym.Services;

public class CafeService : ICafeService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	public CafeService(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public void GetCafeItemModifications()
	{
		throw new NotImplementedException();
	}

	public void GetCurrentPromos()
	{
		throw new NotImplementedException();
	}

	public void GetFullMenu()
	{
		throw new NotImplementedException();
	}

	public void GetMealPrepItems()
	{
		throw new NotImplementedException();
	}

	public void GetOrderHistory(User user)
	{
		throw new NotImplementedException();
	}
}
