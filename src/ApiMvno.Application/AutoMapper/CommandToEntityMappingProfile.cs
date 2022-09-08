using ApiMvno.Application.Commands.CompanyCommands;
using ApiMvno.Domain.Entities;
using AutoMapper;

namespace ApiMvno.Application.AutoMapper;

public class CommandToEntityMappingProfile : Profile
{
    public CommandToEntityMappingProfile()
    {
        CreateMap<CreateCompanyCommand, Company>()
            .ConstructUsing(command => new Company(command.Name));
    }
}