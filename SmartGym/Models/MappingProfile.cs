using AutoMapper;
using Newtonsoft.Json;

namespace SmartGym.Models;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		//Users
		CreateMap<User, UserDto>().ReverseMap();
		CreateMap<Checkin, CheckinDTO>().ReverseMap();

		//Classes
		CreateMap<Class, ClassDTO>().ReverseMap();
		CreateMap<Class, ClassPostDTO>().ReverseMap();
		CreateMap<Class, ClassPatchDTO>().ReverseMap();
    CreateMap<Booking, BookingDTO>().ReverseMap();
    CreateMap<Booking, BookingPostDTO>().ReverseMap();
    CreateMap<Booking, BookingPatchDTO>().ReverseMap();

    //Orders
    CreateMap<Order, OrderDTO>()
      .AfterMap((src, dest) =>
      {
        dest.OrderCartList = string.IsNullOrEmpty(src.OrderCart)
        ? new List<CartItemsDTO>()
        : JsonConvert.DeserializeObject<List<CartItemsDTO>>(src.OrderCart);
      })
      .ReverseMap();
    CreateMap<Order, OrderPatchDTO>().ReverseMap();
		CreateMap<MenuItem, MenuItemsDTO>().ReverseMap();

		CreateMap<Images, ImagesDTO>().ReverseMap();
	}
}