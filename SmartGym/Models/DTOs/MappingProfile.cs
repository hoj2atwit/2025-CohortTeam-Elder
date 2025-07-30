using AutoMapper;

namespace SmartGym.Models;

public class MappingProfile : Profile
{
  public MappingProfile()
  {
    //Users


    //Classes
    CreateMap<Class, ClassDTO>().ReverseMap();
    CreateMap<Class, ClassPostDTO>().ReverseMap();
    CreateMap<Class, ClassPatchDTO>().ReverseMap();

  }
}