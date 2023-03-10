using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Interfaces.Repositories;
using ApiMvno.Domain.Interfaces.Validators;
using ApiMvno.Infra.Data.Contexts.MvnoDb.Seeders.Interfaces;

namespace ApiMvno.Infra.Data.Contexts.MvnoDb.Seeders;

public class CountrySeed : ICountrySeed
{
    private readonly ICountryRepository _countryRepository;
    private readonly ICountryValidator _countryValidator;

    public CountrySeed(ICountryRepository countryRepository, ICountryValidator countryValidator)
    {
        _countryRepository = countryRepository;
        _countryValidator = countryValidator;
    }

    public async Task SeedAsync()
    {
        var countries = new List<Country>()
        {
            new Country()
            {
                Name = "Brasil",
                PhoneCode = "55"
            }
        };

        foreach (var country in countries)
        {
            if (await _countryValidator.IsValidAsync(country))
            {
                await _countryRepository.InsertAsync(country);
                await _countryRepository.SaveChangesAsync();
            }
        }
    }
}