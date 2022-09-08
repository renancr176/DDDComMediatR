using ApiMvno.Application.Models;
using ApiMvno.Domain.Entities;
using AutoMapper;

namespace ApiMvno.Application.AutoMapper;

public class EntityToModelMappingProfile : Profile
{
    public EntityToModelMappingProfile()
    {
        CreateMap<Company, CompanyModel>();
    }
}