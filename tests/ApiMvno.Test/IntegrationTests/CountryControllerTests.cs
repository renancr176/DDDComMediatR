using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ApiMvno.Application.Commands.CountryCommands;
using ApiMvno.Application.Models;
using ApiMvno.Application.Models.Queries.Responses;
using ApiMvno.Domain.Entities;
using ApiMvno.Services.Api;
using ApiMvno.Services.Api.Models.Responses;
using ApiMvno.Test.Extensions;
using ApiMvno.Test.IntegrationTests.Config;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ApiMvno.Test.IntegrationTests;

[Collection(nameof(IntegrationTestsFixtureCollection))]
public class CountryControllerTests
{
    private readonly IntegrationTestsFixture<StartupTests> _testsFixture;

    public CountryControllerTests(IntegrationTestsFixture<StartupTests> testsFixture)
    {
        _testsFixture = testsFixture;
    }

    #region Negative Cases

    #endregion

    #region Positive Cases

    [Trait("IntegrationTest", "Controllers")]
    [Fact(DisplayName = "Country get by id should get country successfuly")]
    public async Task GetAll_GetCountryById_ShuldGetSuccessfuly()
    {
        // Arrange 
        var country = await _testsFixture.MvnoDbContext.Countries.FirstOrDefaultAsync(c => c.Active);

        if (country is null)
        {
            country = new Country("Test country", "0", true);
            await _testsFixture.MvnoDbContext.Countries.AddAsync(country);
            await _testsFixture.MvnoDbContext.SaveChangesAsync();
        }

        // Act & Assert
        var responseObj = await _testsFixture.Client
            .GetFromJsonAsync<BaseResponse<CountryModel?>>($"/Country/{country.Id}");

        // Assert 
        responseObj.Should().NotBeNull();
        responseObj.Success.Should().BeTrue();
        responseObj.Errors.Should().HaveCount(0);
        responseObj.Data.Should().NotBeNull();
    }

    [Trait("IntegrationTest", "Controllers")]
    [Fact(DisplayName = "Country get all should at least get a success response code 200/OK")]
    public async Task GetAll_RequestAllCountries_ShuldQuerySuccessfuly()
    {
        // Arrange 

        // Act & Assert
        var responseObj = await _testsFixture.Client
            .GetFromJsonAsync<BaseResponse<IEnumerable<CountryModel>?>>("/Country");

        // Assert 
        responseObj.Should().NotBeNull();
        responseObj.Success.Should().BeTrue();
        responseObj.Errors.Should().HaveCount(0);
    }

    [Trait("IntegrationTest", "Controllers")]
    [Fact(DisplayName = "Country search should at least get a success response code 200/OK")]
    public async Task GetAll_SearchCountries_ShuldQuerySuccessfuly()
    {
        // Arrange 

        // Act & Assert
        var responseObj = await _testsFixture.Client
            .GetFromJsonAsync<BaseResponse<PagedResponse<CountryModel>?>>("/Country/Search");

        // Assert 
        responseObj.Should().NotBeNull();
        responseObj.Success.Should().BeTrue();
        responseObj.Errors.Should().HaveCount(0);
    }

    [Trait("IntegrationTest", "Controllers")]
    [Fact(DisplayName = "Country given valid country should create a new country successfuly")]
    public async Task Create_GivenValidCountry_ShouldCreateSuccessfuly()
    {
        // Arrange 
        var request = new CreateCountryCommand("Teste country",
            "0",
            true);

        if (string.IsNullOrEmpty(_testsFixture.AdminAccessToken))
        {
            await _testsFixture.AuthenticateAsAdminAsync();
        }

        // Act 
        var response = await _testsFixture.Client
            .AddToken(_testsFixture.AdminAccessToken)
            .PostAsJsonAsync("/Country", request);

        // Assert 
        response.EnsureSuccessStatusCode();
        var responseObj = await response.DeserializeObject<BaseResponse<CountryModel>>();
        responseObj.Success.Should().BeTrue();
        responseObj.Errors.Should().HaveCount(0);
    }

    [Trait("IntegrationTest", "Controllers")]
    [Fact(DisplayName = "Country Given existing country should update successfuly")]
    public async Task Update_GivenExistingCountry_ShouldUpdateSuccessfuly()
    {
        // Arrange 
        var country = await _testsFixture.MvnoDbContext.Countries.FirstOrDefaultAsync();

        if (country is null)
        {
            country = new Country(
                "Teste country",
                "0",
                false);
            await _testsFixture.MvnoDbContext.Countries.AddAsync(country);
            await _testsFixture.MvnoDbContext.SaveChangesAsync();
        }

        var request = new UpdateCountryCommand(
            country.Id,
            $"{country.Name} Updated",
            country.PhoneCode,
            country.Active);

        if (string.IsNullOrEmpty(_testsFixture.AdminAccessToken))
        {
            await _testsFixture.AuthenticateAsAdminAsync();
        }

        // Act 
        var response = await _testsFixture.Client
            .AddToken(_testsFixture.AdminAccessToken)
            .PutAsJsonAsync("/Country", request);

        // Assert 
        response.EnsureSuccessStatusCode();
        var responseObj = await response.DeserializeObject<BaseResponse<CountryModel>>();
        responseObj.Success.Should().BeTrue();
        responseObj.Errors.Should().HaveCount(0);
    }

    #endregion
}