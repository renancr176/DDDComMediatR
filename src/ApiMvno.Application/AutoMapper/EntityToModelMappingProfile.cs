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
        CreateMap<UserCompany, UserCompanyModel>();

        #region Shared

        CreateMap<Address, AddressModel>();
        CreateMap<AddressType, AddressTypeModel>();
        CreateMap<Phone, PhoneModel>();
        CreateMap<PhoneType, PhoneTypeModel>();

        #endregion

        #region Entity Types

        CreateMap<Phone, PhoneModel>();
        CreateMap<PhoneType, PhoneTypeModel>();

        #endregion

        #region Company

        CreateMap<Company, CompanyModel>();
        CreateMap<CompanyAddress, CompanyAddressModel>();
        CreateMap<CompanyPhone, CompanyPhoneModel>();
        CreateMap<CompanyPhone, CompanyPhoneModel>();

        #endregion
    }
}