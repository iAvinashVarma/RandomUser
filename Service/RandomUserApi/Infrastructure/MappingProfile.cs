using AutoMapper;
using RandomUser.Business.Entity.Dto;
using RandomUser.Business.Model;

namespace RandomUserApi.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<User, User>();
            CreateMap<UserDto, UserDto>();
        }
    }
}
