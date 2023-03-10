using ApiMvno.Application.Commands.AddressCommands;
using ApiMvno.Application.Commands.CompanyCommands;
using ApiMvno.Application.Commands.PhoneCommands;
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

        #region Shared

        #region Address

        CreateMap<CreateAddressCommand, Address>();
        CreateMap<UpdateAddressCommand, Address>();

        #endregion

        #region Phone

        CreateMap<CreatePhoneCommand, Phone>();
        CreateMap<UpdatePhoneCommand, Phone>();

        #endregion

        #endregion

        #region Company

        CreateMap<CreateCompanyCommand, Company>()
            .ForMember(dest => dest.CompanyAddresses,
                act => act.MapFrom(src =>
                    src.Addresses.Select(a =>
                        new CompanyAddress(
                            new Address(
                                a.CountryId,
                                a.AddressTypeId,
                                a.ZipCode,
                                a.State,
                                a.City,
                                a.Neighborhood,
                                a.StreetName,
                                a.StreetNumber,
                                a.Details))
                    )
                ))
            .ForMember(dest => dest.CompanyPhones,
                act => act.MapFrom(src =>
                    src.Phones.Select(p =>
                        new CompanyPhone(
                            new Phone(
                                p.PhoneTypeId,
                                p.Number))
                    )
                ));

        CreateMap<UpdateCompanyCommand, Company>();

        CreateMap<CreateCompanyAddressCommand, CompanyAddress>();
        CreateMap<UpdateCompanyAddressCommand, CompanyAddress>();

        CreateMap<CreateCompanyPhoneCommand, CompanyPhone>();
        CreateMap<UpdateCompanyPhoneCommand, CompanyPhone>();

        #endregion
    }
}