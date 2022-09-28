using ApiMvno.Application.Commands.AddressCommands;
using ApiMvno.Application.Commands.CompanyCommands;
using ApiMvno.Application.Commands.UserCommands;
using ApiMvno.Domain.Entities;
using AutoMapper;

namespace ApiMvno.Application.AutoMapper;

public class CommandToEntityMappingProfile : Profile
{
    public CommandToEntityMappingProfile()
    {
        #region User

        CreateMap<SignUpCommand, User>();

        #endregion

        #region Company

        CreateMap<CreateCompanyCommand, Company>();

        #endregion

        #region Address

        CreateMap<CreateAddressCommand, Address>();
        CreateMap<UpdateAddressCommand, Address>();

        #endregion

    }
}