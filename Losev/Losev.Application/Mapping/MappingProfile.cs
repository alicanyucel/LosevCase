using AutoMapper;
using Losev.Application.Features.User.CreateUser;
using Losev.Application.Features.User.UpdateUser;
using Losev.Domain.Entities;

namespace Losev.Application.Mapping;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
      CreateMap<CreateUserCommand,AppUser>().ReverseMap();
      CreateMap<UpdateUserCommand,AppUser>().ReverseMap();
    }
}
