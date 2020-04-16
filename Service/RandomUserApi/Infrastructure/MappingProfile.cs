using AutoMapper;
using RandomUser.Business.Entity.Dto;
using RandomUser.Business.Model;

namespace RandomUserApi.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();
            CreateMap<User, User>();
            CreateMap<UserModel, UserModel>();
        }
    }
}
