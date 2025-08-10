using AutoMapper;
using Newtonsoft.Json;

namespace SmartGym.Models;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		//Users
		CreateMap<AppUser, UserDto>().ReverseMap();
		CreateMap<Checkin, CheckinDTO>().ReverseMap();
		CreateMap<AccountHistory, AccountHistoryDTO>().ReverseMap();

		//Classes
		CreateMap<Class, ClassDTO>().ReverseMap();
		CreateMap<Class, ClassPostDTO>().ReverseMap();
		CreateMap<Class, ClassPatchDTO>().ReverseMap();
		CreateMap<ClassSession, ClassSessionDTO>().ReverseMap();
		CreateMap<Booking, BookingDTO>().ReverseMap();
		CreateMap<Booking, BookingPostDTO>().ReverseMap();
		CreateMap<Booking, BookingPatchDTO>().ReverseMap();
		CreateMap<Waitlist, WaitlistDTO>().ReverseMap();

		//Orders
		CreateMap<Order, OrderDTO>()
			.AfterMap((src, dest) =>
			{
				dest.OrderCartList = string.IsNullOrEmpty(src.OrderCart)
					? new List<CartItemsDTO>()
					: JsonConvert.DeserializeObject<List<CartItemsDTO>>(src.OrderCart);
			})
			.ReverseMap();

		CreateMap<Order, OrderPatchDTO>()
			.ReverseMap()
			.AfterMap((src, dest) =>
			{
				dest.OrderCart = src.OrderCartList != null
					? JsonConvert.SerializeObject(src.OrderCartList)
					: null;
			});
		CreateMap<MenuItem, MenuItemsDTO>().ReverseMap();

		CreateMap<Images, ImagesDTO>().ReverseMap();
	}
}
