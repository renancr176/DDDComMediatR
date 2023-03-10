using ApiMvno.Domain.Interfaces.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace ApiMvno.Domain.Validators;

public static class ValidatorsIoC
{
    public static void AddValidators(this IServiceCollection services)
    {
        #region Base configs / Enums

        services.AddScoped<IAddressTypeValidator, AddressTypeValidator>();
        services.AddScoped<ICountryValidator, CountryValidator>();
        services.AddScoped<IPhoneTypeValidator, PhoneTypeValidator>();

        #endregion

        #region Shared

        services.AddScoped<IAddressValidator, AddressValidator>();
        services.AddScoped<IPhoneValidator, PhoneValidator>();

        #endregion

        #region Company

        services.AddScoped<ICompanyAddressValidator, CompanyAddressValidator>();
        services.AddScoped<ICompanyPhoneValidator, CompanyPhoneValidator>();
        services.AddScoped<ICompanyValidator, CompanyValidator>();

        #endregion
    }
}