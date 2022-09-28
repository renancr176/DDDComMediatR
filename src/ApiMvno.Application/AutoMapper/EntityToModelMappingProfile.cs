using ApiMvno.Application.Models;
using ApiMvno.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace ApiMvno.Application.AutoMapper;

public class EntityToModelMappingProfile : Profile
{
    public EntityToModelMappingProfile()
    {
        CreateMap<IdentityUser<Guid>, UserModel>();
        CreateMap<Company, CompanyModel>();
        CreateMap<Address, AddressModel>();
    }
}