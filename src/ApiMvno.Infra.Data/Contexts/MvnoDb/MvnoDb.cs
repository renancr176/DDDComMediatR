using ApiMvno.Domain.Interfaces.Repositories;
using ApiMvno.Infra.Data.Contexts.MvnoDb.Repositories;
using ApiMvno.Infra.Data.Contexts.MvnoDb.Seeders;
using ApiMvno.Infra.Data.Contexts.MvnoDb.Seeders.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb;

public static class MvnoDb
{
    public static void AddMvnoDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MvnoDbContext>(dbContextOptions =>
            dbContextOptions.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        #region Repositories

        #region Shared

        services.AddScoped<IAddressTypeRepository, AddressTypeRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<IPhoneTypeRepository, PhoneTypeRepository>();
        services.AddScoped<IPhoneRepository, PhoneRepository>();

        #endregion

        #region Company

        services.AddScoped<ICompanyAddressRepository, CompanyAddressRepository>();
        services.AddScoped<ICompanyPhoneRepository, CompanyPhoneRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();

        #endregion

        #endregion

        #region Seeders

        services.AddScoped<IAddressTypeSeed, AddressTypeSeed>();
        services.AddScoped<ICountrySeed, CountrySeed>();
        services.AddScoped<IPhoneTypeSeed, PhoneTypeSeed>();

        #endregion
    }

    public static void MvnoDbMigrate(this IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetService<MvnoDbContext>();
        dbContext.Database.Migrate();

        #region Seeders

        Task.Run(async () =>
        {
            await serviceProvider.GetService<IAddressTypeSeed>().SeedAsync();
            await serviceProvider.GetService<ICountrySeed>().SeedAsync();
            await serviceProvider.GetService<IPhoneTypeSeed>().SeedAsync();
        }).Wait();

        #endregion
    }
}