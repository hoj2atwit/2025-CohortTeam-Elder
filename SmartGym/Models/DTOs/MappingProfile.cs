using AutoMapper;

namespace SmartGym.Models;

public class MappingProfile : Profile
{
  public MappingProfile()
  {
    //Users
    CreateMap<User, UserDto>().ReverseMap();

    //Classes
    CreateMap<Class, ClassDTO>().ReverseMap();
    CreateMap<Class, ClassPostDTO>().ReverseMap();
    CreateMap<Class, ClassPatchDTO>().ReverseMap();

  }
}