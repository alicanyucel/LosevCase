using AutoMapper;
using Losev.Application.Dtos.User;
using Losev.Domain.Entities;

namespace Losev.Application.Mapping;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        // entitiler veri tabanı oluşumrak için dtolarda applicationda business katmanlar için
        // command ve commandhandlerda cqrs controllerda kullanmak için
        CreateMap<AppUser, UserResultDto>().ReverseMap();
    }
}
