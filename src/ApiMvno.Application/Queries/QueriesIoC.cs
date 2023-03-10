using ApiMvno.Application.Queries.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ApiMvno.Application.Queries;

public static class QueriesIoC
{
    public static void AddQueries(this IServiceCollection services)
    {
        services.AddScoped<IAddressQuery, AddressQuery>();
        services.AddScoped<IPhoneQuery, PhoneQuery>();
        services.AddScoped<ICountryQuery, CountryQuery>();
        services.AddScoped<ICompanyQuery, CompanyQuery>();
    }
}